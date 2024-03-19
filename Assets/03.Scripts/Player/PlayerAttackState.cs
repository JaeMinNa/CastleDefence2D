using System.Collections;
using UnityEngine;

public class PlayerAttackState : MonoBehaviour, IPlayerState
{
    private PlayerController _playerController;
    private PlayerAnimationEvent _playerAnimationEvnet;
    private SpriteRenderer _spriteRenderer;
    private RaycastHit2D _hitInfo;
    [HideInInspector] public float time
        ;

    public void Handle(PlayerController playerController)
    {
        if (!_playerController)
            _playerController = playerController;

        Debug.Log("Player Attack State");
        _spriteRenderer = _playerController.transform.GetChild(0).GetComponent<SpriteRenderer>();
        _playerAnimationEvnet = GetComponent<PlayerAnimationEvent>();
        time = 0;
        StartCoroutine(COUpdate());
    }

    // Update���� �����ϰ� ���
    IEnumerator COUpdate()
    {
        while (true)
        {
            time += Time.deltaTime;
            Debug.Log(time);
            if(time >= _playerController.PlayerSO.AttackCoolTime)
            {
                time = 0f;
                _playerController.Animator.SetTrigger("Attack");
                StartCoroutine(_playerAnimationEvnet.COStartAttack());
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
                _spriteRenderer.flipX = false;
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
                _spriteRenderer.flipX = true;
            }

            yield return null;
        }
    }
}
