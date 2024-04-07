using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleController : MonoBehaviour
{
    [Header("Castle")]
    public float Hp;

    [Header("UI")]
    [SerializeField] private Slider _castleHp;
    [SerializeField] private Sprite _brokenCastle;
    [SerializeField] private StageController _stageController;

    [Header("Arrow")]
    [SerializeField] private Transform _arrowRight;
    [SerializeField] private Transform _arrowLeft;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private CastleData _castleData;
    private bool _isBroken;
    private int _count;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _castleData = GameManager.I.DataManager.CastleData;
        Hp = _castleData.Hp;
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
        _castleHp.value = (float)Hp / _castleData.Hp;
        _animator.SetTrigger("Hit");


        if (Hp <= 0)
        {
            _stageController.GameOverActive();
        }
    }

    public void CastleHpRecovery(int hp)
    {
        Hp += hp;

        if (Hp >= _castleData.Hp)
        {
            Hp = _castleData.Hp;
        }

        CastleHpUpdate();
    }

    public void CastleHpUpdate()
    {
        _castleHp.value = (float)Hp / _castleData.Hp;
    }

    IEnumerator COCastleArrow()
    {
        while (true)
        {
            yield return new WaitForSeconds(_castleData.AttackCoolTime);

            GameManager.I.SoundManager.StartSFX("ArrowShoot");
            if (_count % 2 == 0) GameManager.I.ObjectPoolManager.ActivePrefab("ArrowRight", _arrowRight.localPosition);
            else GameManager.I.ObjectPoolManager.ActivePrefab("ArrowLeft", _arrowLeft.localPosition);

            _count++;
        }
    }
}
