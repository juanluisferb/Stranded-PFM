using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainScene : MonoBehaviour {

    [SerializeField] OVRScreenFade _visionFade;
    [SerializeField] AudioSource _backgroundMusic;

    //Al pasar por el trigger en la pantalla de game over se vuelve a la escena 2 (nuevo intento)
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("Hands"))
        {
            _visionFade.FadeOut();
            Invoke("CompleteReturnToMainScene", 2.0f);
            StartCoroutine(AudioController.FadeOut(_backgroundMusic, 5f));

        }
    }
    

    private void CompleteReturnToMainScene()
    {
        SceneManager.LoadScene("Scene2");
    }
}
