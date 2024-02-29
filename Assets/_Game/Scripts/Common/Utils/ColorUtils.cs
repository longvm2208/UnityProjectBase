using UnityEngine;

public class ColorUtils
{
    public static Color[] GenerateColors(int n)
    {
        Color[] colors = new Color[n];

        float hueStep = 1f / n; // Divide the hue spectrum evenly

        // Generate colors
        for (int i = 0; i < n; i++)
        {
            // Calculate hue for this color
            float hue = i * hueStep;

            // Generate a color with random saturation and value/brightness
            Color newColor = RandomColor(hue);

            // Ensure at least one of the RGB values is in the range [220, 255]
            while (newColor.r < 220 / 255f && newColor.g < 220 / 255f && newColor.b < 220 / 255f)
            {
                newColor = RandomColor(hue);
            }

            // Ensure generated color is not too similar to previous colors
            for (int j = 0; j < i; j++)
            {
                while (ColorSimilarity(newColor, colors[j]) < 0.2f)
                {
                    hue += 0.1f; // Increase hue slightly to make the color more distinct
                    newColor = RandomColor(hue);

                    // Ensure at least one of the RGB values is in the range [220, 255]
                    while (newColor.r < 220 / 255f && newColor.g < 220 / 255f && newColor.b < 220 / 255f)
                    {
                        newColor = RandomColor(hue);
                    }
                }
            }

            colors[i] = newColor;
        }

        return colors;
    }

    private static Color RandomColor(float hue)
    {
        return Color.HSVToRGB(hue, Random.Range(0.3f, 1f), Random.Range(0.6f, 1f));
    }

    private static float ColorSimilarity(Color color1, Color color2)
    {
        return Mathf.Abs(color1.r - color2.r) + Mathf.Abs(color1.g - color2.g) + Mathf.Abs(color1.b - color2.b);
    }
}
