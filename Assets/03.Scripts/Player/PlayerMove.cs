using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public StatData PlayerSO;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        transform.position += new Vector3(PlayerSO.Speed, 0, 0) * Time.deltaTime;

        if (PlayerSO.Speed > 0) _spriteRenderer.flipX = false;
        else _spriteRenderer.flipX = true;
    }
}
