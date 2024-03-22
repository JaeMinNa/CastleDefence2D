using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
{
    private PlayerController _playerController;
    private SkillSO _skillSO;

    private void Start()
    {
        _playerController = GameManager.I.PlayerManager.Player.GetComponent<PlayerController>();
        _skillSO = GameManager.I.DataManager.GameDataSO.MeleeSkill;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Vector2 _dir = collision.transform.position - _playerController.transform.position;
            collision.transform.GetComponent<EnemyController>().Ishit = true;

            if(transform.CompareTag("AttackCollider"))
            {
                collision.transform.GetComponent<EnemyController>().Hp -= _playerController.PlayerSO.Atk;
                if (_dir.x > 0)
                {
                    collision.GetComponent<EnemyController>().Rigdbody.AddForce(new Vector2(1, 1) * _playerController.PlayerSO.NuckbackPower, ForceMode2D.Impulse);
                }
                else
                {
                    collision.GetComponent<EnemyController>().Rigdbody.AddForce(new Vector2(-1, 1) * _playerController.PlayerSO.NuckbackPower, ForceMode2D.Impulse);
                }
            }
            else if(transform.CompareTag("StrikeCollider"))
            {
                collision.transform.GetComponent<EnemyController>().Hp -= _skillSO.Atk;
                if (_dir.x > 0)
                {
                    collision.GetComponent<EnemyController>().Rigdbody.AddForce(new Vector2(1, 1) * _skillSO.NuckbackPower, ForceMode2D.Impulse);
                }
                else
                {
                    collision.GetComponent<EnemyController>().Rigdbody.AddForce(new Vector2(-1, 1) * _skillSO.NuckbackPower, ForceMode2D.Impulse);
                }
            }

            Debug.Log("Àû Hp : " + collision.transform.GetComponent<EnemyController>().Hp);
        }
    }
}
