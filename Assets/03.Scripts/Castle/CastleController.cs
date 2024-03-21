using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleController : MonoBehaviour
{
    public CastleSO CastleSO;
    public float Hp;
    [SerializeField] private Slider _castleHp;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Hp = CastleSO.Hp;
    }
    
    public void CastleHit(float damage)
    {
        Hp -= damage;
        _castleHp.value = (float)Hp / CastleSO.Hp;
        _animator.SetTrigger("Hit");


        if (Hp <= 0)
        {
            Debug.Log("GameOver");
        }
    }
}
