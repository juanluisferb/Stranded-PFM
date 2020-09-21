using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{

    [SerializeField] protected AudioSource shoot;
    [SerializeField] protected float _timeBetweenShoots = 1.5f;
    [SerializeField] protected Transform _aimPoint;
    Animator _shotgunAnimator;
    [SerializeField] Animator _handAnimator;
    [SerializeField] AudioSource _emptyClip;
    [SerializeField] protected float _maxGunDistance;
    protected float _nextShootTime;


    public override void PushTrigger()
    {
        base.PushTrigger();
        LaunchProyectile();
    }

    private void Awake()
    {
        _shotgunAnimator = GetComponent<Animator>();
    }


    public override void StartReload()
    {
        //Sincronizo la animación de la mano con la animación de la escopeta en la recarga
        base.StartReload();
        if(_chargerCurrentAmmo < _chargerMaxAmmo &&
           _playerCurrentAmmo > 0)
        {
            if(_handAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f &&
                !_handAnimator.GetCurrentAnimatorStateInfo(0).IsName("HandAnimationShotgun") &&
                !_handAnimator.IsInTransition(0))
            {
                _handAnimator.SetTrigger("HandAnimationShotgun");
            }
  

            if (_shotgunAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f &&
                !_shotgunAnimator.GetCurrentAnimatorStateInfo(0).IsName("ShotgunReload") &&
                !_shotgunAnimator.IsInTransition(0))
            {
                _shotgunAnimator.SetTrigger("ShotgunReload");
            }

        }
        
    }

    private void LaunchProyectile()
    {
        if (_chargerCurrentAmmo == 0)
        {
            _emptyClip.Play();
            return;
        }


        if (_chargerCurrentAmmo > 0)
        {
            _chargerCurrentAmmo -= 1;

            _handAnimator.SetTrigger("ShotgunShoot");
            shoot.PlayOneShot(shoot.clip);
            OVRHaptics.Channels[1].Preempt(new OVRHapticsClip(shoot.clip, 0));
            

            if (_fireMuzzle != null)
            {
                _fireMuzzle.Play();
            }
            

            RaycastHit hit;
            //Raycast


            if (Physics.Raycast(_aimPoint.transform.position, _aimPoint.transform.forward, out hit, _maxGunDistance))
            {
                EnemyBehaviour _enemyB = hit.collider.GetComponentInParent<EnemyBehaviour>();
                EnemyHead head = hit.collider.GetComponent<EnemyHead>();
                EnemyMovement _enemyMov = hit.collider.GetComponent<EnemyMovement>();

                //Si le impacto en la cabeza lo aturdo y si es vulnerable además le hago daño
                if (head != null)
                {
                    head.HitByShotGun();

                    
                    _enemyB.TakeDamage(_damage);
                    
                    return;
                }
                //En el torso sólo le hago daño
                else if (_enemyB != null)
                {
                    _enemyB.TakeDamage(_damage);
                    return;
                }


                //Si le disparo le alerto
                else if (_enemyMov != null)
                {
                    _enemyMov.EnemyAlert();
                }
            }



        }



    }

}
