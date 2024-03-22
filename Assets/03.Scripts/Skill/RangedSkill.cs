using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedSkill : MonoBehaviour
{
    [SerializeField] private float _inactiveTime = 0.2f;
    [SerializeField] private Collider2D[] _targets;
    private SkillSO _skillSO;
    private SpriteRenderer _playerSpriteRenderer;
    private SpriteRenderer _skillSpriteRenderer;
    private Animator _animator;
    private GameObject _player;
    private LayerMask _layerMask;
    private bool _playerFlipX;
    private bool _isMove;
    private Vector2 _dir;
    private Vector3 _localPosition;


    private void Start()
    {
        _player = GameManager.I.PlayerManager.Player;
        _playerSpriteRenderer = _player.transform.GetChild(0).GetComponent<SpriteRenderer>();
        _skillSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _animator = _skillSpriteRenderer.transform.GetComponent<Animator>();
        _skillSO = GameManager.I.DataManager.GameDataSO.RangedSkill;
        _layerMask = LayerMask.NameToLayer("Enemy");
        _isMove = true;
        _localPosition = transform.GetChild(0).gameObject.transform.localPosition;

        if (_playerSpriteRenderer.flipX)
        {
            _skillSpriteRenderer.flipX = true;
            _playerFlipX = true;
        }
        else
        {
            _skillSpriteRenderer.flipX = false;
            _playerFlipX = false;
        }

        StartCoroutine(COInactiveSkill(4f));
    }

    private void OnEnable()
    {
        if (_playerSpriteRenderer != null)
        {
            _isMove = true;
            if (_playerSpriteRenderer.flipX)
            {
                _skillSpriteRenderer.flipX = true;
                _playerFlipX = true;
            }
            else
            {
                _skillSpriteRenderer.flipX = false;
                _playerFlipX = false;
            }

            // 재사용 시, 로컬 포지션이 변함 -> 임시 방편 해결
            transform.GetChild(0).gameObject.transform.localPosition = _localPosition;
            StartCoroutine(COInactiveSkill(4f));
        }
    }

    private void Update()
    {
        if(_isMove)
        {
            if (!_playerFlipX)
            {
                transform.position += new Vector3(_skillSO.Speed, 0, 0) * Time.deltaTime;
            }
            else
            {
                transform.position += new Vector3(-_skillSO.Speed, 0, 0) * Time.deltaTime;
            }
        }
    }

    private void Targetting()
    {
        int layerMask = (1 << _layerMask);  // Layer 설정
        _targets = Physics2D.OverlapCircleAll(transform.position - new Vector3(0, 2, 0), _skillSO.ExplosionRange, layerMask);

        for (int i = 0; i < _targets.Length; i++)
        {
            _dir = _targets[i].gameObject.transform.position - transform.position;
            _targets[i].gameObject.GetComponent<EnemyController>().Ishit = true;
            _targets[i].gameObject.GetComponent<EnemyController>().Hp -= _skillSO.Atk;
            if (_dir.x > 0)
            {
                _targets[i].gameObject.GetComponent<EnemyController>().Rigdbody.AddForce(new Vector2(1, 1) * _skillSO.NuckbackPower, ForceMode2D.Impulse);
            }
            else
            {
                _targets[i].gameObject.GetComponent<EnemyController>().Rigdbody.AddForce(new Vector2(-1, 1) * _skillSO.NuckbackPower, ForceMode2D.Impulse);
            }
        }
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position - new Vector3(0, 2, 0), _skillSO.ExplosionRange);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        { 
            StartCoroutine(COInactiveSkill(_inactiveTime));
            _isMove = false;
            _animator.SetTrigger("Hit");
            Targetting();
        }
    }

    IEnumerator COInactiveSkill(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
