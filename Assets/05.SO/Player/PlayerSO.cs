using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerSO", menuName = "Data/PlayerSO", order = 1)]

public class PlayerSO : StatSO 
{
    [Header("Player Stats")]
    public float SkillTime;
    public float AttackCoolTime;
    public float NuckbackPower;

    [Header("Player Equip Skills")]
    public SkillSO MeleeSkill;
    public SkillSO RangedSkill;
    public SkillSO AreaSkill;
}

