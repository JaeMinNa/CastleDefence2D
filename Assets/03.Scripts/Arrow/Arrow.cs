using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] CastleSO _castleSO;
    private Rigidbody2D _rigidbody;
    private float _originAngle;
    

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.AddForce(transform.right * _castleSO.Speed, ForceMode2D.Impulse);
        _originAngle = transform.rotation.eulerAngles.z;
    }

    private void OnEnable()
    {
        if (_rigidbody != null)
        {
            transform.rotation = Quaternion.Euler(0, 0, _originAngle);
            _rigidbody.AddForce(transform.right * _castleSO.Speed, ForceMode2D.Impulse);
        }
    }

    private void Update()
    {
        transform.right = _rigidbody.velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Debug.Log("화살 적중!");
            GameManager.I.SoundManager.StartSFX("ArrowHit", collision.transform.position);
            collision.transform.GetComponent<EnemyController>().Ishit = true;
            Vector2 _dir = collision.transform.position - transform.position;
            collision.transform.GetComponent<EnemyController>().Hp -= _castleSO.Atk;
            if (_dir.x > 0)
            {
                GameManager.I.ObjectPoolManager.ActiveDamage("DamageText", collision.transform.position - new Vector3(0, 2, 0), (int)_castleSO.Atk, 255);
                collision.GetComponent<EnemyController>().Rigdbody.AddForce(new Vector2(1, 1) * _castleSO.NuckbackPower, ForceMode2D.Impulse);
            }
            else
            {
                GameManager.I.ObjectPoolManager.ActiveDamage("DamageText", collision.transform.position - new Vector3(0, 2, 0), (int)_castleSO.Atk, 255);
                collision.GetComponent<EnemyController>().Rigdbody.AddForce(new Vector2(-1, 1) * _castleSO.NuckbackPower, ForceMode2D.Impulse);
            }
            gameObject.SetActive(false);
        }
        else if(collision.CompareTag("Ground"))
        {
            Debug.Log("땅 적중!");
            gameObject.SetActive(false);
        }
    }
}
