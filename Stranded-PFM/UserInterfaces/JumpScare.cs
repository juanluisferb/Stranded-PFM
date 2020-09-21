using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare: MonoBehaviour {

    [SerializeField] OVRScreenFade _visionFade;
    GameManager _gm;

    private void Awake()
    {
        _gm = FindObjectOfType<GameManager>();
    }

    //Fundido en negro y carga de créditos finales
    public void Fade()
    {
        _visionFade.FadeOut();
        Invoke("LoadCredits", 2.0f);

    }

    private void LoadCredits() {

        _gm.EndGame(true);
    }
}
