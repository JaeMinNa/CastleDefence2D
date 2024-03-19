using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemySO", menuName = "Data/EnemySO", order = 2)]

public class EnemySO : StatSO
{
    [Header("Enemy Stats")]
    public int Price;
    public int CoinDropPercent;
}

