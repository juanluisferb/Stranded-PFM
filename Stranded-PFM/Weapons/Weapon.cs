using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Weapon : MonoBehaviour {

    public enum Type
    {
        
        Gun, Shotgun
    }

#region Variables

    [SerializeField] Type _weaponType;
    [SerializeField] protected int _chargerMaxAmmo = 8;
    [SerializeField] protected int _playerInitialAmmo = 8;
    [SerializeField] protected int _playerMaxAmmo = 32;
    [SerializeField] protected ParticleSystem _fireMuzzle;
    [SerializeField] protected float _damage;

[SerializeField] AudioSource _reloadAudio;
    
    protected int _playerCurrentAmmo;
    protected int _chargerCurrentAmmo;
    protected bool _canUseWeapon;
    protected bool _triggerPushed = false;

    #endregion

#region Getters/Setters

    public int GetChargerCurrentAmmo() { return _chargerCurrentAmmo; }
    public int GetChargerMaxAmmo() { return _chargerMaxAmmo; }
    public bool GetCanUseWeapon() { return _canUseWeapon; }
    public int GetPlayerCurrentAmmo() { return _playerCurrentAmmo; }
    public int GetPlayerMaxAmmo() { return _playerMaxAmmo; }
    public Type GetWeaponType() { return _weaponType; }

    

    public void IncreasePlayerCurrentAmmo(int ammo)
    {
        _playerCurrentAmmo = Mathf.Min(_playerCurrentAmmo + ammo, _playerMaxAmmo);
    }

    public void SetCanUseWeapon(bool canUseWeapon)
    {
        _canUseWeapon = canUseWeapon;
    }

#endregion

    protected virtual void Start()
    {
        _chargerCurrentAmmo = _chargerMaxAmmo;
        _playerCurrentAmmo = _playerInitialAmmo;
    }

    //Si se está recargando no se puede disparar
    public virtual void PushTrigger()
    {
        if (IsInvoking("CompleteReload"))
        {
            CancelInvoke("CompleteReload");
            _reloadAudio.Stop();
        }
    }

    

    private void OnDisable()
    {
        CancelInvoke("CompleteReload");

    }

    //Métodos de recarga sincronizados con el sonido de la recarga
    public virtual void StartReload()
    {

        if (_chargerCurrentAmmo < _chargerMaxAmmo &&
            _playerCurrentAmmo > 0 &&
            !IsInvoking("CompleteReload"))
        {
            WeaponSound();
            Invoke("CompleteReload", _reloadAudio.clip.length);
            

        }
    }

    void CompleteReload()
    {

        int bulletsToReload = Mathf.Min(_chargerMaxAmmo - _chargerCurrentAmmo, _playerCurrentAmmo);
        _chargerCurrentAmmo += bulletsToReload;
        _playerCurrentAmmo -= bulletsToReload;
    }

    protected virtual void WeaponSound()
    {
        
        if(_reloadAudio != null) { _reloadAudio.Play(); }

         
    }
}
