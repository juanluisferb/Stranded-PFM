using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DiaryPanelContinueButton : MonoBehaviour {

    [SerializeField] Text[] _diaries;
    [SerializeField] GameObject _diaryPanel;

    //Botón para desactivar con la mano el panel del diario
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hands"))
        {

            for( int i=0; i<_diaries.Length; i++)
            {
                if (_diaries[i].gameObject.activeSelf)
                {
                    _diaries[i].gameObject.SetActive(false);
                }
                
            }
            _diaryPanel.SetActive(false);

}
    }
}
