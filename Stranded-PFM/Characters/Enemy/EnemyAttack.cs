using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    [SerializeField] float _damage = 1;
    [SerializeField] float _damageRate;
    private float _nextTimeDamaged;
    

    //Comparo si las manos del monstruo colisionan con el player o con sus manos (mandos de Oculus)
    protected virtual void OnTriggerEnter(Collider other)
    {
        //Rate de ataque para que no haga daño cada frame
        if (Time.time >= _nextTimeDamaged)
        {
            _nextTimeDamaged = Time.time + _damageRate;
            Character character = other.GetComponent<Character>();

            if (character != null && (other.CompareTag("Player")))
            {
                character.TakeDamage(_damage);

            }

            else if (other.CompareTag("Hands"))
            {
                Character chara = other.GetComponentInParent<Character>();

                if(chara != null)
                {
                    chara.TakeDamage(_damage);
                }
            }
        }

        
    }

}
