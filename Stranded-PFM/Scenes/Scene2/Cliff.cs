using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cliff : MonoBehaviour {

    [SerializeField] Player _player;
    private float _timeBetweenDamages = 5.0f;
    private float _nextDamageTime;

    //Daño de "Caída" desde el saliente 
    private void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Hands"))
        {
            if(Time.time > _nextDamageTime)
            {
                _nextDamageTime = Time.time + _timeBetweenDamages;

                _player.TakeDamage(20.0f);
            }
            
        }

    }
}
