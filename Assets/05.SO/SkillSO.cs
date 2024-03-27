using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum SkillStep
//{
//    First,
//    Second,
//    Third,
//}

//public enum SkillType
//{
//    Melee,
//    Ranged,
//}

[CreateAssetMenu(fileName = "SkillSO", menuName = "Data/SkillSO", order = 5)]
public class SkillSO : ScriptableObject
{
    [Header("Common Skill Info")]
    public string Tag;
    //public SkillStep SkillStep;
    //public SkillType SkillType;
    public float AtkRatio;
    public float NuckbackPower;
    public float Speed;
    public float ExplosionRange;

    [Header("AreaSkill Info")]
    public float Range;
    public int Count;
    public float Interval;

    [Header("ETC")]
    public string SkillExplosionTag;
    public Sprite Icon;
    public GameObject SkillPrefab;
    public string ColliderName;
    public Vector3 StartPosition;
    public Vector3 HitRangePosition;
}