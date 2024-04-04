using System.Collections;
using UnityEngine;

public class PlayerAttackState : MonoBehaviour, IPlayerState
{
    private PlayerController _playerController;
    private PlayerAnimationEvent _playerAnimationEvnet;
    private RaycastHit2D _hitInfo;
    private int _layerMask;
    [HideInInspector] public float time;

    public void Handle(PlayerController playerController)
    {
        if (!_playerController)
            _playerController = playerController;

        Debug.Log("Player Attack State");

        _playerAnimationEvnet = gameObject.transform.GetChild(0).GetComponent<PlayerAnimationEvent>();
        _layerMask = 1 << LayerMask.NameToLayer("Wall");
        time = 0;

        StartCoroutine(COUpdate());
    }

    // Update문과 동일하게 사용
    IEnumerator COUpdate()
    {
        while (true)
        {
            if(_playerController.IsMove) time += Time.deltaTime;
            if(time >= GameManager.I.DataManager.PlayerData.AttackCoolTime)
            {
                time = 0f;
                StartCoroutine(_playerAnimationEvnet.COStartAttack());
            }

            if (_playerController.Speed > 0)
            {
                Debug.DrawRay(transform.position - new Vector3(0, 2, 0), Vector2.right, new Color(1, 0, 0));
                _hitInfo = Physics2D.Raycast(transform.position - new Vector3(0, 2, 0), Vector2.right, 1f, _layerMask);
                if (_hitInfo.collider != null)
                {
                    if (!_hitInfo.transform.CompareTag("Wall") && _playerController.IsMove)
                    {
                        transform.position += new Vector3(_playerController.Speed, 0, 0) * Time.deltaTime;
                    }
                }
                else if (_hitInfo.collider == null && _playerController.IsMove)
                {
                    transform.position += new Vector3(_playerController.Speed, 0, 0) * Time.deltaTime;
                }
            }
            else
            {
                Debug.DrawRay(transform.position - new Vector3(0, 2, 0), Vector2.left, new Color(1, 0, 0));
                _hitInfo = Physics2D.Raycast(transform.position - new Vector3(0, 2, 0), Vector2.left, 1f, _layerMask);
                if (_hitInfo.collider != null)
                {
                    if (!_hitInfo.transform.CompareTag("Wall") && _playerController.IsMove)
                    {
                        transform.position += new Vector3(_playerController.Speed, 0, 0) * Time.deltaTime;
                    }
                }
                else if (_hitInfo.collider == null && _playerController.IsMove)
                {
                    transform.position += new Vector3(_playerController.Speed, 0, 0) * Time.deltaTime;
                }
            }

            yield return null;
        }
    }
}
