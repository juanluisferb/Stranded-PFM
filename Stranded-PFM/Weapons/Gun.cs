using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{

    [SerializeField] AudioSource _noAmmo;
    [SerializeField] AudioSource shoot;
    [SerializeField] protected float _timeBetweenShoots = 0.2f;
    [SerializeField] protected Transform _aimPoint;
    [SerializeField] Animator _handAnimator;
    [SerializeField] protected float _maxGunDistance;
    [SerializeField] Animator _gunShoot;
    protected float _nextShootTime;

    public override void PushTrigger()
    {
        base.PushTrigger();
        LaunchProyectile();
    }


    public override void StartReload()
    {
        base.StartReload();
        if(_chargerCurrentAmmo < _chargerMaxAmmo &&
           _playerCurrentAmmo > 0)
        {
            _gunShoot.SetBool("NoAmmo", false);
        }
        
    }

    private void LaunchProyectile()
    {
        if (_chargerCurrentAmmo == 0 &&
                !_gunShoot.GetBool("NoAmmo"))
        {
            _gunShoot.SetBool("NoAmmo", true);
            _noAmmo.Play();
            return;
        }


        if (_chargerCurrentAmmo > 0)
        {
            _chargerCurrentAmmo -= 1;

            _handAnimator.SetTrigger("GunShoot");
            _gunShoot.SetTrigger("Shoot");
            shoot.Play();

            //Acción de la vibración mando
            OVRHaptics.Channels[1].Preempt(new OVRHapticsClip(shoot.clip, 0));

	    //Animación pistola
            if (_fireMuzzle != null)
            {
                _fireMuzzle.Play();
            }
            

            RaycastHit hit;
            //Raycast


            //Funcionamiento similar al de la escopeta, pero la pistola necesita 3 disparos en la cabeza del enemigo para que éste se aturda
            if (Physics.Raycast(_aimPoint.transform.position, _aimPoint.transform.forward, out hit, _maxGunDistance))
            {
                EnemyBehaviour _enemyB = hit.collider.GetComponentInParent<EnemyBehaviour>();
                EnemyHead head = hit.collider.GetComponent<EnemyHead>();
                EnemyMovement _enemyMov = hit.collider.GetComponent<EnemyMovement>();

		//Si impacta el disparo en la cabeza, llamo al método del monstruo para que reproduzca la animación, aparte de recibir daño
                if (head != null)
                {
                    head.Hit();

                    
                    _enemyB.TakeDamage(_damage);
                    
                    return;
                }
		
		//El enemigo recibirá daño	
                else if (_enemyB != null)
                {
                    _enemyB.TakeDamage(_damage);
                    return;
                }


		//El enemigo será alertado por la acción del disparo
                else if (_enemyMov != null)
                {
                    _enemyMov.EnemyAlert();
                }
            }

            

        }

    }




}
