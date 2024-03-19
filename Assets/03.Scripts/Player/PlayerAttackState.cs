using System.Collections;
using UnityEngine;

public class PlayerAttackState : MonoBehaviour, IPlayerState
{
    private PlayerController _playerController;
    private SpriteRenderer _spriteRenderer;
    private RaycastHit2D _hitInfo;

    public void Handle(PlayerController enemyController)
    {
        if (!_playerController)
            _playerController = enemyController;

        Debug.Log("Player Attack State");
        _spriteRenderer = _playerController.transform.GetChild(0).GetComponent<SpriteRenderer>();
        StartCoroutine(COUpdate());
    }

    // Update문과 동일하게 사용
    IEnumerator COUpdate()
    {
        while (true)
        {
            if (_playerController.PlayerSO.Speed > 0)
            {
                Debug.DrawRay(transform.position - new Vector3(0, 2, 0), Vector2.right, new Color(1, 0, 0));
                _hitInfo = Physics2D.Raycast(transform.position - new Vector3(0, 2, 0), Vector2.right, 1f);
                if (_hitInfo.collider != null)
                {
                    if (!_hitInfo.transform.CompareTag("Wall"))
                    {
                        transform.position += new Vector3(_playerController.PlayerSO.Speed, 0, 0) * Time.deltaTime;
                    }
                }
                else transform.position += new Vector3(_playerController.PlayerSO.Speed, 0, 0) * Time.deltaTime;
                _spriteRenderer.flipX = false;
            }
            else
            {
                Debug.DrawRay(transform.position - new Vector3(0, 2, 0), Vector2.left, new Color(1, 0, 0));
                _hitInfo = Physics2D.Raycast(transform.position - new Vector3(0, 2, 0), Vector2.left, 1f);
                if (_hitInfo.collider != null)
                {
                    if (!_hitInfo.transform.CompareTag("Wall"))
                    {
                        transform.position += new Vector3(_playerController.PlayerSO.Speed, 0, 0) * Time.deltaTime;
                    }
                }
                else transform.position += new Vector3(_playerController.PlayerSO.Speed, 0, 0) * Time.deltaTime;
                _spriteRenderer.flipX = true;
            }

            yield return null;
        }
    }
}
