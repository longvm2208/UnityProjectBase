using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class RendererExtensions
{
    public static void ChangeAlpha(this SpriteRenderer sr, float alpha)
    {
        if (alpha >= 0f && alpha <= 1f)
        {
            Color color = sr.color;
            color.a = alpha;
            sr.color = color;
        }
    }

    public static void ChangeAlpha(this TMP_Text text, float alpha)
    {
        if (alpha >= 0f && alpha <= 1f)
        {
            Color color = text.color;
            color.a = alpha;
            text.color = color;
        }
    }

    public static void ChangeAlpha(this Image image, float alpha)
    {
        if (alpha >= 0f && alpha <= 1f)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }
}
