using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    // GameManager은 Manager을 관리하는 하나의 역할만 함
    public PlayerManager PlayerManager { get; private set; }
    public ScenesManager ScenesManager { get; private set; }
    public ObjectPoolManager ObjectPoolManager { get; private set; }
    public DataManager DataManager { get; private set; }
    public SoundManager SoundManager { get; private set; }

    public static GameManager I;

    private void Awake()
    {
        if (I == null)
        {
            I = this;
            //DontDestroyOnLoad(gameObject);
        }
        //else
        //{
        //    Destroy(gameObject);
        //}

        PlayerManager = GetComponentInChildren<PlayerManager>();
        ScenesManager = GetComponentInChildren<ScenesManager>();
        ObjectPoolManager = GetComponentInChildren<ObjectPoolManager>();
        DataManager = GetComponentInChildren<DataManager>();
        SoundManager = GetComponentInChildren<SoundManager>();

        Init();
    }

    private void Init()
    {
        PlayerManager.Init();
        ScenesManager.Init();
        ObjectPoolManager.Init();
        DataManager.Init();
        SoundManager.Init();
    }

    private void Release()
    {
        PlayerManager.Release();
        ScenesManager.Release();
        ObjectPoolManager.Release();
        DataManager.Release();
        SoundManager.Release();
    }
}