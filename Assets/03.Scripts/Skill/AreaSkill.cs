using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSkill : MonoBehaviour
{
    [SerializeField] private float _inactiveTime = 0.2f;
    [SerializeField] private Collider2D[] _targets;
    private GameObject _player;
    private Animator _animator;
    private CameraShake _cameraShake;
    private SkillData _areaSkillData;
    private Vector3 _startPos;
    private Vector2 _dir;
    private LayerMask _layerMask;
    private bool _isMove;

    private void Start()
    {
        _player = GameManager.I.PlayerManager.Player;
        _areaSkillData = GameManager.I.DataManager.PlayerData.EquipAreaSkillData;
        _animator = transform.GetChild(0).GetComponent<Animator>();
        _cameraShake = Camera.main.GetComponent<CameraShake>();
        _layerMask = LayerMask.NameToLayer("Enemy");

        float random = Random.Range(_player.transform.position.x - _areaSkillData.Range, _player.transform.position.x + _areaSkillData.Range);
        _startPos = new Vector3(random, 10f, 0);
        transform.position = _startPos;
        _isMove = true;

        GameManager.I.SoundManager.StartSFX(_areaSkillData.Tag);
    }

    private void OnEnable()
    {
        if (_areaSkillData != null)
        {
            _isMove = true;
            float random = Random.Range(_player.transform.position.x - _areaSkillData.Range, _player.transform.position.x + _areaSkillData.Range);
            _startPos = new Vector3(random, 10f, 0);
            transform.position = _startPos;
            GameManager.I.SoundManager.StartSFX(_areaSkillData.Tag);
        }
    }

    private void Update()
    {
        if(_isMove)
        {
            transform.position += new Vector3(0, -_areaSkillData.Speed, 0) * Time.deltaTime;
        }
    }

    private void Targetting()
    {
        int layerMask = (1 << _layerMask);  // Layer ¼³Á¤
        _targets = Physics2D.OverlapCircleAll(transform.position - new Vector3(0, 2, 0), _areaSkillData.ExplosionRange, layerMask);

        for (int i = 0; i < _targets.Length; i++)
        {
            _dir = _targets[i].gameObject.transform.position - transform.position;
            _targets[i].gameObject.GetComponent<EnemyController>().Ishit = true;
            _targets[i].gameObject.GetComponent<EnemyController>().Hp -= _player.GetComponent<PlayerController>().Atk * _areaSkillData.AtkRatio;
            GameManager.I.ObjectPoolManager.ActiveDamage("DamageText", _targets[i].gameObject.transform.position - new Vector3(0, 2, 0), (int)(GameManager.I.DataManager.PlayerData.Atk * _areaSkillData.AtkRatio), 31);
            if (_dir.x > 0)
            {
                _targets[i].gameObject.GetComponent<EnemyController>().Rigdbody.AddForce(new Vector2(1, 1) * _areaSkillData.NuckbackPower, ForceMode2D.Impulse);
            }
            else
            {
                _targets[i].gameObject.GetComponent<EnemyController>().Rigdbody.AddForce(new Vector2(-1, 1) * _areaSkillData.NuckbackPower, ForceMode2D.Impulse);
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
            GameManager.I.SoundManager.StartSFX(_areaSkillData.SkillExplosionTag);
            StartCoroutine(_cameraShake.COShake(0.5f, 0.5f));
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
