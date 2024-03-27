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

    private void Start()
    {
        _coinText.text = GameManager.I.DataManager.GameDataSO.Coin.ToString();
        _stageText.text = "Stage " + GameManager.I.DataManager.GameDataSO.Stage.ToString();
        _playerLevelText.text = "Lv " + _playerSO.Level.ToString();
        _castleLevelText.text = "Lv " + _castleSO.Level.ToString();
        _playerExpSlider.value = _playerSO.CurrentExp / _playerSO.MaxExp;
        _castleExpSlider.value = _castleSO.CurrentExp / _castleSO.MaxExp;
    }

    public void BattleSceneButton()
    {
        GameManager.I.ScenesManager.SceneMove("BattleScene0");
    }

    public void ActiveSetting()
    {
        _setting.SetActive(true);
    }

    public void InactiveSetting()
    {
        _setting.SetActive(false);
    }

    public void PlayerExpButton()
    {
        if(GameManager.I.DataManager.GameDataSO.Coin >= _expPrice)
        {
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
    }

    public void CastleExpButton()
    {
        if (GameManager.I.DataManager.GameDataSO.Coin >= _expPrice)
        {
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
    }

    public void PlayerLevelUp()
    {
        _playerSO.Speed += 0.1f;
        _playerSO.Atk += 1f;
    }

    public void CastleLevelUp()
    {
        _castleSO.Atk++;
        _castleSO.Hp += 10f;
    }
}
