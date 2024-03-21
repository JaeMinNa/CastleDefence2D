using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private float _attackStopTime = 0.6f;
    [SerializeField] private float _activeAttackColliderTime = 0.4f;
    [SerializeField] private float _inactiveAttackColliderTime = 0.2f;
    [SerializeField] private GameObject _attackCollider;

    [Header("MeleeSkill")]
    [SerializeField] private float _meleeSkillStopTime = 0.6f;
    [SerializeField] private float _active_meleeSkillColliderTime = 0.4f;
    [SerializeField] private float _inactive_meleeSkillColliderTime = 0.2f;
    [SerializeField] private GameObject _colliders;

    private PlayerController _playerController;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _playerController = GameManager.I.PlayerManager.Player.GetComponent<PlayerController>();
        _spriteRenderer = _playerController.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public IEnumerator COStartAttack()
    {
        _playerController.IsMove = false;

        yield return new WaitForSeconds(_attackStopTime);
        _playerController.IsMove = true;
    }

    public IEnumerator COActiveAttackCollider()
    {
        yield return new WaitForSeconds(_activeAttackColliderTime);
        _attackCollider.SetActive(true);

        yield return new WaitForSeconds(_inactiveAttackColliderTime);
        _attackCollider.SetActive(false);
    }

    public IEnumerator COStartMeleeSkill()
    {
        
        _playerController.IsMove = false;
        yield return new WaitForSeconds(_meleeSkillStopTime);
        _playerController.IsMove = true;
    }

    public IEnumerator COActiveMeleeSkillCollider(SkillSO skillSO)
    {
        yield return new WaitForSeconds(_active_meleeSkillColliderTime);
        _colliders.transform.Find(skillSO.ColliderName).gameObject.SetActive(true);
        if(!_spriteRenderer.flipX)
        {
            GameManager.I.ObjectPoolManager.
            InstantiatePrefab(skillSO.Tag, _colliders.transform.Find(skillSO.ColliderName).transform.position + skillSO.Position);
        }
        else
        {
            Vector3 vec = new Vector3(-skillSO.Position.x, skillSO.Position.y, skillSO.Position.z);
            GameManager.I.ObjectPoolManager.
            InstantiatePrefab(skillSO.Tag, _colliders.transform.Find(skillSO.ColliderName).transform.position + vec);
        }

         yield return new WaitForSeconds(_inactive_meleeSkillColliderTime);
        _colliders.transform.Find(skillSO.ColliderName).gameObject.SetActive(false);
        skillSO.SkillPrefab.SetActive(false);
    }
}
