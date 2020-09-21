using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitApplication : MonoBehaviour
{
    [SerializeField] OVRScreenFade _visionFade;
    [SerializeField] Text _text;

    //Botón de exit de la demo desde el menú de pausa
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hands"))
        {
            //Aparece Texto
            _text.gameObject.SetActive(true);
            
            //Si se pulsa cualquiera de los gatillos, se sale del juego
            if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) >= 0.55f ||
                OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) >= 0.55f)
            {
                _visionFade.FadeOut();
                Invoke("CompleteExit", 2.0f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _text.gameObject.SetActive(false);
    }


    private void CompleteExit()
    {
        Application.Quit();
    }
}
