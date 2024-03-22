using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSkill : MonoBehaviour
{
    private SkillSO _meleeSkillSO;
    private SpriteRenderer _playerSpriteRenderer;
    private SpriteRenderer _skillSpriteRenderer;

    private void Start()
    {
        _playerSpriteRenderer = GameManager.I.PlayerManager.Player.transform.GetChild(0).GetComponent<SpriteRenderer>();
        _skillSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _meleeSkillSO = GameManager.I.DataManager.GameDataSO.MeleeSkill;

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
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}