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

    [Header("Setting")]
    [SerializeField] private GameObject _setting;

    private void Start()
    {
        _coinText.text = GameManager.I.DataManager.GameDataSO.Coin.ToString();
        _stageText.text = "Stage " + GameManager.I.DataManager.GameDataSO.Stage.ToString();
        _playerLevelText.text = "Lv " + _playerSO.Level.ToString();
        _castleLevelText.text = "Lv " + _castleSO.Level.ToString();
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
}
