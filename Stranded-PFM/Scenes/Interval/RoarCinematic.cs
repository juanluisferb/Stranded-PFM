using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoarCinematic : MonoBehaviour {
	
    [SerializeField] AudioSource _roarSound;

    public void Roar(){
	//Control del rugido del enemigo en la cinemática    
        if (!_roarSound.isPlaying)
        {
            _roarSound.Play();
        }
    }
}
