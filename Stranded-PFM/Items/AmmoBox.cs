using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : GenericBox {

    [SerializeField] Player player;
    [SerializeField] Weapon.Type _weaponType;
    [SerializeField] int _ammo = 16;

    protected override void ApplyBoxEffect(Player player)
    {
        //Incrementa la munición del tipo establecido
        player.GetWeapon(_weaponType).IncreasePlayerCurrentAmmo(_ammo);

    }
}
