using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillSO", menuName = "Data/SkillSO", order = 5)]
public class SkillSO : ScriptableObject
{
    public enum SkillRank
    {
        B,
        A,
        S,
    }

    [Header("Common Skill Info")]
    public string Tag;
    public SkillRank Rank;
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