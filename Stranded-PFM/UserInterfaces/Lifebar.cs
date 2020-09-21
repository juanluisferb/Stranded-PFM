using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lifebar : MonoBehaviour {

    [SerializeField] Image _barForeground;
    [SerializeField] Character _character;

    private float lifePercent = 0;

    private void Awake()
    {
        if (_character == null)
        {
            _character = GetComponentInParent<Character>();
        }
    }

    //Muestra en tiempo real la vida del Player
    void Update () {
        lifePercent = _character.GetCurrentLife() / _character.GetMaxLife();
        _barForeground.fillAmount = lifePercent;
    }
}
