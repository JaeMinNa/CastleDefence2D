using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerSO PlayerSO;
    public Animator Animator { get; private set; }
    [HideInInspector] public bool IsMove;

    public PlayerStateContext _playerStateContext { get; private set; }

    private IPlayerState _attackState;

    private void Start()
    {
        _playerStateContext = new PlayerStateContext(this);
        _attackState = gameObject.AddComponent<PlayerAttackState>();

        Animator = gameObject.transform.GetChild(0).GetComponent<Animator>();

        _playerStateContext.Transition(_attackState);

        IsMove = true;
    }
}
