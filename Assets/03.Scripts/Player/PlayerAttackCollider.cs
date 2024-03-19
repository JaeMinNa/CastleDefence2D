using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
{
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GameManager.I.PlayerManager.Player.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<EnemyController>().Ishit = true;
            collision.transform.GetComponent<EnemyController>().Hp -= _playerController.PlayerSO.Atk;
            Vector2 _dir = collision.transform.position - _playerController.transform.position;
            if(_dir.x > 0)
            {
                collision.GetComponent<EnemyController>().Rigdbody.AddForce(new Vector2(1, 1) * _playerController.PlayerSO.NuckbackPower, ForceMode2D.Impulse);
            }
            else
            {
                collision.GetComponent<EnemyController>().Rigdbody.AddForce(new Vector2(-1, 1) * _playerController.PlayerSO.NuckbackPower, ForceMode2D.Impulse);
            }
            Debug.Log("Àû Hp : " + collision.transform.GetComponent<EnemyController>().Hp);
        }
    }
}
