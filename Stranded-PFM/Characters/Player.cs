using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

//Datos del jugador
    public class PlayerData
    {
        public Vector3 position;
        public int numberOfTotems;
        public Weapon[] weapons;

    }



public class Player : Character
{
    #region Serializables
    
    protected CharacterController controller;
    RaycastHit hit;
    SphereCollider noiseMaker;
    [SerializeField] SphereCollider _grabberR;
    [SerializeField] SphereCollider _grabberL;
    [SerializeField] OVRGrabber _LScript;
    [SerializeField] OVRGrabber _RScript;
    [SerializeField] Animator _RightHandAnimator;
    [SerializeField] Animator _LeftHandAnimator;
    [SerializeField] PostProcessVolume _postProcessVolume;
    [SerializeField] GameObject _torch;
    [SerializeField] float _vignetteIntensity;
    [SerializeField] float _smoothnessValue;
    [SerializeField] float _lerpValue;
    [SerializeField] AudioSource _damageSound;
    [SerializeField] GameObject _UIAmmo;
    [SerializeField] AudioSource _walkSound;
    [SerializeField] AudioSource _runSound;
    [SerializeField] AudioSource _deathSound;
    
    #endregion
    
    #region Variables

    Weapon[] _allWeapons;
    protected Weapon _activeWeapon;
    protected float TimeForChangeWeapon;
    protected float TimeBetweenWeapons = 0.4f;

    protected float TimeForFreeRHand;
    protected float TimeBetweenFreeRHand = 1f;

    protected int weaponIndex;
    protected bool activateTorch;
    protected float _fireRate = 0.25f;
    protected float _nextShootTime = 0;
    protected bool _canShoot;
    private Vignette _vignetteValue;
    private float _vignetteDefaultIntensityValue;
    private float _vignetteDefaultSmoothnessValue;
    private bool _IAmDead;
    public bool _busyRightHand { get; set; }
    public bool _foundFirstWeapon { get; set; }

    OVRScreenFade _visionFade;

    #endregion

    #region Getters
    
    //Getters comprobación arma activa y array de armas
    public Weapon GetCurrentWeapon() { return _activeWeapon; }
    public Weapon GetWeapon(Weapon.Type weaponType)
    {
        for (int i = 0; i < _allWeapons.Length; i++)
        {
            Weapon weapon = _allWeapons[i];
            if (weapon.GetWeaponType() == weaponType)
            {
                return weapon;
            }
        }

        return null;
    }


    #endregion

    

    protected override void Awake()
    {
        //Establecer variables por defecto e inicializar componentes
        base.Awake();
        _visionFade = GetComponentInChildren<OVRScreenFade>();
        controller = GetComponent<CharacterController>();
        noiseMaker = GetComponent<SphereCollider>();
        _allWeapons = GetComponentsInChildren<Weapon>();
        _IAmDead = false;
        _busyRightHand = false;
        _foundFirstWeapon = false;

    }

   

    protected override void Start()
    {
        //Ejecuto el start de character y añado las variables de player
        base.Start();

        for (int i = 0; i < _allWeapons.Length; i++)
        {
            _allWeapons[i].gameObject.SetActive(false);
        }

        _activeWeapon = _allWeapons[0];
        _activeWeapon.SetCanUseWeapon(false);
        activateTorch = false;
        
        //Desactivo la linterna al principio
        _LeftHandAnimator.SetBool("HasTorchlight", false); 
        //La mano puede coger objetos al principio
        _grabberR.enabled = true; 
        weaponIndex = 0;
        


    }

    

