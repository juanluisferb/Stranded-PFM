using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawsBehaviour : MonoBehaviour {

    [SerializeField] GameObject LClaw;
    [SerializeField] GameObject RClaw;
   
    //Métodos para activar/desactivar las garras del enemigo en los animation events
    public void ActivateLeftClaw()
    {

        LClaw.SetActive(true);
        
        
    }

    public void ActivateRightClaw()
    {

        RClaw.SetActive(true);

    }

    public void DeactivateLeftClaw()
    {
        LClaw.SetActive(false);
            
    } 

    public void DeactivateRightClaw()
    {

        RClaw.SetActive(false);
            

    }

    public void DeactivateBothClaws()
    {
        if (LClaw.activeInHierarchy)
        {
            LClaw.SetActive(false);
        }

        if (RClaw.activeInHierarchy)
        {
            RClaw.SetActive(false);
        }
    }
}
