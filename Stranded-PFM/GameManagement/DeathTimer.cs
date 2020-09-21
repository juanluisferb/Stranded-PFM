using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class DeathTimer : MonoBehaviour {

    float _Count;
    float _deathTimer = 25.0f;
    float _deadEnd;
    [SerializeField] OVRScreenFade _visionFade;
    [SerializeField] float _deathVignetteRate;
    [SerializeField] float _deathBloomRate;
    [SerializeField] float _rotationSpeed = 2.0f;
    [SerializeField] PostProcessVolume _postProcessVolume;
    [SerializeField] Transform _redSun;
    [SerializeField] AudioSource _backgroundMusic;
    [SerializeField] Animator _gameOverTextAnimator;

    
    private Vignette _vignetteValue;
    private Bloom _BloomValue;


    private void Start()
    {
        _Count = Time.time;
        _deadEnd = _Count + _deathTimer;
        Invoke("GameOverText", 3.0f);
    }


    //Métodos para controlar un timer en la pantalla de Game Over / Continue
    private void Update()
    {

        //Incremento valores de Viñeta y Bloom en el visor Oculus para dar tensión al jugador
        if(_Count >= _deadEnd)
        {
            _gameOverTextAnimator.SetTrigger("EndGameText");
            StartCoroutine(AudioController.FadeOut(_backgroundMusic, 2f));
            Invoke("LoadCompleteExitApplication", 6.0f);
        }
        else
        {
            //A los 25 segundos se pone el sol y el jugador muere de verdad
            _Count = Time.time;

            bool foundVignetteEffect = _postProcessVolume.profile.TryGetSettings<Vignette>(out _vignetteValue);
            bool foundBloomEffect = _postProcessVolume.profile.TryGetSettings<Bloom>(out _BloomValue);

            _vignetteValue.intensity.value += _deathVignetteRate * Time.deltaTime;
            _BloomValue.intensity.value -= _deathBloomRate * Time.deltaTime;


            _redSun.Rotate(-_rotationSpeed * Time.deltaTime, 0, 0);
        }

    }


    private void GameOverText()
    {
        _gameOverTextAnimator.SetTrigger("GameOverText");
    }

    private void LoadCompleteExitApplication()
    {
        _visionFade.FadeOut();
        Invoke("LoadNextScene", 2.0f);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("Scene1");
    }

}
