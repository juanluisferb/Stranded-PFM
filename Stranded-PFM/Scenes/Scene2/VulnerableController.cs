using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulnerableController : MonoBehaviour {

    [SerializeField] ParticleSystem _enemyAura;
    [SerializeField] EnemyBehaviour _enemy;
    [SerializeField] ParticleSystem _auraPentacle;
    [SerializeField] Animator _5TotemActivatedText;
    private bool _has5totems  = false;
    private int _numberOfTokens = 0;


    public void IncreaseNumberOfTokens() {

        _numberOfTokens += 1;

    }

    private void Update()
    {
        //Sólo compruebo una vez que tenga los 5 totems
        if (!_has5totems)
        {
            HasFiveTokens();
        }
        
    }

    //Controlador de los totems que hacen vulnerable al enemigo
    void HasFiveTokens()
    {
        if(_numberOfTokens >= 5)
        {
            _has5totems = true;
            _5TotemActivatedText.SetTrigger("5TotemActivatedText");
            _enemy.SetIsVulnerable(true);
            _enemyAura.gameObject.SetActive(false);
            _auraPentacle.Stop();

        }
    }


}
