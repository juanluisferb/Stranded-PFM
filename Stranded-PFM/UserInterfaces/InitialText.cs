using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialText : MonoBehaviour {

    [SerializeField] Animator _textAnim;
    private bool _readText = false;

    //Texto animado inicial de la escena 2
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hands") && !_readText)
        {
            _readText = true;
            _textAnim.SetTrigger("InitialText");
        }
    }
}
