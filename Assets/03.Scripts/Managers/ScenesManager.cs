using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public string CurrentSceneName;

    // 초기화
    public void Init()
    {
        CurrentSceneName = SceneManager.GetActiveScene().name;

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
}
