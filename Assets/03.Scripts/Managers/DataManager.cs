using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public GameDataSO GameDataSO;

    private int _currentStageCoin;

    // �ʱ�ȭ
    public void Init()
    {
        _currentStageCoin = 0;
    }

    // �޸� ����
    public void Release()
    {

    }

    public void CoinUpdate(int value)
    {
        _currentStageCoin += value;
    }
}
