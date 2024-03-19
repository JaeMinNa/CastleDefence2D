using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyStateContext _enemyStateContext { get; private set; }

    public EnemySO EnemySO;
    public Animator Animator { get; private set; }
    public Rigidbody2D Rigdbody { get; private set; }
    public int Hp;
    public bool Ishit;

    private IEnemyState _walkState;
    private IEnemyState _hitState;


    private void Start()
    {
        _enemyStateContext = new EnemyStateContext(this);

        _walkState = gameObject.AddComponent<EnemyWalkState>();
        _hitState = gameObject.AddComponent<EnemyHitState>();
        Animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        Rigdbody = GetComponent<Rigidbody2D>();

        Hp = EnemySO.Hp;
        Ishit = false;

        _enemyStateContext.Transition(_walkState);
    }

    public void WalkStart()
    {
        _enemyStateContext.Transition(_walkState);
    }

    public void HitStart()
    {
        _enemyStateContext.Transition(_hitState);
    }

}
