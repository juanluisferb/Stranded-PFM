using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : Character {

    Animator _anim;
    EnemyMovement _enemy;
    [SerializeField] GameObject _endGameKeyInstancer;
    [SerializeField] AudioSource _chaseMusic;
    [SerializeField] PauseGame _paused;
    [SerializeField] DiaryRead _isReadingDiary;
    [SerializeField] GameObject _LClaw;
    [SerializeField] GameObject _RClaw;
    [SerializeField] AudioSource _backgroundMusic;
    [SerializeField] Animator _deathText;

    protected override void Awake()
    {
        base.Awake();
        _anim = GetComponent<Animator>();
        _enemy = GetComponent<EnemyMovement>();
        
    }

    protected override void Start () {
        base.Start();
	}

    //El enemigo no sufre daño cuando el juego está en pausa o el player lee un diario
    public override void TakeDamage(float damage)
    {
        if (_paused._paused) return;
        if (_isReadingDiary._isReadingDiary) return;

        if (GetIsVulnerable())
        {
            base.TakeDamage(damage);
        }
        
    }

    protected override void Die()
    {
        //Boolean de control para que no se muera varias veces
        if (_enemy.bIsDead) return;

        //Paro la música de persecución
        if (_chaseMusic.isPlaying)
        {
            StartCoroutine(AudioController.FadeOut(_chaseMusic, 5f));
            StartCoroutine(AudioController.FadeIn(_backgroundMusic, 5f));
        }

        //Desactivo las garras en caso de que la animación se haya cortado y sigan activas
        _anim.SetBool("bDeath", true);
        if (_LClaw.activeInHierarchy)
        {
            _LClaw.SetActive(false);
        }

        if (_RClaw.activeInHierarchy)
        {
            _RClaw.SetActive(false);
        }

        _enemy.bIsDead = true;
        
        Invoke("SpawnEndGameKey", 0.1f);

    }

    //Spawneo la llave para ir al final del nivel
    private void SpawnEndGameKey()
    {
        _deathText.SetTrigger("WendigoDeathText");
        _endGameKeyInstancer.SetActive(true);
    }
 
}
