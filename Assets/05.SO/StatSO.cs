using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatSO_", menuName = "Data/StatSO", order = 0)]
public class StatSO : ScriptableObject
{
    [Header("Common Stats")]
    public float Speed;
    public float Atk;
    public float Hp;
}
