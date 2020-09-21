using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class House2 : MonoBehaviour {

    [SerializeField] Text _text;

    //Texto de la casa que no puede abrirse
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hands"))
        {
            _text.gameObject.SetActive(true);
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
