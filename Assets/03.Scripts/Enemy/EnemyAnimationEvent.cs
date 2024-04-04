using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    private CastleController _castleController;
    private EnemyController _enemyController;
    private StageController _stageController;

    private void Start()
    {
        _castleController = GameObject.FindWithTag("Castle").GetComponent<CastleController>();
        _enemyController = transform.parent.GetComponent<EnemyController>();
        _stageController = GameObject.FindWithTag("StageController").GetComponent<StageController>();
    }

    public void AttackMelee()
    {
        if(_stageController.IsDangerTime) _castleController.CastleHit(_enemyController.EnemyData.Atk * _stageController.DangerTimeAtkRatio);
        else _castleController.CastleHit(_enemyController.EnemyData.Atk);
    }
    
    public void ShootBullet()
    {
         GameManager.I.ObjectPoolManager.ActivePrefab(_enemyController.EnemyData.BulletTag, transform.position + _enemyController.EnemyData.BulletPosition);
    }
}
