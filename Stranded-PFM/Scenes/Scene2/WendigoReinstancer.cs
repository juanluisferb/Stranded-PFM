using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WendigoReinstancer : MonoBehaviour {

    [SerializeField] EnemyMovement _enemy;
    [SerializeField] Transform _reinstancer;
    [SerializeField] ParticleSystem _auraReinstance;

    Vector3 _OldEnemyPosition;
    [SerializeField] float _rateCheck;
    private float _TimeCheck;

   
    //Código para resituar al enemigo en una posición neutra en caso de que se quede atascado 
    //en el terrano dada la irregularidad del mismo.
    
    private void Start()
    {
        _OldEnemyPosition = _enemy.transform.position;
        InvokeRepeating("ResetEnemy", 2.0f, _rateCheck);
        
    }

    

    private void ResetEnemy()
    {
        if (_enemy.bIsDead) return;
        //Comparo la posición cada X tiempo para ver si sigue en el mismo sitio
        if((_enemy.transform.position == _OldEnemyPosition))
        {
            Invoke("Reinstance", 3.0f);
            _auraReinstance.Play();
        }

        _OldEnemyPosition = _enemy.transform.position;
    }

    private void Reinstance()
    {
        _enemy.transform.position = _reinstancer.position;
        //Reseteo al enemigo en caso de que estuviese buscando al player
        _enemy._hasSeenPlayer = false;
        _auraReinstance.Stop();
    }
}
