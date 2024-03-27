using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobyController : MonoBehaviour
{
    [Header("Coin")]
    [SerializeField] private TMP_Text _coinText;

    [Header("Stage")]
    [SerializeField] private TMP_Text _stageText;

    [Header("Level")]
    [SerializeField] private PlayerSO _playerSO;
    [SerializeField] private CastleSO _castleSO;
    [SerializeField] private TMP_Text _playerLevelText;
    [SerializeField] private TMP_Text _castleLevelText;
    [SerializeField] private Slider _playerExpSlider;
    [SerializeField] private Slider _castleExpSlider;
    [SerializeField] private int _expPrice;
    [SerializeField] private int _exp;

    [Header("Setting")]
    [SerializeField] private GameObject _setting;
    [SerializeField] private GameObject _reset;

    [Header("Info")]
    [SerializeField] private GameObject _playerInfo;
    [SerializeField] private TMP_Text _playerLvText;
    [SerializeField] private TMP_Text _playerExpText;
    [SerializeField] private TMP_Text _playerAtkText;
    [SerializeField] private TMP_Text _playerSpeedText;
    [SerializeField] private GameObject _castleInfo;
    [SerializeField] private TMP_Text _castleLvText;
    [SerializeField] private TMP_Text _castleExpText;
    [SerializeField] private TMP_Text _castleHpText;
    [SerializeField] private TMP_Text _castleAtkText;
    [SerializeField] private TMP_Text _castleTimeText;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _coinText.text = GameManager.I.DataManager.GameDataSO.Coin.ToString();
        _stageText.text = "Stage " + GameManager.I.DataManager.GameDataSO.Stage.ToString();
        _playerLevelText.text = "Lv " + _playerSO.Level.ToString();
        _castleLevelText.text = "Lv " + _castleSO.Level.ToString();
        _playerExpSlider.value = _playerSO.CurrentExp / _playerSO.MaxExp;
        _castleExpSlider.value = _castleSO.CurrentExp / _castleSO.MaxExp;
    }

    public void BattleScene0Button()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        GameManager.I.ScenesManager.SceneMove("BattleScene0");
    }

    public void BattleScene1Button()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClickMiss");
    }

    public void ActiveSetting()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _setting.SetActive(true);
    }

    public void InactiveSetting()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _setting.SetActive(false);
    }

    public void ActiveReset()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _reset.SetActive(true);
    }

    public void InactiveReset()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _reset.SetActive(false);
    }

    public void PlayerExpButton()
    {
        if(GameManager.I.DataManager.GameDataSO.Coin >= _expPrice)
        {
            GameManager.I.SoundManager.StartSFX("ButtonExp");
            GameManager.I.DataManager.GameDataSO.Coin -= _expPrice;
            _coinText.text = GameManager.I.DataManager.GameDataSO.Coin.ToString();
            _playerSO.CurrentExp += _exp;

            if (_playerSO.CurrentExp >= _playerSO.MaxExp)
            {
                _playerSO.CurrentExp -= _playerSO.MaxExp;
                _playerSO.Level++;
                _playerSO.MaxExp *= 1.5f;
                _playerLevelText.text = "Lv " + _playerSO.Level.ToString();
                PlayerLevelUp();
            }

            _playerExpSlider.value = _playerSO.CurrentExp / _playerSO.MaxExp;
        }
        else 
        {
            GameManager.I.SoundManager.StartSFX("ButtonClickMiss");
        }
    }

    public void CastleExpButton()
    {
        if (GameManager.I.DataManager.GameDataSO.Coin >= _expPrice)
        {
            GameManager.I.SoundManager.StartSFX("ButtonExp");
            GameManager.I.DataManager.GameDataSO.Coin -= _expPrice;
            _coinText.text = GameManager.I.DataManager.GameDataSO.Coin.ToString();
            _castleSO.CurrentExp += _exp;

            if (_castleSO.CurrentExp >= _castleSO.MaxExp)
            {
                _castleSO.CurrentExp -= _castleSO.MaxExp;
                _castleSO.Level++;
                _castleSO.MaxExp *= 1.5f;
                _castleLevelText.text = "Lv " + _castleSO.Level.ToString();
                CastleLevelUp();
            }

            _castleExpSlider.value = _castleSO.CurrentExp / _castleSO.MaxExp;
        }
        else
        {
            GameManager.I.SoundManager.StartSFX("ButtonClickMiss");
        }
    }

    private void PlayerLevelUp()
    {
        GameManager.I.SoundManager.StartSFX("LevelUp");
        _playerSO.Speed += 0.1f;
        _playerSO.Atk += 1f;
    }

    private void CastleLevelUp()
    {
        GameManager.I.SoundManager.StartSFX("LevelUp");
        _castleSO.Atk++;
        _castleSO.Hp += 10f;
        if(_castleSO.AttackCoolTime >= 0.5f)
        {
            _castleSO.AttackCoolTime -= 0.1f;
        }
    }

    public void DataReset()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        GameManager.I.DataManager.DataReset();
        Init();
        _reset.SetActive(false);
        _setting.SetActive(false);
    }

    public void ActivePlayerInfo()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _playerLvText.text = _playerSO.Level.ToString();
        _playerExpText.text = ((int)_playerSO.CurrentExp).ToString() + " / " + ((int)_playerSO.MaxExp).ToString();
        _playerAtkText.text = _playerSO.Atk.ToString();
        if(_playerSO.Speed > 0)
        {
            _playerSpeedText.text = _playerSO.Speed.ToString();
        }
        else
        {
            _playerSpeedText.text = (-_playerSO.Speed).ToString();
        }
        _playerInfo.SetActive(true);
    }

    public void InactivePlayerInfo()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _playerInfo.SetActive(false);
    }

    public void ActiveCastleInfo()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _castleLvText.text = _castleSO.Level.ToString();
        _castleExpText.text = ((int)_castleSO.CurrentExp).ToString() + " / " + ((int)_castleSO.MaxExp).ToString();
        _castleHpText.text = _castleSO.Hp.ToString();
        _castleAtkText.text = _castleSO.Atk.ToString();
        _castleTimeText.text = _castleSO.AttackCoolTime.ToString();
        _castleInfo.SetActive(true);
    }

    public void InactiveCastleInfo()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _castleInfo.SetActive(false);
    }
}
