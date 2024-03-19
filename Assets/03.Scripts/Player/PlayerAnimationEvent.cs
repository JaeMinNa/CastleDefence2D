using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    [Header("Stop Time")]
    [SerializeField] private float _attackStopTime = 0.6f;
    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    public void StartAttack()
    {
        _playerController.IsMove = false;
    }

    public IEnumerator COStartAttack()
    {
        _playerController.IsMove = false;

        yield return new WaitForSeconds(_attackStopTime);
        _playerController.IsMove = true;
    }
}
