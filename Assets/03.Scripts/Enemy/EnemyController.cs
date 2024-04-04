using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum EnemyTag
    {
        Snail,
        Rock,
        Chicken,
        Mushroom,
        Bunny,
        Turtle,
        Rino,
        Plant,
        Trunk,
        Radish,
        Skull,
    }

    public EnemyStateContext _enemyStateContext { get; private set; }

    public EnemyTag Tag;
    //public EnemySO EnemySO;
    public Animator Animator { get; private set; }
    public Rigidbody2D Rigdbody { get; private set; }
    public float Hp;
    public bool Ishit;
    public bool IsAttack;
    public EnemyData EnemyData;

    private IEnemyState _walkState;
    private IEnemyState _hitState;
    private IEnemyState _attackState;

    private void Start()
    {
        _enemyStateContext = new EnemyStateContext(this);

        _walkState = gameObject.AddComponent<EnemyWalkState>();
        _hitState = gameObject.AddComponent<EnemyHitState>();
        _attackState = gameObject.AddComponent<EnemyAttackState>();
        Animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        Rigdbody = GetComponent<Rigidbody2D>();

        switch (Tag)
        {
            case EnemyTag.Snail:
                EnemyData = GameManager.I.DataManager.DataWrapper.MeleeEnemyData[0];
                break;
            case EnemyTag.Rock:
                EnemyData = GameManager.I.DataManager.DataWrapper.MeleeEnemyData[1];
                break;
            case EnemyTag.Chicken:
                EnemyData = GameManager.I.DataManager.DataWrapper.MeleeEnemyData[2];
                break;
            case EnemyTag.Mushroom:
                EnemyData = GameManager.I.DataManager.DataWrapper.MeleeEnemyData[3];
                break;
            case EnemyTag.Bunny:
                EnemyData = GameManager.I.DataManager.DataWrapper.MeleeEnemyData[4];
                break;
            case EnemyTag.Turtle:
                EnemyData = GameManager.I.DataManager.DataWrapper.MeleeEnemyData[5];
                break;
            case EnemyTag.Rino:
                EnemyData = GameManager.I.DataManager.DataWrapper.MeleeEnemyData[6];
                break;
            case EnemyTag.Plant:
                EnemyData = GameManager.I.DataManager.DataWrapper.MeleeEnemyData[7];
                break;
            case EnemyTag.Trunk:
                EnemyData = GameManager.I.DataManager.DataWrapper.MeleeEnemyData[8];
                break;
            case EnemyTag.Radish:
                EnemyData = GameManager.I.DataManager.DataWrapper.MeleeEnemyData[9];
                break;
            case EnemyTag.Skull:
                EnemyData = GameManager.I.DataManager.DataWrapper.MeleeEnemyData[10];
                break;
            default:
                break;
        }

        Hp = EnemyData.Hp;
        Ishit = false;
        IsAttack = false;

        _enemyStateContext.Transition(_walkState);
    }

    private void OnEnable()
    {
        if(_enemyStateContext != null)
        {
            Hp = EnemyData.Hp;
            _enemyStateContext.Transition(_walkState);
        }
    }

    public void WalkStart()
    {
        _enemyStateContext.Transition(_walkState);
    }

    public void HitStart()
    {
        _enemyStateContext.Transition(_hitState);
    }

    public void AttackStart()
    {
        _enemyStateContext.Transition(_attackState);
    }

}
