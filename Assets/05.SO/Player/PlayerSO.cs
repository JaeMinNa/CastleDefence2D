using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerSO", menuName = "Data/PlayerSO", order = 1)]

public class PlayerSO : StatSO 
{
    [Header("Player Stats")]
    public int Level;
    public float MaxExp;
    public float CurrentExp;
    public float SkillTime;
    public float AttackCoolTime;
    public float NuckbackPower;

    [Header("Player Skill Inventory")]
    public List<SkillSO> SkillInventroy;

    [Header("Player Equip Skills")]
    public SkillSO EquipMeleeSkill;
    public SkillSO EquipRangedSkill;
    public SkillSO EquipAreaSkill;
}

