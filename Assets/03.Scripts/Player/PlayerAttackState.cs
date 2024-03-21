using System.Collections;
using UnityEngine;

public class PlayerAttackState : MonoBehaviour, IPlayerState
{
    private PlayerController _playerController;
    private PlayerAnimationEvent _playerAnimationEvnet;
    private RaycastHit2D _hitInfo;
    [HideInInspector] public float time;

    public void Handle(PlayerController playerController)
    {
        if (!_playerController)
            _playerController = playerController;

        Debug.Log("Player Attack State");

        _playerAnimationEvnet = gameObject.transform.GetChild(0).GetComponent<PlayerAnimationEvent>();

        time = 0;

        StartCoroutine(COUpdate());
    }

    // Update문과 동일하게 사용
    IEnumerator COUpdate()
    {
        while (true)
        {
            time += Time.deltaTime;
            if(time >= _playerController.PlayerSO.AttackCoolTime)
            {
                time = 0f;
                _playerController.Animator.SetTrigger("Attack");
                StartCoroutine(_playerAnimationEvnet.COStartAttack());
                StartCoroutine(_playerAnimationEvnet.COActiveAttackCollider());
            }

            if (_playerController.PlayerSO.Speed > 0)
            {
                Debug.DrawRay(transform.position - new Vector3(0, 2, 0), Vector2.right, new Color(1, 0, 0));
                _hitInfo = Physics2D.Raycast(transform.position - new Vector3(0, 2, 0), Vector2.right, 1f);
                if (_hitInfo.collider != null)
                {
                    if (!_hitInfo.transform.CompareTag("Wall") && _playerController.IsMove)
                    {
                        transform.position += new Vector3(_playerController.PlayerSO.Speed, 0, 0) * Time.deltaTime;
                    }
                }
                else if (_hitInfo.collider == null && _playerController.IsMove)
                {
                    transform.position += new Vector3(_playerController.PlayerSO.Speed, 0, 0) * Time.deltaTime;
                }
            }
            else
            {
                Debug.DrawRay(transform.position - new Vector3(0, 2, 0), Vector2.left, new Color(1, 0, 0));
                _hitInfo = Physics2D.Raycast(transform.position - new Vector3(0, 2, 0), Vector2.left, 1f);
                if (_hitInfo.collider != null)
                {
                    if (!_hitInfo.transform.CompareTag("Wall") && _playerController.IsMove)
                    {
                        transform.position += new Vector3(_playerController.PlayerSO.Speed, 0, 0) * Time.deltaTime;
                    }
                }
                else if (_hitInfo.collider == null && _playerController.IsMove)
                {
                    transform.position += new Vector3(_playerController.PlayerSO.Speed, 0, 0) * Time.deltaTime;
                }
            }

            yield return null;
        }
    }
}
