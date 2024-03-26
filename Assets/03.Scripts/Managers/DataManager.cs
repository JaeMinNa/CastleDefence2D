using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public GameDataSO GameDataSO;
    public int CurrentStageCoin;

    // 초기화
    public void Init()
    {
        CurrentStageCoin = 0;
    }

    // 메모리 해제
    public void Release()
    {

    }

    public void CoinUpdate(int value)
    {
        CurrentStageCoin += value;
    }
}
