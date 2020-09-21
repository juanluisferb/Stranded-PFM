using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameKeys : MonoBehaviour {

    EndGameHouse _scene;
    [SerializeField] Material[] _newMaterials;
    Material[] _defaultMaterials;
    MeshRenderer _rend;
    ParticleSystem _ps;

    private void Awake()
    {
        _rend = GetComponent<MeshRenderer>();
        _defaultMaterials = _rend.materials;
        _ps = GetComponentInChildren<ParticleSystem>();
        _scene = FindObjectOfType<EndGameHouse>();

    }

    //Cambio material al tener la llave y paro el aura de partículas
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hands"))
        {
            _rend.materials = _newMaterials;
            _scene.EndGameKey = true;
            _ps.Stop();
        }


    }

    //Al soltar la llave se vuelve al material Default y se reactiva el sistema de particulas
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hands"))
        {
            _rend.materials = _defaultMaterials;
            _scene.EndGameKey = false;
            _ps.Play();
        }
    }
}
