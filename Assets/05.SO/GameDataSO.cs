using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDataSO", menuName = "Data/GameDataSO", order = 4)]
public class GameDataSO : ScriptableObject
{
    public int Stage;
    public int Coin;
}
