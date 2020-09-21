using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diary : GenericBox {

    [SerializeField] GameObject _diariesPanel;
    [SerializeField] Text _diaryText;

    //Activación de texto de los diarios en el HUD
    protected override void ApplyBoxEffect(Player player)
    {
        _diariesPanel.SetActive(true);
        _diaryText.gameObject.SetActive(true);


    }

}
