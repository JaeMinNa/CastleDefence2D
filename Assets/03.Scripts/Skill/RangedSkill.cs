using System.Collections;
using UnityEngine;

public class RangedSkill : MonoBehaviour
{
    [SerializeField] private float _inactiveTime = 0.2f;
    [SerializeField] private Collider2D[] _targets;
    private SkillData _rangedSkillData;
    private SpriteRenderer _playerSpriteRenderer;
    private SpriteRenderer _skillSpriteRenderer;
    private Animator _animator;
    private GameObject _player;
    private CameraShake _cameraShake;
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
        _cameraShake = Camera.main.GetComponent<CameraShake>();
        _rangedSkillData = GameManager.I.DataManager.PlayerData.EquipRangedSkillData;
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

        GameManager.I.SoundManager.StartSFX(_rangedSkillData.Tag);
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

            GameManager.I.SoundManager.StartSFX(_rangedSkillData.Tag);
            StartCoroutine(COInactiveSkill(4f));
        }
    }

    private void Update()
    {
        if(_isMove)
        {
            if (!_playerFlipX)
            {
                transform.position += new Vector3(_rangedSkillData.Speed, 0, 0) * Time.deltaTime;
            }
            else
            {
                transform.position += new Vector3(-_rangedSkillData.Speed, 0, 0) * Time.deltaTime;
            }
        }
    }

    private void Targetting()
    {
        int layerMask = (1 << _layerMask);  // Layer 설정
        _targets = Physics2D.OverlapCircleAll(transform.position - new Vector3(0, 2, 0), _rangedSkillData.ExplosionRange, layerMask);

        for (int i = 0; i < _targets.Length; i++)
        {
            _dir = _targets[i].gameObject.transform.position - _player.transform.position;
            _targets[i].gameObject.GetComponent<EnemyController>().Ishit = true;
            _targets[i].gameObject.GetComponent<EnemyController>().Hp -= _player.GetComponent<PlayerController>().Atk * _rangedSkillData.AtkRatio;
            GameManager.I.ObjectPoolManager.ActiveDamage("DamageText", _targets[i].gameObject.transform.position - new Vector3(0, 2, 0), (int)(GameManager.I.DataManager.PlayerData.Atk * _rangedSkillData.AtkRatio), 31);
            if (_dir.x > 0)
            {
                _targets[i].gameObject.GetComponent<EnemyController>().Rigdbody.AddForce(new Vector2(1, 1) * _rangedSkillData.NuckbackPower, ForceMode2D.Impulse);
            }
            else
            {
                _targets[i].gameObject.GetComponent<EnemyController>().Rigdbody.AddForce(new Vector2(-1, 1) * _rangedSkillData.NuckbackPower, ForceMode2D.Impulse);
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
            if(!_player.GetComponent<PlayerController>().IsMeleeExplosion)
            {
                _isMove = false;
                _skillSpriteRenderer.enabled = false;
                GameManager.I.SoundManager.StartSFX(_rangedSkillData.SkillExplosionTag);
                StartCoroutine(_cameraShake.COShake(0.5f, 0.5f));
                StartCoroutine(COInactiveSkill(_inactiveTime));

                Targetting();

                _animator.SetTrigger("Hit");
                if (!_skillSpriteRenderer.flipX)
                {
                    transform.position += _rangedSkillData.HitRangePosition;
                }
                else
                {
                    transform.position += new Vector3(-_rangedSkillData.HitRangePosition.x, _rangedSkillData.HitRangePosition.y, _rangedSkillData.HitRangePosition.z);
                }
                _skillSpriteRenderer.enabled = true;
                _player.GetComponent<PlayerController>().IsMeleeExplosion = true;
            }
        }
    }

    IEnumerator COInactiveSkill(float time)
    {
        yield return new WaitForSeconds(time);
        _player.GetComponent<PlayerController>().IsMeleeExplosion = false;
        gameObject.SetActive(false);
    }
}
