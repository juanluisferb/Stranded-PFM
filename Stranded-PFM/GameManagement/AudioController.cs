using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioController
{

    //Corrutina usada de forma global para parar implementar sonidos en la escena de forma gradual
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }

        if(audioSource.volume < 0.05f)
        {
            audioSource.Stop();
 
        }
        
    }
    //Corrutina usada de forma global para parar quitar sonidos de la escena de forma gradual
    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        audioSource.Play();
        audioSource.volume = 0f;
        while (audioSource.volume < 0.4f)
        {
            audioSource.volume += Time.deltaTime / FadeTime;
            yield return null;
        }
    }
}
