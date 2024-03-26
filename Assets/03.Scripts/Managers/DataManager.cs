using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public GameDataSO GameDataSO;

    private int _currentStageCoin;

    // 초기화
    public void Init()
    {
        _currentStageCoin = 0;
    }

    // 메모리 해제
    public void Release()
    {

    }

    public void CoinUpdate(int value)
    {
        _currentStageCoin += value;
    }
}
