using System.Collections;
using UnityEngine;

public class ItemNudge : MonoBehaviour
{
    private WaitForSeconds _wait;
    private bool _isAnimating = false;
    private void Awake()
    {
        _wait = new WaitForSeconds(0.04f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isAnimating)
        {
            if (this.gameObject.transform.position.x < collision.gameObject.transform.position.x)
            {
                StartCoroutine(RotateAntiClock());
            }
            else
            {
                StartCoroutine(RotateClock());
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!_isAnimating)
        {
            if (this.gameObject.transform.position.x > collision.gameObject.transform.position.x)
            {
                StartCoroutine(RotateAntiClock());
            }
            else
            {
                StartCoroutine(RotateClock());
            }
        }
    }
    private IEnumerator RotateAntiClock()
    {
        _isAnimating = true;
        for (int i = 0; i < 4; ++i)
        {
            this.gameObject.transform.GetChild(0).Rotate(0, 0, 2.0f);
            yield return _wait;
        }
        for (int i = 0; i < 5; ++i)
        {
            this.gameObject.transform.GetChild(0).Rotate(0, 0, -2.0f);
            yield return _wait;
        }
        gameObject.transform.GetChild(0).Rotate(0, 0, 2.0f);
        yield return _wait;
        _isAnimating = false;
    }
    private IEnumerator RotateClock()
    {
        _isAnimating = true;
        for (int i = 0; i < 4; ++i)
        {
            this.gameObject.transform.GetChild(0).Rotate(0, 0, -2.0f);
            yield return _wait;
        }
        for (int i = 0; i < 5; ++i)
        {
            this.gameObject.transform.GetChild(0).Rotate(0, 0, 2.0f);
            yield return _wait;
        }
        gameObject.transform.GetChild(0).Rotate(0, 0, -2.0f);
        yield return _wait;
        _isAnimating = false;
    }
}
