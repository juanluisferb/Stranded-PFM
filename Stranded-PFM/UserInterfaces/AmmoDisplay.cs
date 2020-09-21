using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay: MonoBehaviour {

    #region Variables
    Weapon _activeWeapon;
    [SerializeField] Text _displayAmmo;
    [SerializeField] Player player;

    Weapon _lastWeapon;
    int _lastCurrentAmmo;
    int _lastPlayerAmmo;

    #endregion

    private void Awake()
    {
        
        player = GameObject.Find("OVRPlayerController").GetComponent<Player>();
    }

    //Script para mostrar en tiempo real la munición en función del arma activa
    void Update () {
        _activeWeapon = player.GetCurrentWeapon();

        if (_lastWeapon != _activeWeapon || 
            _lastPlayerAmmo != _activeWeapon.GetPlayerCurrentAmmo() ||
            _lastCurrentAmmo != _activeWeapon.GetChargerCurrentAmmo())
        {
            _displayAmmo.text = _activeWeapon.GetChargerCurrentAmmo() + " / " + _activeWeapon.GetPlayerCurrentAmmo();

           

            _lastCurrentAmmo = _activeWeapon.GetChargerCurrentAmmo();
            _lastPlayerAmmo = _activeWeapon.GetPlayerCurrentAmmo();
            _lastWeapon = _activeWeapon;
        }
    }
}
