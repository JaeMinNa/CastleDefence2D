using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public GameDataSO GameDataSO;
    public int CurrentStageCoin;

    // �ʱ�ȭ
    public void Init()
    {
        CurrentStageCoin = 0;
    }

    // �޸� ����
    public void Release()
    {

    }

    public void CoinUpdate(int value)
    {
        CurrentStageCoin += value;
    }
}
