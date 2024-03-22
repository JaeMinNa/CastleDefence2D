using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    public TMP_Text Text;
    [SerializeField] private float _speed;
    [SerializeField] private float _inactiveTime;

    private void Start()
    {
        StartCoroutine(COInactiveText(_inactiveTime));
    }

    private void OnEnable()
    {
        StartCoroutine(COInactiveText(_inactiveTime));
    }

    void Update()
    {
        transform.position += new Vector3(0, 1, 0) * Time.deltaTime * _speed;
    }

    IEnumerator COInactiveText(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
