using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smartphone : MonoBehaviour {

    [SerializeField] AudioSource _vibration;
    [SerializeField] Material _screen;


    //Para la vibración del movil (elemento informativo)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hands"))
        {

            _vibration.Stop();

        }


    }
}
