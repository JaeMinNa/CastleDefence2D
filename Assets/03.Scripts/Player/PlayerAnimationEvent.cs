using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private float _activeAttackColliderTime = 0.2f;
    [SerializeField] private float _inactiveAttackColliderTime = 0.2f;
    [SerializeField] private float _attackStopTime = 0.2f;
    [SerializeField] private GameObject _attackCollider;

    [Header("MeleeSkill")]
    [SerializeField] private float _activeMeleeSkillColliderTime = 0.2f;
    [SerializeField] private float _inactiveMeleeSkillColliderTime = 0.2f;
    [SerializeField] private float _meleeSkillStopTime = 0.01f;

    [Header("RangedSkill")]
    [SerializeField] private float _shootRangedSkillTime = 0.2f;
    [SerializeField] private float _rangedSkillStopTime = 0.2f;

    [Header("AreaSkill")]
    [SerializeField] private float _shootAreaSkillTime = 1f;
    [SerializeField] private float _areaSkillStopTime = 0.5f;

    [Header("Colliders")]
    [SerializeField] private GameObject _colliders;

    private PlayerController _playerController;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private void Start()
    {
        _playerController = GameManager.I.PlayerManager.Player.GetComponent<PlayerController>();
        _spriteRenderer = _playerController.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    public IEnumerator COStartAttack()
    {
        _playerController.Animator.SetBool("Attack", true);
        _playerController.IsMove = false;
        GameManager.I.SoundManager.StartSFX("Sword");

        yield return new WaitForSeconds(_activeAttackColliderTime);
        _attackCollider.SetActive(true);

        yield return new WaitForSeconds(_inactiveAttackColliderTime);
        _attackCollider.SetActive(false);

        yield return new WaitForSeconds(_attackStopTime);
        _playerController.IsMove = true;
        _animator.SetBool("Attack", false);

    }

    public IEnumerator COStartMeleeSkill(SkillSO skillSO)
    {
        _playerController.Animator.SetBool("MeleeSkill", true);
        _playerController.IsMove = false;

        yield return new WaitForSeconds(_activeMeleeSkillColliderTime);
        _colliders.transform.Find(skillSO.ColliderName).gameObject.SetActive(true);
        if (!_spriteRenderer.flipX)
        {
            GameManager.I.ObjectPoolManager.
            ActivePrefab(skillSO.Tag, transform.position + skillSO.StartPosition);
        }
        else
        {
            Vector3 vec = new Vector3(-skillSO.StartPosition.x, skillSO.StartPosition.y, skillSO.StartPosition.z);
            GameManager.I.ObjectPoolManager.
            ActivePrefab(skillSO.Tag, transform.position + vec);
        }

        yield return new WaitForSeconds(_inactiveMeleeSkillColliderTime);
        _colliders.transform.Find(skillSO.ColliderName).gameObject.SetActive(false);

        yield return new WaitForSeconds(_meleeSkillStopTime);
        _playerController.IsMove = true;
        _playerController.Animator.SetBool("MeleeSkill", false);
    }

    public IEnumerator COStartRangedSkill(SkillSO skillSO)
    {
        _playerController.Animator.SetBool("RangedSkill", true);
        _playerController.IsMove = false;

        yield return new WaitForSeconds(_shootRangedSkillTime);
        if (!_spriteRenderer.flipX)
        {
            GameManager.I.ObjectPoolManager.ActivePrefab(skillSO.Tag, transform.position + skillSO.StartPosition);
        }
        else
        {
            Vector3 vec = new Vector3(-skillSO.StartPosition.x, skillSO.StartPosition.y, skillSO.StartPosition.z);
            GameManager.I.ObjectPoolManager.ActivePrefab(skillSO.Tag, transform.position + vec);
        }

        yield return new WaitForSeconds(_rangedSkillStopTime);
        _playerController.IsMove = true;
        _playerController.Animator.SetBool("RangedSkill", false);
    }

    public IEnumerator COStartAreaSkill(SkillSO skillSO)
    {
        _playerController.Animator.SetBool("AreaSkill", true);
        _playerController.IsMove = false;

        yield return new WaitForSeconds(_shootAreaSkillTime);
        StartCoroutine(COShootAreaSkill(skillSO));     

        yield return new WaitForSeconds(_areaSkillStopTime);
        _playerController.IsMove = true;
        _playerController.Animator.SetBool("AreaSkill", false);
    }

    IEnumerator COShootAreaSkill(SkillSO areaSkillSO)
    {
        int count = 0;
        while (true)
        {
            count++;
            GameManager.I.ObjectPoolManager.ActivePrefab(areaSkillSO.Tag, transform.position);

            if (count == areaSkillSO.Count) break;
            yield return new WaitForSeconds(areaSkillSO.Interval);
        }
    }
}
