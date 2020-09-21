using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummoningCircle : MonoBehaviour {

    [SerializeField] EnemyMovement _enemy;

    //El enemigo va al pentáculo si el player lo toca
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hands"))
        {
            _enemy._agent.SetDestination(this.transform.position);
        }
    }
}
