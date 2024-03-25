using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CastleSO", menuName = "Data/CastleSO", order = 3)]

public class CastleSO : StatSO
{
    [Header("Castle Stats")]
    public float NuckbackPower;
    public int AttackCoolTime;
}
