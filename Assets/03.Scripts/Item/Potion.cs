using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public enum PotionType
    {
        None,
        Hp,
        Speed,
        Power,
        CoolTime,
    }

    public PotionType Type;
    [SerializeField] private float _itemTime;
    [SerializeReference] private float _fadeTime;

    [Header("HpPotion")]
    [SerializeField] private int _hp;
    private CastleController _castleController;

    [Header("PowerPotion")]
    [SerializeField] private float _powerRatio;
    
    [Header("SpeedPotion")]
    [SerializeField] private float _speedRatio;

    [Header("CoolTimePotion")]
    [SerializeField] private float _coolTimeRatio;
    private AttackButton _attackButton;

    private PlayerController _playerController;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _playerSpriteRenderer;
    private BoxCollider2D _collider;

    private void Start()
    {
        if (GameManager.I.ScenesManager.CurrentSceneName != "LobyScene")
        {
            _castleController = GameObject.FindWithTag("Castle").GetComponent<CastleController>();
            _attackButton = GameObject.FindWithTag("AttackButton").GetComponent<AttackButton>();
            _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
            _collider = GetComponent<BoxCollider2D>();
            _playerController = GameManager.I.PlayerManager.Player.GetComponent<PlayerController>();
            _playerSpriteRenderer = _playerController.transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
    }

    IEnumerator COInactive(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    IEnumerator COPlayerStatReset()
    {
        yield return new WaitForSeconds(_itemTime);
        _playerController.Atk = GameManager.I.DataManager.PlayerData.Atk;
        _playerController.Speed = GameManager.I.DataManager.PlayerData.Speed;
        _attackButton.SkillCoolTime = GameManager.I.DataManager.PlayerData.SkillTime;
    }

    private IEnumerator COStopFade()
    {
        yield return new WaitForSeconds(_itemTime);
        StopAllCoroutines();
        _playerSpriteRenderer.color = Color.white;
    }

    private IEnumerator COFadeInOut(float coolTime, int a, int b, int c, int d)
    {
        while (true)
        {
            // 알파 값이 1에서 0으로 text가 보이지 않는다.
            yield return StartCoroutine(Fade(Color.white, new Color(a / 255f, b / 255f, c / 255f, d / 255f)));

            // 알파 값이 0에서 1로 text가 보인다.
            yield return StartCoroutine(Fade(new Color(a / 255f, b / 255f, c / 255f, d / 255f), Color.white));
        }
    }

    private IEnumerator Fade(Color start, Color end)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / _fadeTime;

            Color color = _playerSpriteRenderer.color;
            color = Color.Lerp(start, end, percent);
            _playerSpriteRenderer.color = color;

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Type == PotionType.Hp)
        {
            if (collision.CompareTag("Player"))
            {
                GameManager.I.SoundManager.StartSFX("Potion");
                _castleController.CastleHpRecovery(_hp);
                StartCoroutine(COInactive(0));
            }
        }
        else if(Type == PotionType.Power)
        {
            if (collision.CompareTag("Player"))
            {
                GameManager.I.SoundManager.StartSFX("Potion");
                StartCoroutine(COFadeInOut(_itemTime, 127, 0, 255, 255));
                StartCoroutine(COPlayerStatReset());
                StartCoroutine(COInactive(7));
                StartCoroutine(COStopFade());
                _playerController.Atk *= _powerRatio;
                _spriteRenderer.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0 / 255f);
                _collider.enabled = false;
            }
        }
        else if (Type == PotionType.Speed)
        {
            if (collision.CompareTag("Player"))
            {
                GameManager.I.SoundManager.StartSFX("Potion");
                StartCoroutine(COFadeInOut(_itemTime, 66, 179, 0, 255));
                StartCoroutine(COPlayerStatReset());
                StartCoroutine(COInactive(7));
                StartCoroutine(COStopFade());
                _playerController.Speed *= _speedRatio;
                _spriteRenderer.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0 / 255f);
                _collider.enabled = false;
            }
        }
        else if (Type == PotionType.CoolTime)
        {
            if (collision.CompareTag("Player"))
            {
                GameManager.I.SoundManager.StartSFX("Potion");
                StartCoroutine(COFadeInOut(_itemTime, 50, 50, 50, 255));
                StartCoroutine(COPlayerStatReset());
                StartCoroutine(COInactive(7));
                StartCoroutine(COStopFade());
                _attackButton.SkillCoolTime *= _coolTimeRatio;
                _spriteRenderer.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0 / 255f);
                _collider.enabled = false;
            }
        }

    }
}
