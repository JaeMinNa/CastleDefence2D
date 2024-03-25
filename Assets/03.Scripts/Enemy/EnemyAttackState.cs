using System.Collections;
using UnityEngine;

public class EnemyAttackState : MonoBehaviour, IEnemyState
{
    private EnemyController _enemyController;
    private Type _type;

    public void Handle(EnemyController enemyController)
    {
        if (!_enemyController)
            _enemyController = enemyController;

        Debug.Log("Enemy Attack State");
        _type = _enemyController.EnemySO.AttackType;
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
                if (_type == Type.Ranged && _enemyController != null) _enemyController.IsAttack = false;
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
        if (_type == Type.Melee)
        {
            if (collision.CompareTag("Castle"))
            {
                if (_enemyController != null) _enemyController.IsAttack = false;
            }
        }
        
    }
}
