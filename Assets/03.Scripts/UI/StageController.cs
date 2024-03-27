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
    [SerializeField] private GameObject _star3;
    [SerializeField] private GameObject _star2;
    [SerializeField] private GameObject _star1;

    [Header("GameOver")]
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private TMP_Text _gameOverCoinText;

    [HideInInspector] public bool IsDangerTime;
    private float _time;
    private bool _gameFinish;
    private string _currentScene;
    
    private void Start()
    {
        IsDangerTime = false;
        _gameFinish = false;
        _time = 60f;
        _stageText.text = "STAGE " + GameManager.I.DataManager.GameDataSO.Stage.ToString();
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
            //StopDangerTime();
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
        _gameClearCoinText.text = "Coin : " + GameManager.I.DataManager.CurrentStageCoin.ToString();

        if(_castleController.Hp >= (_castleController.CastleSO.Hp / 3) * 2)
        {
            _star3.SetActive(true);
        }
        else if(_castleController.Hp >= _castleController.CastleSO.Hp / 3)
        {
            _star2.SetActive(true);
        }
        else
        {
            _star1.SetActive(true);
        }

        _gameClear.gameObject.SetActive(true);
    }

    //public void GameClearInactive()
    //{
    //    _gameClear.gameObject.SetActive(false);
    //    Time.timeScale = 1f;
    //}

    public void GameOverActive()
    {
        Time.timeScale = 0f;
        _gameOverCoinText.text = "Coin : " + GameManager.I.DataManager.CurrentStageCoin.ToString();
        _gameOver.gameObject.SetActive(true);
    }

    //public void GameOverInactive()
    //{
    //    _gameOver.gameObject.SetActive(false);
    //    Time.timeScale = 1f;
    //}

    public void NextSceneButton()
    {
        Time.timeScale = 1f;
        GameManager.I.DataManager.GameDataSO.Stage++;
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
        GameManager.I.DataManager.GameDataSO.Coin += GameManager.I.DataManager.CurrentStageCoin;
        GameManager.I.ScenesManager.SceneMove("LobyScene");
    }

    IEnumerator COStartDangerTime()
    {
        _dangerTimePanel.SetActive(true);
        yield return new WaitForSeconds(_dangerTimePanelActiveTime);
        _dangerTimePanel.SetActive(false);
    }
}
