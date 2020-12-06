using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DynamicUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winningPathText;
    [SerializeField] private TextMeshProUGUI wrongPathText;
    [SerializeField] private GameObject replayButton;
    [SerializeField] private GameObject continueButton;
    private string originalWinningPathText;
    private string originalWrongPathText;
    // Start is called before the first frame update
    void Start()
    {
        originalWinningPathText = winningPathText.text;
        originalWrongPathText = wrongPathText.text;
        winningPathText.text = "";
        wrongPathText.text = "";
        SetButtonsUnactive();
    }
    public void SetWinningPathText()
    {
        winningPathText.color = Color.white;
        winningPathText.text = originalWinningPathText;
    }
    public void FadeOutWinningPathtext()
    {
        StartCoroutine(FadeOutRoutine(winningPathText,0.2f));
    }
    public void SetWrongPathText()
    {
        wrongPathText.color = Color.white;
        wrongPathText.text = originalWrongPathText;
        StartCoroutine(FadeOutRoutine(wrongPathText,1f));
    }
    public void SetButtonsUnactive()
    {
        continueButton.SetActive(false);
        replayButton.SetActive(false);
    }
    public void SetButtonsActive()
    {
        continueButton.SetActive(true);
        replayButton.SetActive(true);
    }
    private IEnumerator FadeOutRoutine(TextMeshProUGUI text,float waitTime)
    {
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(waitTime);
        }
        Color originalColor = text.color;
        for (float t = 0.01f; t < 0.6f; t += Time.deltaTime)
        {
            text.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / 0.6f));
            yield return null;
        }

    }
}

