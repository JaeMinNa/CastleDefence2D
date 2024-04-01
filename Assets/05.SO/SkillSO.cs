using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillSO", menuName = "Data/SkillSO", order = 5)]
public class SkillSO : ScriptableObject
{
    public enum SkillRank
    {
        None,
        B,
        A,
        S,
    }

    public enum SkillType
    {
        None,
        Melee,
        Ranged,
        Area,
    }

    [Header("Common Skill Info")]
    public string Tag;
    public SkillType Type;
    public SkillRank Rank;
    public string Description;
    public int Level;
    public int MaxUpgradeCount;
    public int CurrentUpgradeCount;
    public float AtkRatio;
    public float NuckbackPower;
    public float Speed;
    public float ExplosionRange;
    public bool IsGet;
    public bool IsEquip;

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