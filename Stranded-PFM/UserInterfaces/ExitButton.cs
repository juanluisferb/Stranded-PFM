using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour {

    [SerializeField] OVRScreenFade _visionFade;


    //Botón de exit del manu de pausa
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
        Application.Quit();
    }

}
