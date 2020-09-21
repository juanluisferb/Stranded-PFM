using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene2 : MonoBehaviour {

    [SerializeField] OVRScreenFade _visionFade;

    //Trigger en la cinemática Interval
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _visionFade.FadeOut();
            Invoke("CompleteLoad", 2.0f);
        }
        
    }


    private void CompleteLoad()
    {
        SceneManager.LoadScene("Scene2");
    }
}
