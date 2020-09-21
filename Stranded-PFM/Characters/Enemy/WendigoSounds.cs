using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WendigoSounds : MonoBehaviour {

    [SerializeField] AudioSource _idleSound;
    [SerializeField] AudioSource _roarSound;
    [SerializeField] AudioSource _deathSound;
    [SerializeField] AudioSource _chaseSound;
    
    //Métodos de gestión de los sonidos que provoca el enemigo
    
    public void StartIdleSound()
    {
        if (!_idleSound.isPlaying)
        {
            _idleSound.Play();
        }

    }

    public void StopIdleSound()
    {
        if (_idleSound.isPlaying)
        {
            _idleSound.Stop();
        }

    }


    public void StartRoarSound()
    {
        if (!_idleSound.isPlaying)
        {
            _roarSound.Play();
        }

    }

    public void StopRoarSound()
    {
        if (_idleSound.isPlaying)
        {
            _roarSound.Stop();
        }

    }

    public void StartDeathSound()
    {
        if (!_deathSound.isPlaying)
        {
            _deathSound.Play();
        }

    }

    public void StopDeathSound()
    {
        if (_deathSound.isPlaying)
        {
            _deathSound.Stop();
        }

    }


    public void StartChaseSound()
    {
        if (!_chaseSound.isPlaying)
        {
            _chaseSound.Play();
        }

    }

    public void StopChaseSound()
    {
        if (_chaseSound.isPlaying)
        {
            _chaseSound.Stop();
        }

    }
}
