using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageController : MonoBehaviour
{
    [SerializeField] private TMP_Text _stageText;
    [SerializeField] private TMP_Text _timeText;

    private float _time;

    private void Start()
    {
        _time = 60f;
        _stageText.text = "STAGE " + GameManager.I.DataManager.GameDataSO.Stage.ToString();
    }

    private void Update()
    {
        _time -= Time.deltaTime;
        _timeText.text = _time.ToString("N1");

        if(_time <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}
