using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    [SerializeField] private float _fadeTime; // ���̵� �Ǵ� �ð�

    private void Start()
    {
        // Fade In <-> Fade Out ���� �ݺ�
        StartCoroutine(COFadeInOut());
    }

    private IEnumerator COFadeInOut()
    {
        while (true)
        {
            // ���� ���� 1���� 0���� text�� ������ �ʴ´�.
            yield return StartCoroutine(Fade(0.5f, 0));

            // ���� ���� 0���� 1�� text�� ���δ�.
            yield return StartCoroutine(Fade(0, 0.5f));
        }
    }

    private IEnumerator Fade(float start, float end)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / _fadeTime;

            Color color = transform.GetComponent<Image>().color;
            color.a = Mathf.Lerp(start, end, percent);
            transform.GetComponent<Image>().color = color;

            yield return null;
        }
    }
}