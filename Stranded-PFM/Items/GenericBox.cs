using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericBox : MonoBehaviour {

    protected float speedRotation = 20;
    [SerializeField] Material[] _newMaterials;
    Material[] _defaultMaterials;
    MeshRenderer _rend;

    //Clase padre de los items
    private void Awake()
    {
        //Guardo el material del objeto para restablecerlo más tarde
        _rend = GetComponent<MeshRenderer>();
        _defaultMaterials = _rend.materials;
        
    }

    protected virtual void Update()
    {
        //Movimiento de item
        this.transform.Rotate(new Vector3(0, 0, -speedRotation * Time.deltaTime));
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hands"))
        {
            //Cambio material a un fresnel
            _rend.materials = _newMaterials;

            //Los items solamente se activan con el player
            if ((OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.5f ||
            OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.5f))
            {
                Player player = other.GetComponentInParent<Player>();

                if (player != null)
                {
                    ApplyBoxEffect(player);

                    Destroy(this.gameObject);
                }

            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hands"))
        {
            //Restablezco los materiales
            _rend.materials = _defaultMaterials;

        }
    }

    protected abstract void ApplyBoxEffect(Player player); //Implementado en clases hijas
