using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageController : MonoBehaviour
{
    [Header("Danger Time")]
    [field : SerializeField] public float DangerTimeSpeedRatio = 1.5f;
    [field : SerializeField] public float DangerTimeAtkRatio = 1.5f;
    [SerializeField] private GameObject _dangerTimePanel;
    [SerializeField] private float _dangerTimePanelActiveTime = 1f;

    [Header("Castle")]
    [SerializeField] private CastleController _castleController;

    [Header("Text")]
    [SerializeField] private TMP_Text _stageText;
    [SerializeField] private TMP_Text _timeText;

    [Header("Pause")]
    [SerializeField] private GameObject _pause;

    [Header("GameClear")]
    [SerializeField] private GameObject _gameClear;
    [SerializeField] private TMP_Text _gameClearCoinText;
    [SerializeField] private TMP_Text _gameClearStageText;
    [SerializeField] private GameObject _star3;
    [SerializeField] private GameObject _star2;
    [SerializeField] private GameObject _star1;

    [Header("GameOver")]
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private TMP_Text _gameOverCoinText;
    [SerializeField] private TMP_Text _gameOverStageText;

    [HideInInspector] public bool IsDangerTime;
    private float _time;
    private bool _gameFinish;
    private string _currentScene;
    private int _currentStage;
    
    private void Start()
    {
        IsDangerTime = false;
        _gameFinish = false;
        _time = 60f;
        _currentStage = GameManager.I.DataManager.GameDataSO.Stage;
        _stageText.text = "STAGE " + _currentStage.ToString();
        _currentScene = GameManager.I.ScenesManager.CurrentSceneName;
    }

    private void Update()
    {
        _time -= Time.deltaTime;
        _timeText.text = _time.ToString("N1");

        if(_time <= 0 && !_gameFinish)
        {
            _gameFinish = true;
            _time = 0;
            GameClearActive();
        }
        else if(_time <= 20f && !IsDangerTime)
        {
            IsDangerTime = true;
            GameManager.I.SoundManager.StartSFX("Danger");
            StartCoroutine(COStartDangerTime());
        }
    }

    public void PauseActive()
    {
        Time.timeScale = 0f;
        _pause.gameObject.SetActive(true);
    }

    public void PauseInactive()
    {
        _pause.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GameClearActive()
    {
        Time.timeScale = 0f;
        GameManager.I.DataManager.GameDataSO.Stage++;

        if (_castleController.Hp >= (_castleController.CastleSO.Hp / 3) * 2)
        {
            _star3.SetActive(true);
            GameManager.I.DataManager.CoinUpdate(1500);
        }
        else if(_castleController.Hp >= _castleController.CastleSO.Hp / 3)
        {
            _star2.SetActive(true);
            GameManager.I.DataManager.CoinUpdate(1000);
        }
        else
        {
            _star1.SetActive(true);
            GameManager.I.DataManager.CoinUpdate(500);
        }

        if (_currentStage % 5 == 0)
        {
            EnemyLevelUp();
        }

        _gameClearStageText.text = "STAGE " + _currentStage.ToString();
        _gameClearCoinText.text = "Coin : " + GameManager.I.DataManager.CurrentStageCoin.ToString();
        _gameClear.gameObject.SetActive(true);
    }

    public void GameOverActive()
    {
        _dangerTimePanel.SetActive(false);
        Time.timeScale = 0f;
        _gameOverCoinText.text = "Coin : " + GameManager.I.DataManager.CurrentStageCoin.ToString();
        _gameOverStageText.text = "STAGE " + _currentStage.ToString();
        _gameOver.gameObject.SetActive(true);
    }

    public void NextSceneButton()
    {
        Time.timeScale = 1f;
        GameManager.I.DataManager.GameDataSO.Coin += GameManager.I.DataManager.CurrentStageCoin;
        GameManager.I.ScenesManager.SceneMove(_currentScene);
    }

    public void RetryButton()
    {
        Time.timeScale = 1f;
        GameManager.I.DataManager.GameDataSO.Coin += GameManager.I.DataManager.CurrentStageCoin;
        GameManager.I.ScenesManager.SceneMove(_currentScene);
    }

    public void LobySceneButton()
    {
        Time.timeScale = 1f;
        GameManager.I.DataManager.GameDataSO.Stage++;
        GameManager.I.DataManager.GameDataSO.Coin += GameManager.I.DataManager.CurrentStageCoin;
        GameManager.I.ScenesManager.SceneMove("LobyScene");
    }

    IEnumerator COStartDangerTime()
    {
        _dangerTimePanel.SetActive(true);
        yield return new WaitForSeconds(_dangerTimePanelActiveTime);
        _dangerTimePanel.SetActive(false);
    }

    private void EnemyLevelUp()
    {
        EnemySO[] meleeEnemySO = GameManager.I.DataManager.MeleeEnemySO;
        EnemySO[] rangedEnemySO = GameManager.I.DataManager.RangedEnemySO;
        for (int i = 0; i < meleeEnemySO.Length; i++)
        {
            meleeEnemySO[i].Atk *= 1.2f;
            meleeEnemySO[i].Hp *= 1.2f;
        }
        for (int i = 0; i < rangedEnemySO.Length; i++)
        {
            rangedEnemySO[i].Atk *= 1.2f;
            rangedEnemySO[i].Hp *= 1.2f;
        }
    }
}
