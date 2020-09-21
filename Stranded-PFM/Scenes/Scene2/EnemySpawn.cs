using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    [SerializeField] Transform[] newPatrolPoints;
    [SerializeField] Transform[] newAlertPatrolPoints;
    [SerializeField] GameObject _enemy;
    

    //Si el jugador cruza el trigger, éste asigna nuevos puntos de patrulla y de alerta al monstruo para que siempre 
    //vaya a la zona a la que va el jugador
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Hands"))
        { 
            EnemyMovement enemyMov = _enemy.GetComponent<EnemyMovement>();
            enemyMov._bIsIdling = false;
            enemyMov._patrolPoints = newPatrolPoints;
            enemyMov._currentPathIndex = 0;
            EnemyHead headEnemy = _enemy.GetComponentInChildren<EnemyHead>();
            headEnemy._AlertPatrolPoints = newAlertPatrolPoints;

            enemyMov._agent.SetDestination(enemyMov._patrolPoints[enemyMov._currentPathIndex].position);


        }
    }
}
