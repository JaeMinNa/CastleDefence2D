using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleController : MonoBehaviour
{
    [Header("Castle")]
    public CastleSO CastleSO;
    public float Hp;

    [Header("UI")]
    [SerializeField] private Slider _castleHp;
    [SerializeField] private Sprite _brokenCastle;
    [SerializeField] private StageController _stageController;

    [Header("Arrow")]
    [SerializeField] private Transform _arrowRight;
    [SerializeField] private Transform _arrowLeft;

    [Header("Danger Time")]
    [SerializeField] private GameDataSO _gameDataSO;
    [SerializeField] private float _dangerTimeSpeed = 1.5f;
    [SerializeField] private float _dangerTimeAttack = 1.5f;

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
            _stageController.GameOverActive();
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
