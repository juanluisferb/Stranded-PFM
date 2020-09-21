using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    bool _isVictory = false;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    //En función del boolean que se parametrice en la llamada al método, se carga una u otra escena de fin de juego
    public void EndGame(bool victory)
    {
        _isVictory = victory;

        if (_isVictory)
        {

            SceneManager.LoadScene("Credits");
        }
        if (!_isVictory)
        {

            SceneManager.LoadScene("GameOver");
        }

    }

    public bool GetIsVictory()
    {
        return _isVictory;
    }
    
    //---------------------------------------------------//
    
    #region DataManagement
    
    const string KEY_PLAYER_DATA = "KEY_PLAYER_DATA";


    public bool HasSavedGame()
    {
        return PlayerPrefs.HasKey(KEY_PLAYER_DATA);
    }

    
    public void SaveGame()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();

        //Obtiene los datos del player 
        PlayerData playerData = player.GetPlayerData();
        string jsonPlayerData = JsonUtility.ToJson(playerData);

        //Los guarda en el JSON
        PlayerPrefs.SetString(KEY_PLAYER_DATA, jsonPlayerData);
        PlayerPrefs.Save();


    }

    //Load de los datos del player desde el fichero JSON
    public void LoadGame()
    {

        if (PlayerPrefs.HasKey(KEY_PLAYER_DATA))
        {
            string jsonPlayerData = PlayerPrefs.GetString(KEY_PLAYER_DATA);

            PlayerData playerData = JsonUtility.FromJson<PlayerData>(jsonPlayerData);
            Player player = GameObject.Find("Player").GetComponent<Player>();

            //Para implementar
            player.SetPlayerData(playerData);
        }


    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteKey(KEY_PLAYER_DATA);

    }

    #endregion
}
