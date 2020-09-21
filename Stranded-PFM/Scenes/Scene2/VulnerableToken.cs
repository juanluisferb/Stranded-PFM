using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulnerableToken : MonoBehaviour {

    
    [SerializeField] Material _fresnelMaterial;
    [SerializeField] VulnerableController _vulnerableController;
    [SerializeField] EnemyMovement _enemy;
    [SerializeField] GameObject _light;
    [SerializeField] AudioSource _thud;
    [SerializeField] ParticleSystem _fireTotem;

    MeshRenderer _rend;
    ParticleSystem _aura;
    bool _isActivated;

    private void Awake()
    {
        _rend = GetComponent<MeshRenderer>();
        _aura = GetComponentInChildren<ParticleSystem>();
        _isActivated = false;
        
    }

    //El totem se activa y suma al controlador de vulnerabilidad
    //El enemigo va al totem que se acaba de activar para sorprender al player si sigue en la zona.
    //El enemigo tiene mayor visión
    private void OnTriggerEnter(Collider other)
    {

            if (other.CompareTag("Hands"))
            {
                if (!_isActivated)
                {
                    _isActivated = true;
                    _thud.PlayOneShot(_thud.clip);
                    if (!_aura.isPlaying)
                    {
                    _aura.Play();
                    }

                _light.gameObject.SetActive(true);
                _rend.material = _fresnelMaterial;
                _fireTotem.Play();
                _vulnerableController.IncreaseNumberOfTokens();
                _enemy.SetMinDistanceSight();
                _enemy._agent.SetDestination(this.transform.position);
                }
               
            }

        
    }

}
