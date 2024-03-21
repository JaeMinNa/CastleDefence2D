using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillStep
{
    First,
    Second,
    Third,
}

public enum SkillType
{
    Melee,
    Ranged,
}

[CreateAssetMenu(fileName = "SkillSO", menuName = "Data/SkillSO", order = 5)]
public class SkillSO : ScriptableObject
{
    [Header("Skill Info")]
    public string Tag;
    public SkillStep SkillStep;
    public SkillType SkillType;
    public float Atk;
    public float Speed;
    public float NuckbackPower;

    [Header("ETC")]
    public Sprite Icon;
    public GameObject SkillPrefab;
    public string ColliderName;
    public Vector3 Position;
}