using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDataSO", menuName = "Data/GameDataSO", order = 4)]
public class GameDataSO : ScriptableObject
{
    [Header("GameData")]
    public int Stage;
    public int Coin;

    [Header("Enemys")]
    public List<EnemySO> MeleeEnemy;
    public List<EnemySO> RangedEnemy;
}
