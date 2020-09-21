using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    enum EState { Patrol, Pursuit, Attacking }

    [Header("Patrol")]
    public Transform[] _patrolPoints;
    [SerializeField]float ThresholdDistance;

    [Header("Velocities")]
    [SerializeField]float SpeedMovementPatrol;
    [SerializeField]float SpeedWalkingFlee;
    [SerializeField]float SpeedRunningFlee;

    [Header("Attack")]
    [SerializeField]float RateAttack;
    private float _nextTimeAttack;

    [Header("Combat")]
    [SerializeField]Player _player;
    [SerializeField]float _minDistanceAttack;
    [SerializeField]float _minDistanceSight;
    [SerializeField]float _minAngleDetection;
    [SerializeField]Transform _viewPoint;
    [SerializeField]Projector _viewConeProjector;
    [SerializeField] float rotationSpeed;
    [SerializeField] Transform _head;
    [SerializeField] AudioSource _chaseMusic;
    [SerializeField] AudioSource _backgroundMusic;

    [SerializeField] PauseGame _paused;
    [SerializeField] DiaryRead _isreadingDiary;

    EState _currentState;
    public int _currentPathIndex;
    Animator _animator;
    public NavMeshAgent _agent; //Se accede desde otros scripts
    bool _bIsValid;
    RaycastHit[] OutRaycastHit;
    bool _bCanAttack;
    public bool _hasSeenPlayer;
    bool _isPlayingCombatMusic;
    bool _isPlayingBackgroundMusic;

    Vector3 _lastPlayerPosition;

    public bool bIsDead { get; set; }
    public bool _bIsIdling { get; set; }
    public bool bIsVisible { get; set; }

    //Método para ampliar la visión del enemigo y aumentar la dificultad del juego	
    public void SetMinDistanceSight()
    {
        _minDistanceSight *= 1.45f;
    }


    float CurrentVelocity
    {
        get
        {
            switch(_currentState)
            {
                case EState.Patrol: return SpeedMovementPatrol;

                case EState.Pursuit: return SpeedRunningFlee;

                default:
                return SpeedWalkingFlee;
            }
        }
    }

	void Awake() 
    {
	//Inicializo variables y componentes
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _bIsValid = _patrolPoints != null && _patrolPoints.Length > 0;
        _bCanAttack = true;
        _bIsIdling = false;
        bIsDead = false;
        _hasSeenPlayer = false;
        _isPlayingCombatMusic = false;
        _isPlayingBackgroundMusic = true;
        
        _currentState = EState.Patrol;
        OutRaycastHit = new RaycastHit[5];
    }

    protected void Start() 
    {
        _agent.SetDestination(_patrolPoints[_currentPathIndex].position);


    }

    void FixedUpdate() 
    {
        
        //Si está muerto, el monstruo se para
        if (bIsDead)
        {
            _agent.velocity = Vector3.zero;
            return;
        }

        //Si el jugador está leyendo un diario o con el juego en pausa, el monstruo también se para
        if (_isreadingDiary._isReadingDiary)
        {
            _agent.velocity = Vector3.zero;
            return;
        }


        if (_paused._paused)
        {
            _agent.velocity = Vector3.zero;
            return;
        }
       
        
        _animator.SetFloat("SpeedMovement", _agent.velocity.magnitude);

        _agent.speed = CurrentVelocity;

        _agent.isStopped = !_bCanAttack; 
        
        
        //Si el enemigo está en un punto de patrulla, se inmoviliza
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 &&
                             (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") &&
                             _bIsIdling))
        {
            _agent.velocity = Vector3.zero;
        }


        if (_bIsValid)
        {
            bool bPursuitPlayer = false;

            //Calculo la distancia al player
            Vector3 directionToPlayer = _player.transform.position - _viewPoint.position;
            if(directionToPlayer.magnitude < _minDistanceSight)
            {
                Vector3 directionToPlayerClamp = directionToPlayer;
                //Calculo el ángulo al player
                if(Vector3.Angle(transform.forward, directionToPlayer) < _minAngleDetection * 2)
                {
                    int hits = Physics.RaycastNonAlloc(_viewPoint.position, directionToPlayerClamp, OutRaycastHit, _minDistanceSight);


                    for(int i = 0; i < hits; i++)
                    {
                        //Si es un obstáculo, ignora
                        RaycastHit target = OutRaycastHit[i];
                        if(target.transform.CompareTag("Occluder"))
                        {
                            break;
                        }

                        //Si es el player, ataca y persigue
                        if(target.transform.CompareTag("Player") ||
                            target.transform.CompareTag("Hands"))
                        {

                            _head.LookAt(_player.transform);
                            _hasSeenPlayer = true;
                            _animator.SetBool("bCombatMode", true);

                            if (_animator.GetBool("bCombatMode") &&
                                !_isPlayingCombatMusic)
                            {
                                _isPlayingBackgroundMusic = false;
                                _isPlayingCombatMusic = true;
                                StartCoroutine(AudioController.FadeIn(_chaseMusic, 1f));
                            }

                            //Ataca
                            if (directionToPlayer.magnitude < _minDistanceAttack)
                            {
                                RotateTowards(_player.transform);

                                Attack();
                                _currentState = EState.Attacking;

                            }
                            //Persigue (no está dentro de la distancia de atacar)
                            else if (_bCanAttack)
                            {
                                bPursuitPlayer = true;
                                _currentState = EState.Pursuit;
                                _agent.SetDestination(_player.transform.position);
                                _lastPlayerPosition = _player.transform.position;
                            }

                            _animator.SetBool("bCombatMode", true);
                            RotateTowards(_player.transform);
                            return;

                        }
                    }
                }
            }
	    //Vuelve al estado de patrulla
            _currentState = EState.Patrol;
            _animator.SetBool("bCombatMode", false);

            

            //Va al último punto en el que ha visto al jugador y hace una animación Idle ahí (en rango de 3m)
            if (_hasSeenPlayer)
            {

                if(Vector3.Distance(_agent.nextPosition,_lastPlayerPosition) < 3.0f)
                {
                    _hasSeenPlayer = false;
                    _animator.SetTrigger("Idle");  


                }
                else
                {
                    //Si no ha llegado, sigue andando
                    _agent.SetDestination(_lastPlayerPosition);
                }

            }
            
            // Si no lo ha visto, patrulla
            if(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f &&
                _animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                if (!bPursuitPlayer)
                {
                    //Si no ha visto al Player en el punto _lastPlayerPosition, se para la música de persecución y entra la de fondo, y continua su patrulla
                    if(!_hasSeenPlayer)
                    {
                        if (!_animator.GetBool("bCombatMode") && 
                            !_isPlayingBackgroundMusic)
                        {
                            _isPlayingBackgroundMusic = true;
                            _isPlayingCombatMusic = false;
                            StartCoroutine(AudioController.FadeOut(_chaseMusic, 1f));
                            StartCoroutine(AudioController.FadeIn(_backgroundMusic, 1f));
                        }

                        _agent.SetDestination(_patrolPoints[_currentPathIndex].position);
                        Patrol();
                    }
                   

                }
            }
            


        }
    }


    //Giro rápido para encarar al jugador si éste está en rango
    private void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    //Función de patrulla por el array de posiciones
    public void Patrol()
    {
        _bCanAttack = true;
        float distance = Vector3.Distance(_patrolPoints[_currentPathIndex].position, transform.position);
        bool bInThreshold = distance < ThresholdDistance + _agent.radius + _agent.stoppingDistance;

	//Si está en uno de los puntos, se para y realiza una animación de Idle        
        if (bInThreshold)
        {
            if (!_bIsIdling)
            {
                _animator.SetTrigger("Idle");
                _bIsIdling = true;
            }

            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f &&
                (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || 
                _animator.GetCurrentAnimatorStateInfo(0).IsName("Stun")))
            {

                _currentPathIndex = (_currentPathIndex + 1) % _patrolPoints.Length;
                _agent.SetDestination(_patrolPoints[_currentPathIndex].position);
                _bIsIdling = false;
            }

        }
    }

  


    void Update() 
    {
        //Update attack
        _bCanAttack = Time.time > _nextTimeAttack;

        //Update projector de debugeo (
        _viewConeProjector.orthographicSize = _minDistanceSight;
        _viewConeProjector.material.SetFloat("_Angle", _minAngleDetection);

        

    }

    //Detección del ruido que hace el jugador
    private void OnTriggerStay(Collider other)
    {
        //El monstruo no va al jugador si se lanza la pausa o algún diario
        if (bIsDead) return;
        if (_isreadingDiary._isReadingDiary) return;
        if (_paused._paused) return; 

        if (other.CompareTag("Player"))
        {
            _currentState = EState.Pursuit;
            _agent.SetDestination(_player.transform.position);
        }
    }

    
    //Alerta al enemigo si es disparado
    public void EnemyAlert()
    {
        //Controlo muerte, pausa y diarios
        if (bIsDead ||
            _paused._paused ||
            _isreadingDiary._isReadingDiary ||
            _animator.GetBool("bCombatmode")) return; 

        RotateTowards(_player.transform);
        _currentState = EState.Pursuit;
        _agent.SetDestination(_player.transform.position);

    }

    private void Attack() 
    {
        //Ataca al jugador de forma aleatoria con animación de brazo derecho o de brazo izquierdo
        if (_bCanAttack)
        {
            _agent.velocity = Vector3.zero;
            _nextTimeAttack = Time.time + RateAttack;

            int prob = Random.Range(0, 2);


            if(prob == 0)
            {
                _animator.SetTrigger("Attack1");
            }
            if(prob == 1)
            {
                _animator.SetTrigger("Attack2");
            }



        }
    }
}