    void Update() 
    {
        //Método para controlar el trigger Sphere collider que puede atraer al monstruo.
        if (controller.isGrounded)
        {
            if (controller.velocity.magnitude > 0.1f)
            {

                if (noiseMaker.enabled == false)
                {
                    noiseMaker.enabled = true;
                }

                noiseMaker.radius = controller.velocity.magnitude;

                //Sonido de pasos en función de la velocidad se reproduce un sonido u otro
                if(controller.velocity.magnitude > 7.0f)
                {
                    if (_walkSound.isPlaying)
                    {
                        _walkSound.Stop();
                    }

                    else if (!_runSound.isPlaying)
                    {
                        _runSound.Play();
                    }
                }

                else if (controller.velocity.magnitude < 7.0f)
                {
                    if (_runSound.isPlaying)
                    {
                        _runSound.Stop();
                    }
                    
                    else if (!_walkSound.isPlaying)
                    {
                        _walkSound.Play();
                    }
                    
                    
                }
            }

            else
            {
                // Se para el sonido si se para el player
                noiseMaker.radius = 0.0f;
                noiseMaker.enabled = false;
                if (_walkSound.isPlaying)
                {
                    _walkSound.Stop();
                }
                else if (_runSound.isPlaying)
                {
                    _runSound.Stop();
                }
            }
 
        }

        //Se paran los sonidos al saltar
        else if (!controller.isGrounded)
        {
            if (_walkSound.isPlaying)
            {
                _walkSound.Stop();
            }
            else if (_runSound.isPlaying)
            {
                _runSound.Stop();
            }
        }

        BusyRightHand();

        //Sólo se activan si hay un arma en la mano derecha
        if (_busyRightHand)
        {
            WeaponInRightHand();
            ChooseActiveWeapon();
        }
        

        
        TorchLight();
        
        

    }


    #region PlayerFunctions

    //Método que activa/desactiva el arma en la mano derecha, controlo grabbers, animaciones, armas y UI de munición
    private void BusyRightHand()
    {
        if (OVRInput.Get(OVRInput.Button.Three) &&
                    Time.time > TimeForFreeRHand &&
                    _foundFirstWeapon)
        {
            TimeForFreeRHand = TimeBetweenFreeRHand + Time.time;


                if (!_busyRightHand)
                {
                    _busyRightHand = true;
                    _grabberR.enabled = false;
                    _activeWeapon.gameObject.SetActive(true);
                    _RScript.SetHasGun(true);
                    UpdateHandAnimation((int)_activeWeapon.GetWeaponType());
                    _UIAmmo.SetActive(true);
                }

                else if (_busyRightHand)
                {
                    _busyRightHand = false;
                    _grabberR.enabled = true;
                    _activeWeapon.gameObject.SetActive(false);
                    _RScript.SetHasGun(false);
                    _RightHandAnimator.SetBool("HasShotgun", false);
                    _RightHandAnimator.SetBool("HasGun", false);
                    _UIAmmo.SetActive(false);
                }
            
            
        }
    }

    //Método que mueve un index por el Array de 2 armas activando la que procede
    private void ChooseActiveWeapon()
    {
        //Controlo el rate para que no haga un cambio cada frame en el Update
        if (OVRInput.Get(OVRInput.Button.Four) &&
            Time.time > TimeForChangeWeapon &&
            _busyRightHand)
        {
            TimeForChangeWeapon = Time.time + TimeBetweenWeapons;

            if (_allWeapons.Length > 1)
            {
                weaponIndex += 1;

                if (weaponIndex >= 2)
                {
                    weaponIndex = 0;
                }

                if (weaponIndex >= 0 && weaponIndex <= 2)
                {
                    ActivateWeapon((Weapon.Type)weaponIndex);
                    _RScript.SetHasGun(true);

                }


            }
            
        }
  
    }

    //Actualizo la animación de la mano conforme al arma que lleva el personaje
    void UpdateHandAnimation(int weaponType)
    {
        int _weaponIndex = weaponType;
        switch (_weaponIndex)
        {
            case 0:
                if (_activeWeapon.isActiveAndEnabled)
                {
                    _RightHandAnimator.SetBool("HasShotgun", false);
                    _RightHandAnimator.SetBool("HasGun", true);
                    
                }
                break;

            case 1:
                if (_activeWeapon.isActiveAndEnabled)
                {
                    _RightHandAnimator.SetBool("HasGun", false);
                    _RightHandAnimator.SetBool("HasShotgun", true);
                }
                break;

            default:
                
                _RightHandAnimator.SetBool("HasShotgun", false);
                _RightHandAnimator.SetBool("HasGun", false);
                break;
        }
    }

