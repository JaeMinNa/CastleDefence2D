using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Atk;
    public float Speed;
    public float CoolTime;
    public Animator Animator { get; private set; }
    [HideInInspector] public bool IsMove;
    [HideInInspector] public bool IsMeleeExplosion;

    public PlayerStateContext _playerStateContext { get; private set; }

    private IPlayerState _attackState;

    private PlayerData _playerData;

    private void Start()
    {
        _playerStateContext = new PlayerStateContext(this);
        _attackState = gameObject.AddComponent<PlayerAttackState>();
        Animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        _playerData = GameManager.I.DataManager.PlayerData;

        if(!_playerData.IsAttribute) Atk = GameManager.I.DataManager.PlayerData.Atk;
        else Atk = GameManager.I.DataManager.PlayerData.Atk * 1.5f;

        Speed = GameManager.I.DataManager.PlayerData.Speed;
        CoolTime = GameManager.I.DataManager.PlayerData.SkillTime;

        _playerStateContext.Transition(_attackState);

        IsMove = true;
        IsMeleeExplosion = false;
    }
}
