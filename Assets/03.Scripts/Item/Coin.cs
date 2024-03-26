using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _coinPrice = 1000;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.I.DataManager.CoinUpdate(_coinPrice);
            GameManager.I.SoundManager.StartSFX("Coin");
            gameObject.SetActive(false);
        }
    }
}
