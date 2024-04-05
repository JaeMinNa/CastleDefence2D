using System.Collections;
using UnityEngine;

public class EnemyHitState : MonoBehaviour, IEnemyState
{
    private int _coinDropPercent = 10;
    private int _potionDropPercent = 13;

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

            int random1 = Random.Range(0, _coinDropPercent);
            if(random1 == 0) GameManager.I.ObjectPoolManager.ActivePrefab("Coin", transform.position + Vector3.up * 0.5f);

            int random2 = Random.Range(0, _potionDropPercent);
            if(random2 == 0)
            {
                random2 = Random.Range(0, 4);
                if (random2 == 0) GameManager.I.ObjectPoolManager.ActivePrefab("HpPotionItem", transform.position + Vector3.up * 0.5f);
                else if (random2 == 1) GameManager.I.ObjectPoolManager.ActivePrefab("CoolTimePotionItem", transform.position + Vector3.up * 0.5f);
                else if (random2 == 2) GameManager.I.ObjectPoolManager.ActivePrefab("PowerPotionItem", transform.position + Vector3.up * 0.5f);
                else if (random2 == 3) GameManager.I.ObjectPoolManager.ActivePrefab("SpeedPotionItem", transform.position + Vector3.up * 0.5f);
            }

            GameManager.I.DataManager.CoinUpdate(_enemyController.EnemyData.Price);
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
