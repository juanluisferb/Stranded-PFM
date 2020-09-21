using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour {

    [SerializeField] GameObject _pausePanel;
    [SerializeField] CharacterController _character;
    [SerializeField] OVRScreenFade _visionFade;


    float _TimeToPause;
    float _TimeBetweenPauses = 1f;

    public bool _paused;

    private void Awake()
    {

        _pausePanel.SetActive(false);
        
    }

    //Llamada a pausa una vez por segundo como máximo
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.Start) &&
            Time.time > _TimeToPause &&
            !_paused)
        {
            _TimeToPause = Time.time + _TimeBetweenPauses;
            
            TogglePause();
        }


    }


    //Pausa, revisar para implementar Time.timeScale = 0 con recuperación de trackeo de mandos de las Oculus
    //Se activa el panel de pausa
    public void TogglePause()
    {
        _paused = !_paused;
        _character.enabled = !_paused;
        _pausePanel.SetActive(_paused);
    }
    
}
