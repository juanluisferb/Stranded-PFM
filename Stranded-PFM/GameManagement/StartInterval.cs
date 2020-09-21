using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartInterval : MonoBehaviour {

    [SerializeField] Text _text;
    [SerializeField] AudioSource _carAudio;
    [SerializeField] AudioSource _carBeep;
    [SerializeField] AudioSource _doorCar;

    [SerializeField] Animator _garageAnimator;
    [SerializeField] Animator _gameTitle;
    [SerializeField] BoxCollider _wall;
    [SerializeField] OVRScreenFade _visionFade;
    [SerializeField] GameObject _light1;
    [SerializeField] GameObject _light2;
    [SerializeField] CharacterController _chara;

    private bool hasPlayed;
    private bool isLoadingInterval;
    public bool StartGameKey { get; set; }

    private void Awake()
    {
        hasPlayed = false;
        isLoadingInterval = false;
        StartGameKey = false;

    }


    private void Update()
    {

    if (!hasPlayed)
        {
            //Control de la secuencia de animación de la puerta del garaje para que solo se lance una vez
            if (_garageAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f &&
                _garageAnimator.GetCurrentAnimatorStateInfo(0).IsName("GarageDoor"))

            {
                hasPlayed = true;
                _doorCar.PlayOneShot(_doorCar.clip);
                
                Invoke("StartLoadNextScene", _doorCar.clip.length);
                

            }

        }

    }


    private void OnTriggerStay(Collider other)
    {
        //Controlo que no se cargue la escena más de una vez
        if (!isLoadingInterval)
        {
            //Si tienes la llave en la mano, puedes acceder a la siguiente escena
            if (other.CompareTag("Hands") &&
            StartGameKey)
            {
                //Aparece Texto
                _text.gameObject.SetActive(true);
                _text.text = "Pulsa táctil izquierdo para empezar";
                _text.color = Color.white;

                //activa la carga de la siguiente escena con el táctil del mando
                if (OVRInput.GetDown(OVRInput.Touch.PrimaryThumbRest))
                {
                    isLoadingInterval = true;
                    _carBeep.PlayOneShot(_carBeep.clip);
                    _light1.SetActive(true);
                    _light2.SetActive(true);
                    _chara.enabled = false;
                    _garageAnimator.SetTrigger("OpenDoor");
                    _gameTitle.SetTrigger("GameTitle");
                    _wall.gameObject.SetActive(true);

                }

            }
        }
        
        //Si no se tiene la llave, salta el aviso
        if(other.CompareTag("Hands") &&
            !StartGameKey)
        {
            _text.gameObject.SetActive(true);
            _text.text = "Necesitas la llave del coche";
            _text.color = Color.red;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hands"))
        {
            _text.gameObject.SetActive(false);
        }
        
    }

    private void StartLoadNextScene()
    {
        _visionFade.FadeOut();
        _carAudio.PlayOneShot(_carAudio.clip);
        Invoke("EndLoadNextScene", 2.0f);
    }

    private void EndLoadNextScene()
    {
        SceneManager.LoadScene("Interval");
    }
}
