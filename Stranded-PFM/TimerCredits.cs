using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCredits : MonoBehaviour {

    [SerializeField] OVRScreenFade _fadevision;
    [SerializeField] AudioSource _backgroundMusic;
    private void Start()
    {
        Invoke("Fade", 10.0f); 
    }

    
    //Sale de la aplicación desde los créditos
    private void Fade()
    {
        _fadevision.FadeOut();
        Invoke("CompleteExitGame", 2.0f);
        StartCoroutine(AudioController.FadeOut(_backgroundMusic, 2f));

    }

    void CompleteExitGame()
    {
        Application.Quit();
    }
}
