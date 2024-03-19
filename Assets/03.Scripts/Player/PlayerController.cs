using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerSO PlayerSO;
    private SpriteRenderer _spriteRenderer;

    public PlayerStateContext _playerStateContext { get; private set; }

    private IPlayerState _attackState;

    private void Start()
    {
        _playerStateContext = new PlayerStateContext(this);
        _attackState = gameObject.AddComponent<PlayerAttackState>();

        _playerStateContext.Transition(_attackState);

        _spriteRenderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
}
