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

    public PlayerStateContext _playerStateContext { get; private set; }

    private IPlayerState _attackState;

    public bool IsMeleeExplosion;

    private void Start()
    {
        _playerStateContext = new PlayerStateContext(this);
        _attackState = gameObject.AddComponent<PlayerAttackState>();
        Animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        Atk = GameManager.I.DataManager.PlayerData.Atk;
        Speed = GameManager.I.DataManager.PlayerData.Speed;
        CoolTime = GameManager.I.DataManager.PlayerData.SkillTime;

        _playerStateContext.Transition(_attackState);

        IsMove = true;
        IsMeleeExplosion = false;
    }
}
