using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleController : MonoBehaviour
{
    public CastleSO CastleSO;
    public float Hp;
    [SerializeField] private Slider _castleHp;
    [SerializeField] private Sprite _brokenCastle;
    [Header("Arrow")]
    [SerializeField] private Transform _arrowRight;
    [SerializeField] private Transform _arrowLeft;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private bool _isBroken;
    private int _count;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Hp = CastleSO.Hp;
        _isBroken = false;
        _count = 0;
        StartCoroutine(COCastleArrow());
    }

    private void Update()
    {
        if(Hp <= Hp/2 && !_isBroken)
        {
            _isBroken = true;
            _spriteRenderer.sprite = _brokenCastle;
        }
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

    IEnumerator COCastleArrow()
    {
        while (true)
        {
            yield return new WaitForSeconds(CastleSO.AttackCoolTime);

            GameManager.I.SoundManager.StartSFX("ArrowShoot");
            if (_count % 2 == 0) GameManager.I.ObjectPoolManager.ActivePrefab("ArrowRight", _arrowRight.localPosition);
            else GameManager.I.ObjectPoolManager.ActivePrefab("ArrowLeft", _arrowLeft.localPosition);

            _count++;
        }
    }
}
