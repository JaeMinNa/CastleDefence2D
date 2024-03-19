using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // GameManager�� Manager�� �����ϴ� �ϳ��� ���Ҹ� ��
    public PlayerManager PlayerManager { get; private set; }
    public ScenesManager ScenesManager { get; private set; }

    public static GameManager I;

    private void Awake()
    {
        if (I == null)
        {
            I = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        PlayerManager = GetComponentInChildren<PlayerManager>();
        ScenesManager = GetComponentInChildren<ScenesManager>();

        Init();
    }

    private void Init()
    {
        PlayerManager.Init();
        ScenesManager.Init();
    }

    private void Release()
    {
        PlayerManager.Release();
        ScenesManager.Release();
    }
}