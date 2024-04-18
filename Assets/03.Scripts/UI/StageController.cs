using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageController : MonoBehaviour
{
    [Header("Danger Time")]
    public float DangerTimeSpeedRatio = 1.5f;
    public float DangerTimeAtkRatio = 1.5f;
    [SerializeField] private GameObject _dangerTimePanel;
    [SerializeField] private float _dangerTimePanelActiveTime = 1f;

    [Header("Castle")]
    [SerializeField] private CastleController _castleController;
    [SerializeField] private Collider2D[] _targets;

    [Header("Text")]
    [SerializeField] private TMP_Text _stageText;
    [SerializeField] private TMP_Text _timeText;

    [Header("Pause")]
    [SerializeField] private GameObject _pause;
    [SerializeField] private SoundController _soundController;

    [Header("GameClear")]
    [SerializeField] private GameObject _gameClear;
    [SerializeField] private TMP_Text _gameClearCoinText;
    [SerializeField] private TMP_Text _gameClearSkillDrawText;
    [SerializeField] private TMP_Text _gameClearStageText;
    [SerializeField] private GameObject _star3;
    [SerializeField] private GameObject _star2;
    [SerializeField] private GameObject _star1;

    [Header("GameOver")]
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private TMP_Text _gameOverCoinText;
    [SerializeField] private TMP_Text _gameOverStageText;

    [Header("Tutorial")]
    [SerializeField] private GameObject _tutorialPanel;

    [Header("Ad")]
    public bool IsAd;

    [HideInInspector] public bool IsDangerTime;
    private CastleData _castleData;
    private DataWrapper _dataWrapper;
    private GameData _gameData;
    private TutorialController _tutorialController;
    private float _time;
    private bool _gameFinish;
    private string _currentScene;
    private int _currentStage;
    private LayerMask _layerMask;
    private Vector2 _dir;
    private int _adCount;

    private void Start()
    {
        IsDangerTime = false;
        _gameFinish = false;
        IsAd = false;
        _time = 60f;
        _adCount = 0;
        _castleData = GameManager.I.DataManager.CastleData;
        _currentStage = GameManager.I.DataManager.GameData.Stage;
        _stageText.text = "STAGE " + _currentStage.ToString();
        _currentScene = GameManager.I.ScenesManager.CurrentSceneName;
        _dataWrapper = GameManager.I.DataManager.DataWrapper;
        _gameData = GameManager.I.DataManager.GameData;
        _tutorialController = _tutorialPanel.GetComponent<TutorialController>();
        _layerMask = LayerMask.NameToLayer("Enemy");
        SoundSetting();
        GameManager.I.SoundManager.StartBGM("BattleMap0");

        if(_gameData.TutorialCount == 1)
        {
            _tutorialPanel.SetActive(true);
        }
        if(_gameData.Stage == 1)
        {
            PlayerPrefs.SetInt("IsTutorial", 1);
        }
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
            
            if(_gameData.Stage == 1) StartCoroutine(_tutorialController.COStartTutorial(4, 0));
            
            StartCoroutine(COStartDangerTime());
        }
    }

    public void PauseActive()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        Time.timeScale = 0f;
        _pause.gameObject.SetActive(true);
    }

    public void PauseInactive()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        _pause.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GameClearActive()
    {
        GameManager.I.SoundManager.StartSFX("Buy");
        Time.timeScale = 0f;
        GameManager.I.DataManager.GameData.Stage++;

        if(_adCount == 1)
        {
            _star1.SetActive(true);
            GameManager.I.DataManager.CoinUpdate(500);
        }
        else
        {
            if (_castleController.Hp >= (_castleData.Hp / 3) * 2)
            {
                _star3.SetActive(true);
                GameManager.I.DataManager.CoinUpdate(1500);
            }
            else if (_castleController.Hp >= _castleData.Hp / 3)
            {
                _star2.SetActive(true);
                GameManager.I.DataManager.CoinUpdate(1000);
            }
            else
            {
                _star1.SetActive(true);
                GameManager.I.DataManager.CoinUpdate(500);
            }
        }

        GameManager.I.DataManager.GameData.Coin += GameManager.I.DataManager.CurrentStageCoin;

        if (_currentStage % 5 == 0)
        {
            EnemyLevelUp();
            GetSkillDrawCount();
            _gameClearSkillDrawText.text = "스킬 뽑기 + 1";
        }

        _gameClearStageText.text = "STAGE " + _currentStage.ToString();
        _gameClearCoinText.text = "Coin + " + GameManager.I.DataManager.CurrentStageCoin.ToString();
        _gameClear.gameObject.SetActive(true);

        if (_gameData.TutorialCount == 5)
        {
            StartCoroutine(_tutorialController.COStartTutorial(5, 0));
        }
        GameManager.I.DataManager.DataSave();
    }

    public void GameOverActive()
    {
        GameManager.I.SoundManager.StartSFX("GameOver");
        _tutorialPanel.SetActive(false);
        _dangerTimePanel.SetActive(false);
        Time.timeScale = 0f;
        _gameData.TutorialCount = 1;
        _gameOverCoinText.text = "Coin : " + GameManager.I.DataManager.CurrentStageCoin.ToString();
        _gameOverStageText.text = "STAGE " + _currentStage.ToString();
        _gameOver.gameObject.SetActive(true);
        GameManager.I.DataManager.DataSave();
    }

    public void NextSceneButton()
    {
        if (_gameData.TutorialCount == 6) return;

        GameManager.I.SoundManager.StartSFX("ButtonClick");
        Time.timeScale = 1f;
        GameManager.I.DataManager.GameData.Coin += GameManager.I.DataManager.CurrentStageCoin;
        GameManager.I.ScenesManager.SceneMove(_currentScene);
    }

    public void RetryButton()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        Time.timeScale = 1f;
        GameManager.I.DataManager.GameData.Coin += GameManager.I.DataManager.CurrentStageCoin;
        GameManager.I.ScenesManager.SceneMove(_currentScene);
    }

    public void LobySceneButton()
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        Time.timeScale = 1f;
        GameManager.I.DataManager.GameData.Coin += GameManager.I.DataManager.CurrentStageCoin;
        GameManager.I.ScenesManager.SceneMove("LobyScene");
    }

    public void AdButton()
    {
        if(!IsAd)
        {
            IsAd = true;
            if (_adCount >= 1)
            {
                GameManager.I.SoundManager.StartSFX("ButtonClickMiss");
                return;
            }

            GameManager.I.SoundManager.StartSFX("ButtonClick");
            GameManager.I.AdsManager.LoadRewardedAd();
        }
    }

    public void AdReword()
    {
        Time.timeScale = 1f;
        int layerMask = (1 << _layerMask);  // Layer 설정
        _targets = Physics2D.OverlapCircleAll(new Vector3(0, 2, 0), 15, layerMask);

        for (int i = 0; i < _targets.Length; i++)
        {
            _dir = _targets[i].gameObject.transform.position - new Vector3(0, 2, 0);

            if (_dir.x > 0)
            {
                _targets[i].gameObject.GetComponent<EnemyController>().Rigdbody.AddForce(new Vector2(1, 1) * 5, ForceMode2D.Impulse);
            }
            else
            {
                _targets[i].gameObject.GetComponent<EnemyController>().Rigdbody.AddForce(new Vector2(-1, 1) * 5, ForceMode2D.Impulse);
            }
        }

        GameManager.I.SoundManager.StartSFX("Nuckback");
        _castleController.Hp = _castleData.Hp / 2;
        _castleController.CastleHpUpdate();

        _gameOver.gameObject.SetActive(false);
        _adCount++;
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(new Vector3(0, 2, 0), 15);
    //}

    IEnumerator COStartDangerTime()
    {
        _dangerTimePanel.SetActive(true);
        yield return new WaitForSeconds(_dangerTimePanelActiveTime);
        _dangerTimePanel.SetActive(false);
    }

    private void EnemyLevelUp()
    {
        for (int i = 0; i < _dataWrapper.EnemyData.Length; i++)
        {
            _dataWrapper.EnemyData[i].Atk += 2;
            _dataWrapper.EnemyData[i].Hp += 2;
        }
    }
    private void GetSkillDrawCount()
    {
        GameManager.I.DataManager.GameData.SkillDrawCount++;
    }

    private void SoundSetting()
    {
        _soundController.BGMSlider.value = GameManager.I.DataManager.GameData.BGMVolume;
        _soundController.SFXSlider.value = GameManager.I.DataManager.GameData.SFXVolume;
        _soundController.SFXControll();
        _soundController.BGMControll();
    }
}
