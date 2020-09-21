using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Howl : MonoBehaviour {

    [SerializeField] AudioSource _howlAudio;
    private bool _hasSounded = false;

    //Rugidos por el mapa para asustar al jugador
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hands") && !_hasSounded){

        _howlAudio.Play();
        _hasSounded = true;
            
        }
        
        
    }


}
