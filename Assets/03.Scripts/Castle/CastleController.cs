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
    private GameObject _player;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _player = GameManager.I.PlayerManager.Player;
        Hp = CastleSO.Hp;
    }
    
    public void CastleHit(float damage)
    {
        GameManager.I.SoundManager.StartSFX("CastleHit", transform.position);   
        Hp -= damage;
        _castleHp.value = (float)Hp / CastleSO.Hp;
        _animator.SetTrigger("Hit");


        if (Hp <= 0)
        {
            Debug.Log("GameOver");
        }
    }
}
