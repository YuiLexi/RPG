using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ObscuringItemFader : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }
    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        float currentAlpha = _spriteRenderer.color.a;
        float variation = 1f - currentAlpha;
        while (1f - currentAlpha > 0.01f)
        {
            currentAlpha += variation / Settings._fadeInSeconds * Time.deltaTime;
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, currentAlpha);
            yield return null;
        }
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 1f);
    }
    private IEnumerator FadeOutCoroutine()
    {
        float currentAlpha = _spriteRenderer.color.a;
        float variation = currentAlpha - Settings._targetAlpha;
        while (currentAlpha - Settings._targetAlpha > 0.01f)
        {
            currentAlpha -= variation / Settings._fadeOutSeconds * Time.deltaTime;
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, currentAlpha);
            yield return null;
        }
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, Settings._targetAlpha);
    }
}