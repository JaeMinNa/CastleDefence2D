using System.Collections;
using UnityEngine;

public class EnemyWalkState : MonoBehaviour, IEnemyState
{
    private EnemyController _enemyController;
    private SpriteRenderer _spriteRenderer;
    private StageController _stageController;
    private EnemyData.AttackType _attackType;
    private RaycastHit2D _hitInfo;
    private int _layerMask;

    public void Handle(EnemyController enemyController)
    {
        if (!_enemyController)
            _enemyController = enemyController;

        Debug.Log("Enemy Walk State");

        _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _stageController = GameObject.FindWithTag("StageController").GetComponent<StageController>();
        _attackType = _enemyController.EnemyData.Type;
        _layerMask = 1 << LayerMask.NameToLayer("Castle");

        if (gameObject.activeInHierarchy) StartCoroutine(COUpdate());
    }

    IEnumerator COUpdate()
    {
        while (true)
        {
            if(transform.position.x > 0)
            {
                _spriteRenderer.flipX = true;
                if(_stageController.IsDangerTime) transform.position += new Vector3(-_enemyController.EnemyData.Speed * _stageController.DangerTimeSpeedRatio, 0, 0) * Time.deltaTime;
                else transform.position += new Vector3(-_enemyController.EnemyData.Speed, 0, 0) * Time.deltaTime;

                if (_attackType == EnemyData.AttackType.Ranged)
                {
                    Debug.DrawRay(transform.position - new Vector3(0, 2, 0), Vector2.left * _enemyController.EnemyData.Distance, new Color(1, 0, 0));
                    _hitInfo = Physics2D.Raycast(transform.position - new Vector3(0, 2, 0), Vector2.left, _enemyController.EnemyData.Distance, _layerMask);

                    if (_hitInfo.collider != null)
                    {
                        if (_hitInfo.transform.CompareTag("Castle"))
                        {
                            _enemyController.IsAttack = true;
                        }
                    }
                }
            }
            else
            {
                _spriteRenderer.flipX = false;
                if (_stageController.IsDangerTime) transform.position += new Vector3(_enemyController.EnemyData.Speed * _stageController.DangerTimeSpeedRatio, 0, 0) * Time.deltaTime;
                else transform.position += new Vector3(_enemyController.EnemyData.Speed, 0, 0) * Time.deltaTime;

                if(_attackType == EnemyData.AttackType.Ranged)
                {
                    Debug.DrawRay(transform.position - new Vector3(0, 2, 0), Vector2.right * _enemyController.EnemyData.Distance, new Color(1, 0, 0));
                    _hitInfo = Physics2D.Raycast(transform.position - new Vector3(0, 2, 0), Vector2.right, _enemyController.EnemyData.Distance, _layerMask);

                    if (_hitInfo.collider != null)
                    {
                        if (_hitInfo.transform.CompareTag("Castle"))
                        {
                            _enemyController.IsAttack = true;
                        }
                    }
                }
            }

            if(_enemyController.Ishit)
            {
                _enemyController.HitStart();
                break;
            }
            if(_enemyController.IsAttack)
            {
                _enemyController.AttackStart();
                break;
            }

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_attackType == EnemyData.AttackType.Melee)
        {
            if (collision.CompareTag("Castle"))
            {
                _enemyController.IsAttack = true;
            }
        }
    }
}
