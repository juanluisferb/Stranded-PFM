using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHead : MonoBehaviour
{

    int numberOfHits;
    float LastImpact;
    [SerializeField] Animator _EnemyAnim;
    [SerializeField] public Transform[] _AlertPatrolPoints;
    NavMeshAgent _agent;
    EnemyMovement _enemy;

    private void Awake()
    {
        _agent = GetComponentInParent<NavMeshAgent>();
        _enemy = GetComponentInParent<EnemyMovement>();

    }

    private void Start()
    {
        numberOfHits = 0;
    }


    private void FixedUpdate()
    {
        //Si está aturdido, lo congelo
        if (_EnemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 &&
                            !_EnemyAnim.IsInTransition(0) &&
                            _EnemyAnim.GetCurrentAnimatorStateInfo(0).IsName("Stun"))
        {
            _agent.velocity = Vector3.zero;
        }
    }

    //Método que lanza la animación de stuneo si recibe 3 o más disparos en un lapso de 3 segundos, en 5 se resetea
    public void Hit()
    {
        if (_enemy.bIsDead) return;

        numberOfHits++;

        float CurrentTime = Time.time;
        float diff = LastImpact - CurrentTime;

        if (diff > 5.0f)
        {
            numberOfHits = 0;
        }

        if (numberOfHits >= 3 && diff < 3.0f)
        {
            
            _EnemyAnim.SetTrigger("Stun");
            numberOfHits = 0;

            //Cambio los puntos de patrulla normales por los de alerta para variarle el comportamiento
            EnemyMovement enemy = GetComponentInParent<EnemyMovement>();
            enemy._patrolPoints = _AlertPatrolPoints;
            enemy._currentPathIndex = 0;
           

        }

        LastImpact = Time.time;
    }


    //llama directamente a la animación de stun
    public void HitByShotGun()
    {
        //controlo la muerte del enemigo
        if (_enemy.bIsDead) return;

        _EnemyAnim.SetTrigger("Stun");

        EnemyMovement enemy = GetComponentInParent<EnemyMovement>();
        enemy._patrolPoints = _AlertPatrolPoints;
        enemy._currentPathIndex = 0;
    }



}