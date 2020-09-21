using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameKeys : MonoBehaviour {

    [SerializeField] StartInterval _scene;
    [SerializeField] Material[] _newMaterials;
    Material[] _defaultMaterials;
    MeshRenderer _rend;

    private void Awake()
    {
        _rend = GetComponent<MeshRenderer>();
        _defaultMaterials = _rend.materials;



    }

    //Llave que vuelve a true la posibilidad de saltar a la cinemática si se coge
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hands"))
        {
            //Cambio de material para resaltar el objeto
            _rend.materials = _newMaterials;
            _scene.StartGameKey = true;

        }

        
    }

    //Método para cuando se suelta la llave
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hands"))
        {
            _rend.materials = _defaultMaterials;
            _scene.StartGameKey = false;

        }
    }





}
