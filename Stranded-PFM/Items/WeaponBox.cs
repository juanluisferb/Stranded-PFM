using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBox : GenericBox
{
    [SerializeField] Weapon.Type weaponType;
    [SerializeField] GameObject _UIAmmo;




    protected override void Update()
    {
    //Movimiento de item
        this.transform.Rotate(new Vector3(speedRotation * Time.deltaTime, 0, 0));

    }

    //Hereda de la clase padre
    protected override void ApplyBoxEffect(Player player)
    {
        //Boolean de control que solo se activa cuando el player encuentra la primera arma, sea la que sea
        if (!player._foundFirstWeapon)
        {
            player._foundFirstWeapon = true;
        }
        
        //Control de la mano derecha
        player._busyRightHand = true;
        player.GetWeapon(weaponType).SetCanUseWeapon(true);
        player.ActivateWeapon(weaponType);
        
        //Activo el UI del arma del player
        if (_UIAmmo != null && !_UIAmmo.activeInHierarchy)
        {
            _UIAmmo.SetActive(true);
        }
    }
}
