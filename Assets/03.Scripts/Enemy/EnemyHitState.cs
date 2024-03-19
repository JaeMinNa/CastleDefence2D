using System.Collections;
using UnityEngine;

public class EnemyHitState : MonoBehaviour, IEnemyState
{
    private EnemyController _enemyController;

    public void Handle(EnemyController enemyController)
    {
        if (!_enemyController)
            _enemyController = enemyController;

        Debug.Log("Enemy Hit State");
        _enemyController.Animator.SetBool("Hit", true);

        StartCoroutine(COStopHit());
    }

    IEnumerator COStopHit()
    {
        yield return new WaitForSeconds(1f);

        if (_enemyController.Hp <= 0)
        {
            Destroy(gameObject);
        }

        _enemyController.Ishit = false;
        _enemyController.Animator.SetBool("Hit", false);
        _enemyController.WalkStart();
    }
}
