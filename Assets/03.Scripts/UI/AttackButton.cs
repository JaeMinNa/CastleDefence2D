using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    public float ClickTime { get; private set; } 
    public bool IsClick { get; private set; }
    private PlayerController _playerController;
    private SpriteRenderer _playerSpriteRenderer;
    private Animator _animator;
    private PlayerAnimationEvent _playerAnimationEvent;
    private Transform _collidersTransform;
    private SkillSO _skillSO;

    private float _collidersPositionX;
    private float _collidersPositionY;

    // 버튼 클릭이 시작했을 때
    public void ButtonDown()
    {
        _collidersPositionX = _collidersTransform.localPosition.x;

        _playerController.PlayerSO.Speed *= -1;

        if (_playerController.PlayerSO.Speed > 0)
        {
            _playerSpriteRenderer.flipX = false;

            if (_collidersPositionX < 0)
            {
                _collidersTransform.localPosition = new Vector3(-_collidersPositionX, _collidersPositionY, 0);
            }

        }
        else
        {
            _playerSpriteRenderer.flipX = true;

            if (_collidersPositionX > 0)
            {
                _collidersTransform.localPosition = new Vector3(-_collidersPositionX, _collidersPositionY, 0);
            }
        }

        IsClick = true;
    }

    // 버튼 클릭이 끝났을 때
    public void ButtonUp()
    {
        IsClick = false;

        if (ClickTime >= _playerController.PlayerSO.SkillTime)
        {
            Debug.Log("3단계 스킬 발동!");
        }
        else if (ClickTime >= 2 * (_playerController.PlayerSO.SkillTime / 3) && ClickTime < _playerController.PlayerSO.SkillTime)
        {
            Debug.Log("2단계 스킬 발동!");
        }
        else if (ClickTime >= _playerController.PlayerSO.SkillTime / 3 && ClickTime < 2 * (_playerController.PlayerSO.SkillTime / 3))
        {
            Debug.Log("1단계 스킬 발동!");
            _animator.SetTrigger("MeleeSkill");
            StartCoroutine(_playerAnimationEvent.COStartMeleeSkill());
            StartCoroutine(_playerAnimationEvent.COActiveMeleeSkillCollider(_skillSO));
        }
    }

    private void Start()
    {
        _playerController = GameManager.I.PlayerManager.Player.GetComponent<PlayerController>();
        _playerSpriteRenderer = _playerController.transform.GetChild(0).GetComponent<SpriteRenderer>();
        _animator = _playerController.transform.GetChild(0).GetComponent<Animator>();
        _playerAnimationEvent = _playerController.transform.GetChild(0).GetComponent<PlayerAnimationEvent>();
        _collidersTransform = _playerController.transform.GetChild(1).GetComponent<Transform>();
        _skillSO = GameManager.I.DataManager.GameDataSO.SkillFirst;

        _collidersPositionX = _collidersTransform.localPosition.x;
        _collidersPositionY = _collidersTransform.localPosition.y;

        if (_playerController.PlayerSO.Speed > 0)
        {
            _playerSpriteRenderer.flipX = false;

            if (_collidersPositionX < 0)
            {
                _collidersTransform.localPosition = new Vector3(-_collidersPositionX, _collidersPositionY, 0);
            }
        }
        else
        {
            _playerSpriteRenderer.flipX = true;

            if (_collidersPositionX > 0)
            {
                _collidersTransform.localPosition = new Vector3(-_collidersPositionX, _collidersPositionY, 0);
            }
        }
    }

    private void Update()
    {
        if (IsClick)
        {
            ClickTime += Time.deltaTime;
            if(ClickTime >= (_playerController.PlayerSO.SkillTime / 4f))
            _playerController.GetComponent<PlayerAttackState>().time = 0f;
        }
        else
        {
            ClickTime = 0;
        }
    }
}
