using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private GameObject[] _tutorials;

    private GameData _gameData;

    private void Start()
    {
        _gameData = GameManager.I.DataManager.GameData;

        if (_gameData.TutorialCount == 1)
        {
            StartCoroutine(COStartTutorial(1, 0));
            StartCoroutine(COStartTutorial(2, 3));
            StartCoroutine(COStartTutorial(3, 7));
        }
        else if(_gameData.TutorialCount == 6)
        {
            StartCoroutine(COStartTutorial(6, 0));
            StartCoroutine(COStartTutorial(7, 3));
            StartCoroutine(COStartTutorial(8, 6));
            StartCoroutine(COStartTutorial(9, 9));
        }
    }

    private void StartTutorial(int count)
    {
        GameManager.I.SoundManager.StartSFX("ButtonClick");
        Time.timeScale = 0f;
        _gameData.TutorialCount = count;
        _tutorials[count - 1].SetActive(true);
    }

    private void StopTutorial(int count)
    {
        _tutorials[count - 1].SetActive(false);
        _gameData.TutorialCount++;
        if(_gameData.TutorialCount != 6) Time.timeScale = 1f;
    }

    public IEnumerator COStartTutorial(int count, float startTime)
    {
        yield return new WaitForSeconds(startTime);
        StartTutorial(count);

        if(count == 7) yield return new WaitForSecondsRealtime(8);
        else yield return new WaitForSecondsRealtime(5);

        StopTutorial(count);
        if (count == 5)
        {
            GameManager.I.DataManager.DataSave();
            transform.gameObject.SetActive(false);
        }
        else if (count == 9)
        {
            GameManager.I.DataManager.DataSave();
            transform.gameObject.SetActive(false);
        }
    }
}
