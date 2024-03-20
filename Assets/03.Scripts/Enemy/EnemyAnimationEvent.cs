using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    private CastleController _castleController;
    private EnemyController _enemyController;

    private void Start()
    {
        _castleController = GameObject.FindWithTag("Castle").GetComponent<CastleController>();
        _enemyController = transform.parent.GetComponent<EnemyController>();
    }

    public void AttackSnail()
    {
        _castleController.CastleHit(_enemyController.EnemySO.Atk);
    }
}
