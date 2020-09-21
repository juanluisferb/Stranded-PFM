using UnityEngine;
using System.Collections;

public class FlickeringLights : MonoBehaviour
{

    [SerializeField] float totalSeconds;     
    [SerializeField] float maxIntensity;     
    [SerializeField] Light myLight1;
    [SerializeField] Light myLight2;   


    // Luces del coche parpadeando
    public IEnumerator flashNow()
    {
        float waitTime = totalSeconds / 2;
        // Mitad de los segundos encendido y mitad apagados

        while (myLight1.intensity < maxIntensity)
        {
            myLight1.intensity += Time.deltaTime / waitTime;
            myLight2.intensity += Time.deltaTime / waitTime; // Increase intensity
            yield return null;
        }
        while (myLight1.intensity > 0)
        {
            myLight1.intensity -= Time.deltaTime / waitTime;
            myLight2.intensity -= Time.deltaTime / waitTime; //Decrease intensity
            yield return null;
        }
        yield return null;
    }
}
