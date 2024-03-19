using System.Collections;
using UnityEngine;

public class EnemyAttackState : MonoBehaviour, IEnemyState
{
    private EnemyController _enemyController;

    public void Handle(EnemyController enemyController)
    {
        if (!_enemyController)
            _enemyController = enemyController;

        Debug.Log("Enemy Attack State");
        _enemyController.Animator.SetBool("Attack", true);

        StartCoroutine(COUpdate());
    }

    IEnumerator COUpdate()
    {
        while (true)
        {           
            if (_enemyController.Ishit)
            {
                _enemyController.HitStart();
                _enemyController.Animator.SetBool("Attack", false);
                break;
            }
            if (!_enemyController.IsAttack)
            {
                _enemyController.WalkStart();
                _enemyController.Animator.SetBool("Attack", false);
                break;
            }

            yield return null;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Castle"))
        {
            _enemyController.IsAttack = false;
        }
    }
}
