using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageController : MonoBehaviour
{
    [Header("Castle")]
    [SerializeField] private CastleController _castleController;

    [Header("Text")]
    [SerializeField] private TMP_Text _stageText;
    [SerializeField] private TMP_Text _timeText;

    [Header("Pause")]
    [SerializeField] private GameObject _pause;

    [Header("GameClear")]
    [SerializeField] private GameObject _gameClear;

    [Header("GameOver")]
    [SerializeField] private GameObject _gameOver;

    private float _time;
    private bool _soundStart;
    private bool _gameFinish;
    
    private void Start()
    {
        _soundStart = false;
        _gameFinish = false;
        _time = 60f;
        _stageText.text = "STAGE " + GameManager.I.DataManager.GameDataSO.Stage.ToString();
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
        else if(_time <= 20f && !_soundStart)
        {
            GameManager.I.SoundManager.StartSFX("Danger");
            _soundStart = true;
            Debug.Log("Danger Time");
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
        _gameClear.gameObject.SetActive(true);
    }

    public void GameClearInactive()
    {
        _gameClear.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GameOverActive()
    {
        Time.timeScale = 0f;
        _gameOver.gameObject.SetActive(true);
    }

    public void GameOverInactive()
    {
        _gameOver.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
