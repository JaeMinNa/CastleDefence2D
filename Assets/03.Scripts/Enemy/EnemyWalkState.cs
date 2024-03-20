using System.Collections;
using UnityEngine;

public class EnemyWalkState : MonoBehaviour, IEnemyState
{
    private EnemyController _enemyController;
    private SpriteRenderer _spriteRenderer;

    public void Handle(EnemyController enemyController)
    {
        if (!_enemyController)
            _enemyController = enemyController;

        Debug.Log("Enemy Walk State");

        _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

        if (gameObject.activeInHierarchy) StartCoroutine(COUpdate());
    }

    IEnumerator COUpdate()
    {
        while (true)
        {
            if(transform.position.x > 0)
            {
                _spriteRenderer.flipX = true;
                transform.position += new Vector3(-_enemyController.EnemySO.Speed, 0, 0) * Time.deltaTime;
            }
            else
            {
                _spriteRenderer.flipX = false;
                transform.position += new Vector3(_enemyController.EnemySO.Speed, 0, 0) * Time.deltaTime;
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
        if(collision.CompareTag("Castle"))
        {
            _enemyController.IsAttack = true;
        }
    }
}
