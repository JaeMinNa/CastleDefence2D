using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatData_", menuName = "Data/StatData", order = 0)]
public class StatData : ScriptableObject
{
    public float Speed;
    public int Atk;
    public int Hp;
}
