using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWendigo : MonoBehaviour {

    [SerializeField] Animator _enemy;

    //Trigger que lanza la animación del enemigo atacando en la cinemática Interval
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _enemy.SetTrigger("Attack");
        }
    }
}
