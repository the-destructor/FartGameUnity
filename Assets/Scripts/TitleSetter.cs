using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Collections.Generic;
using TMPro;

public class TitleSetter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textElement; // Drag your UI text here
    [SerializeField] private float fadeDuration = 1.0f;   // Seconds to fade in/out
    [SerializeField] private float waitDuration = 2.0f;   // Seconds to stay visible
    public List<string> Titles = new List<string>();

    void Start()
    {
        
        if (textElement != null)
        {
            textElement.text = Titles[SceneManager.GetActiveScene().buildIndex];
            StartCoroutine(FadeSequence());
        }
    }

    private IEnumerator FadeSequence()
    {
        // 1. Force text to start fully transparent
        SetTextAlpha(0);

        // 2. Fade In
        yield return StartCoroutine(FadeText(0f, 1f));

        // 3. Wait at full opacity
        yield return new WaitForSeconds(waitDuration);

        // 4. Fade Out
        yield return StartCoroutine(FadeText(1f, 0f));
    }

    private IEnumerator FadeText(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color originalColor = textElement.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float currentAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            
            // Apply the new alpha to the text color
            textElement.color = new Color(originalColor.r, originalColor.g, originalColor.b, currentAlpha);
            yield return null; // Wait for the next frame
        }

        // Ensure the final alpha is perfectly set
        textElement.color = new Color(originalColor.r, originalColor.g, originalColor.b, endAlpha);
    }

    // Helper method to set initial transparency
    private void SetTextAlpha(float alpha)
    {
        Color c = textElement.color;
        textElement.color = new Color(c.r, c.g, c.b, alpha);
    }
}
