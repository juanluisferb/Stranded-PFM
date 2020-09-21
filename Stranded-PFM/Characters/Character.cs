using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Character : MonoBehaviour {

    #region Variables
        
    [SerializeField] protected float _maxLife = 100;
    protected float _currentLife;
    private bool _bIsVulnerable = false;
    protected GameManager _gameManager;
    
    #endregion

    protected virtual void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    protected virtual void Start()
    {
        _currentLife = _maxLife;
    }
    
    #region Getters-setters
        
    public void SetIsVulnerable(bool _nowVulnerable)
    {
        _bIsVulnerable = _nowVulnerable;
    }
    
    #endregion

        
    public bool GetIsVulnerable() { return _bIsVulnerable; }
    public float GetMaxLife() { return _maxLife; }
    public float GetCurrentLife() { return _currentLife; }
    
    
    
    //Método usado por el Enemy y por el Player para recibir daño
    public virtual void TakeDamage(float damage)
    {
        damage = Mathf.Abs(damage); //check
        _currentLife = Mathf.Max(_currentLife - damage, 0);

        if(_currentLife == 0)
        {
            Die();
        }
    }

    
    
    //Método para curar al Player con los botiquines
    public virtual void RestoreLife(float health)
    {
        health = Mathf.Abs(health); //check
        _currentLife = Mathf.Min(_currentLife + health, _maxLife);
        
    }

    
    
    protected virtual void Die()
    {
     //Implementado en Player y EnemyBehaviour  

    }
}
