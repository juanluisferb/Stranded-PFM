using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour {

    [SerializeField] PauseGame _pause;

    //Botón de despausar
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hands"))
        {
            _pause.TogglePause();
        }
    }
}
