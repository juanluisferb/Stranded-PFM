using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageDoor : MonoBehaviour {

    [SerializeField] Animator _garageAnimator;
    private bool isOpened = false;

    //Activación de la animación de la puerta
    private void OnTriggerStay(Collider other)
    {
        if (!isOpened)
        {
            if (other.CompareTag("Hands") &&
            (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.5f ||
            OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.5f))
            {
                if (_garageAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f &&
                    !_garageAnimator.GetCurrentAnimatorStateInfo(0).IsName("OpenDoor") &&
                    !_garageAnimator.IsInTransition(0))
                {
                    _garageAnimator.SetTrigger("OpenDoor");
                    isOpened = true;
                }
            }
        }
        else
        {
            return;
        }
        
    }

    
}
