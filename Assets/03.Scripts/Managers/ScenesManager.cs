using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public string CurrentSceneName;
    private GameData _gameData;

    // 초기화
    public void Init()
    {
        CurrentSceneName = SceneManager.GetActiveScene().name;
        _gameData = GameManager.I.DataManager.GameData;

        if (CurrentSceneName == "BattleScene0")
        {
            GameObject playerPrefab = Instantiate(GameManager.I.PlayerManager.PlayerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            GameManager.I.PlayerManager.Player = playerPrefab;
        }
    }

    // 메모리 해제
    public void Release()
    {

    }

    private void Start()
    {
        if (CurrentSceneName == "LobyScene" && PlayerPrefs.GetInt("IsTutorial") == 0)
        {
            //_gameData.IsTutorial = true;
            PlayerPrefs.SetInt("IsTutorial", 1);
            SceneMove("BattleScene0");
        }
    }

    public void SceneMove(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
