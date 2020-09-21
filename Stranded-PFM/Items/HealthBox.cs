using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBox : GenericBox {

    [SerializeField] float _healpoints = 20;

    //Método para curar al player
    protected override void ApplyBoxEffect(Player player)
    {
        if(player.GetCurrentLife() < player.GetMaxLife())
        {
            player.RestoreLife(_healpoints);
            
        }
        
    }
}
