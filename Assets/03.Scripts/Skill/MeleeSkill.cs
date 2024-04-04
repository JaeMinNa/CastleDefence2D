using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSkill : MonoBehaviour
{
    [SerializeField] private float _inactiveTime;
    private SkillSO _meleeSkillSO;
    private SpriteRenderer _playerSpriteRenderer;
    private SpriteRenderer _skillSpriteRenderer;

    private void Start()
    {
        _playerSpriteRenderer = GameManager.I.PlayerManager.Player.transform.GetChild(0).GetComponent<SpriteRenderer>();
        _skillSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _meleeSkillSO = GameManager.I.DataManager.PlayerData.EquipMeleeSkill;

        if (_playerSpriteRenderer.flipX) _skillSpriteRenderer.flipX = true;
        else _skillSpriteRenderer.flipX = false;

        GameManager.I.SoundManager.StartSFX(_meleeSkillSO.Tag);
    }

    private void OnEnable()
    {
        if(_playerSpriteRenderer != null && _skillSpriteRenderer != null)
        {
            if (_playerSpriteRenderer.flipX) _skillSpriteRenderer.flipX = true;
            else _skillSpriteRenderer.flipX = false;

            GameManager.I.SoundManager.StartSFX(_meleeSkillSO.Tag);
        }

        StartCoroutine(COSkillStart());
    }

    IEnumerator COSkillStart()
    {
        yield return new WaitForSeconds(_inactiveTime);
        gameObject.SetActive(false);
    }
}
