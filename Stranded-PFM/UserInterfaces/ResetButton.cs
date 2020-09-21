using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour {

    [SerializeField] OVRScreenFade _visionFade;

    //Botón de reset desde la escena 2
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hands"))
        {
            _visionFade.FadeOut();
            Invoke("CompleteExitGame", 2.0f);
        }
    }




    private void CompleteExitGame()
    {
        SceneManager.LoadScene("Scene2");
    }

}
