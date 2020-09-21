using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanText: MonoBehaviour {

    [SerializeField] Animator _textAnim;
    private bool _readText = false;


    //Texto animado de la camioneta

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hands") && !_readText)
        {
            _readText = true;
            _textAnim.SetTrigger("VanText");
        }
    }



}
