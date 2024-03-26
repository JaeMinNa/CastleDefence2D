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
            _enemyController.Ishit = false;
            _enemyController.Animator.SetBool("Hit", false);

            int random = Random.Range(0, _enemyController.EnemySO.CoinDropPercent);
            if (random == 0) GameManager.I.ObjectPoolManager.ActivePrefab("Coin", transform.position + Vector3.up * 0.5f);

            GameManager.I.DataManager.CoinUpdate(_enemyController.EnemySO.Price);

            gameObject.SetActive(false);
        }
        else
        {
            _enemyController.Ishit = false;
            _enemyController.Animator.SetBool("Hit", false);
            _enemyController.WalkStart();
        }
    }
}
