using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instancer : MonoBehaviour {

	
    [SerializeField] GameObject _endGameKey;
    //Objeto que instancia la llave para acceder al final del juego
    void Start () {
        Invoke("Instance", 10.0f);
	}
	

    void Instance()
    {
        Instantiate(_endGameKey, this.transform.position, Quaternion.identity);
    }
	
}
