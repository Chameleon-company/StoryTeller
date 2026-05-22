using UnityEngine;
using System.Collections;
public class FadeImage : MonoBehaviour
{
    public SpriteRenderer sprite;
    public float FadeInTime = 0.7f;
    public float VisibleTime = 1.2f;
    public float FadeOutTime = 0.6f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(FadeSequence());
    }

    IEnumerator FadeSequence()
    {
        SetAlpha(0f);

        yield return StartCoroutine(Fade(0f,1f,FadeInTime));

        yield return new WaitForSeconds(VisibleTime);

       yield return StartCoroutine(Fade(1f,0f,FadeOutTime));
    }

    IEnumerator Fade(float from, float to, float time)
    {
        float t = 0f;
        while(t < time)
        {
            t += Time.deltaTime;
            float p = t / time;
            float alpha = Mathf.Lerp(from, to, p);
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(to);
    }

    void SetAlpha(float a)
    {
        if (sprite == null) return;

        Color c = sprite.color;

        c.a = a;
        sprite.color = c;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
