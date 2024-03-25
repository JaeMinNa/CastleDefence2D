using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private EnemySO _rangedEnemySO;
    private CastleController _castleController;
    private SpriteRenderer _bulletSpriteRenderer;

    void Start()
    {
        _castleController = GameObject.FindWithTag("Castle").GetComponent<CastleController>();
        _bulletSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

        if (transform.position.x > 0)
        {
            _bulletSpriteRenderer.flipX = true;
        }
        else
        {
            _bulletSpriteRenderer.flipX = true;
        }
    }

    private void OnEnable()
    {
        if (_castleController != null)
        {
            if (transform.position.x > 0)
            {
                _bulletSpriteRenderer.flipX = true;
            }
            else
            {
                _bulletSpriteRenderer.flipX = true;
            }
        }
    }

    void Update()
    {
        if(transform.position.x > 0)
        {
            transform.position += new Vector3(-_rangedEnemySO.BulletSpeed, 0, 0) * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(_rangedEnemySO.BulletSpeed, 0, 0) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Castle"))
        {
            _castleController.CastleHit(_rangedEnemySO.Atk);
            gameObject.SetActive(false);
        }
    }
}