    //Método que activa el nuevo arma en el Array y desactiva la anterior
    public void ActivateWeapon(Weapon.Type weaponType)
    {
        int weaponIndex = (int)weaponType;
        if (weaponIndex >= 0 && weaponIndex < _allWeapons.Length)
        {
            Weapon desiredWeapon = _allWeapons[weaponIndex];

            //Compruebo si el arma está desbloqueada
            if (desiredWeapon.GetCanUseWeapon())
            {

                //desactivo el arma activa
                _activeWeapon.gameObject.SetActive(false);

                //cambio el arma activa y la activo
                _activeWeapon = desiredWeapon;
                _activeWeapon.gameObject.SetActive(true);
                _grabberR.enabled = false;
                UpdateHandAnimation(weaponIndex);
            }


                
        }
    }

    //Métodos para disparar / recargar el arma activa
    private void WeaponInRightHand()
    {
        if (_activeWeapon.isActiveAndEnabled)
        {
            if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.5f)
            {
                if(Time.time > _nextShootTime &&
                    _canShoot == true)
                {
                    _nextShootTime = Time.time + _fireRate;
                    _activeWeapon.PushTrigger();
                    _canShoot = false;
                    
                    
                }
                

            }

            if(OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) < 0.1f)
            {
                _canShoot = true;
            }

            if (OVRInput.Get(OVRInput.Button.Two))
            {
                _activeWeapon.StartReload();
            }
        }
        
    }

    //Paro al personaje y lanzo el GameOver
    protected override void Die()
    {
        
        if (_IAmDead) return;

        _IAmDead = true;
        CharacterController _chara = GetComponent<CharacterController>();
        _chara.enabled = false;
        _deathSound.Play();
        _visionFade.FadeOut(); //Fundido en negro
        Invoke("CompleteLoadGameOver", 2.0f);

    }

    void CompleteLoadGameOver()
    {
        _gameManager.EndGame(false);
    }

    #endregion

    //Feedback de daño recibido al Player mediante postproceso de Viñeta (aro rojo visual en el visor de Oculus)
    public override void TakeDamage(float damage)
    {
        _postProcessVolume.profile.TryGetSettings<Vignette>(out _vignetteValue);
        _vignetteValue.color.value = Color.red;
        _vignetteDefaultIntensityValue = _vignetteValue.intensity.value;
        _vignetteDefaultSmoothnessValue = _vignetteValue.smoothness.value;

        _vignetteValue.intensity.value = _vignetteIntensity;
        _vignetteValue.smoothness.value = _smoothnessValue;
        _damageSound.Play(); //Sonido de daño

        base.TakeDamage(damage);
        Invoke("ReturnPostprocessSettings", 1.0f); //Devuelvo la visión a su estado inicial
    }

    

    //Método que activa / desactiva la linterna (desactiva / activa el OVRGrabber para no coger objetos con la mano ocupada)
    private void TorchLight()
    {
        if (OVRInput.GetDown(OVRInput.Touch.PrimaryThumbRest) && !_LScript.isGrabbing)
        {
            if (!activateTorch)
            {
                _torch.SetActive(true);
                activateTorch = true;
                _LeftHandAnimator.SetBool("HasTorchlight", true);
                _grabberL.enabled = false;

            }
            else if (activateTorch)
            {
                _torch.SetActive(false);
                activateTorch = false;
                _LeftHandAnimator.SetBool("HasTorchlight", false);
                _grabberL.enabled = true;

            }
        }

    }

    //Restablece los postprocesos después del daño
    private void ReturnPostprocessSettings()
    {
        _postProcessVolume.profile.TryGetSettings<Vignette>(out _vignetteValue);
        _vignetteValue.color.value = Color.black;
        _vignetteValue.intensity.value = _vignetteDefaultIntensityValue;
        _vignetteValue.smoothness.value = _vignetteDefaultSmoothnessValue;
    }
    
    
    
    /* Para implementar
    #region Save-Load
        //Método de carga
        public PlayerData GetPlayerData()
        {

        }

        //Método de guardado
        public void SetPlayerData(PlayerData playerData)
        {

        }

        #endregion
    */
}
