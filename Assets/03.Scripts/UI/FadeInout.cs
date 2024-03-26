using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    [SerializeField] private float _fadeTime; // 페이드 되는 시간

    private void Start()
    {
        // Fade In <-> Fade Out 무한 반복
        StartCoroutine(COFadeInOut());
    }

    private IEnumerator COFadeInOut()
    {
        while (true)
        {
            // 알파 값이 1에서 0으로 text가 보이지 않는다.
            yield return StartCoroutine(Fade(0.5f, 0));

            // 알파 값이 0에서 1로 text가 보인다.
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