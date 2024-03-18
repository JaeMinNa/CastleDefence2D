using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float minClickTime = 1; // 최소 클릭시간
    private float _clickTime; // 클릭 중인 시간
    private bool _isClick; // 클릭 중인지 판단 
    private GameObject _player;

    // 버튼 클릭이 시작했을 때
    public void ButtonDown()
    {
        _player.GetComponent<PlayerMove>().PlayerSO.Speed *= -1;

        _isClick = true;
    }

    // 버튼 클릭이 끝났을 때
    public void ButtonUp()
    {
        _isClick = false;

        if (_clickTime >= minClickTime)
        {
            Debug.Log("스킬 발동!");
        }
    }

    private void Start()
    {
        _player = GameManager.I.PlayerManager.Player;
    }

    private void Update()
    {
        if (_isClick)
        {
            _clickTime += Time.deltaTime;
        }
        else
        {
            _clickTime = 0;
        }
    }
}
