using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private float _attackStopTime = 0.6f;
    [SerializeField] private float _activeAttackColliderTime = 0.4f;
    [SerializeField] private float _InactiveAttackColliderTime = 0.2f;
    [SerializeField] private GameObject _attackCollider;

    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GameManager.I.PlayerManager.Player.GetComponent<PlayerController>();
    }

    public IEnumerator COStartAttack()
    {
        _playerController.IsMove = false;

        yield return new WaitForSeconds(_attackStopTime);
        _playerController.IsMove = true;
    }

    public IEnumerator COActiveAttackCollider()
    {
        yield return new WaitForSeconds(_activeAttackColliderTime);
        _attackCollider.SetActive(true);

        yield return new WaitForSeconds(_InactiveAttackColliderTime);
        _attackCollider.SetActive(false);
    }
}
