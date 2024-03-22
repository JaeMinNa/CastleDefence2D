using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSkill : MonoBehaviour
{
    [SerializeField] private float _inactiveTime = 0.2f;
    [SerializeField] private Collider2D[] _targets;
    private GameObject _player;
    private Animator _animator;
    private SkillSO _skillSO;
    private Vector3 _startPos;
    private Vector2 _dir;
    private LayerMask _layerMask;
    private bool _isMove;

    private void Start()
    {
        _skillSO = GameManager.I.DataManager.GameDataSO.AreaSkill;
        _player = GameManager.I.PlayerManager.Player;
        _animator = transform.GetChild(0).GetComponent<Animator>();
        _layerMask = LayerMask.NameToLayer("Enemy");

        float random = Random.Range(_player.transform.position.x - _skillSO.Range, _player.transform.position.x + _skillSO.Range);
        _startPos = new Vector3(random, 10f, 0);
        transform.position = _startPos;
        _isMove = true;
    }

    private void OnEnable()
    {
        if (_skillSO != null)
        {
            _isMove = true;
            float random = Random.Range(_player.transform.position.x - _skillSO.Range, _player.transform.position.x + _skillSO.Range);
            _startPos = new Vector3(random, 10f, 0);
            transform.position = _startPos;
        }
    }

    private void Update()
    {
        if(_isMove)
        {
            transform.position += new Vector3(0, -_skillSO.Speed, 0) * Time.deltaTime;
        }
    }

    private void Targetting()
    {
        int layerMask = (1 << _layerMask);  // Layer ¼³Á¤
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
        if (collision.CompareTag("Ground"))
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
