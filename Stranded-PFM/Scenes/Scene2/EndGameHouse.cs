using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameHouse : MonoBehaviour {

    [SerializeField] Text _text;
    [SerializeField] Animator _jumpScareAnimator;
    [SerializeField] CharacterController _chara;
    [SerializeField] GameObject _wendigo;
    [SerializeField] AudioSource _backgroundMusic;

    private bool _isLoading = false;
    public bool EndGameKey { get; set; }

    private void OnTriggerStay(Collider other)
    {
            if (other.CompareTag("Hands") &&
            EndGameKey)
            {
                //Aparece Texto al pasar la mano
                _text.gameObject.SetActive(true);
                _text.text = "Pulsa Táctil derecho para entrar";
                _text.color = Color.white;

            if (!_isLoading)
            {
                //Carga la cinemática final
                if (OVRInput.GetDown(OVRInput.Touch.SecondaryThumbRest))
                {
                    StartCoroutine(AudioController.FadeOut(_backgroundMusic, 5f));
                    _isLoading = true;
                    _chara.enabled = false;
                    _wendigo.SetActive(true);
                    _jumpScareAnimator.SetTrigger("Scare");

                }
            }
                

            }
        

    //Si no tengo la llave, no puedo acceder al final
        if (other.CompareTag("Hands") &&
            !EndGameKey)
        {
            _text.gameObject.SetActive(true);
            _text.text = "Necesito la llave";
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

   
}
