using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryRead : MonoBehaviour {

    public bool _isReadingDiary;

    
    //"Pausa" de leer el diario
    private void OnEnable()
    {
        _isReadingDiary = true;
    }

    private void OnDisable()
    {
        _isReadingDiary = false;
    }
}
