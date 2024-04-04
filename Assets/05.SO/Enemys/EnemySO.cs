using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    Melee,
    Ranged,
}

[CreateAssetMenu(fileName = "EnemySO", menuName = "Data/EnemySO", order = 2)]

public class EnemySO : StatSO
{
    [Header("Enemy Stats")]
    public Type AttackType;
    public int Price;
    public int CoinDropPercent;
    public int PotionDropPercent;

    [Header("Ranged Enemy Info")]
    public string BulletTag;
    public float BulletSpeed;
    public float Distance;
    public Vector3 BulletPosition;
}

