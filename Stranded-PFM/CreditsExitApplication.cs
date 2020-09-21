using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsExitApplication : MonoBehaviour {

    //Carga la escena 1 
    public void ExitAppFromCredits()
    {
        SceneManager.LoadScene("Scene1");
    }
}
