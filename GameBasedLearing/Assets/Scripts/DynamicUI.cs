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
    [SerializeField] private GameObject showGraphButon;
    [SerializeField] private GameObject hideGraphButton;
    [SerializeField] private Slider slider;
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
        SetSliderUnactive();
        SetShowGraphButtonUnactive();
        SetHideGraphButtonUnactive();
        
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
    public void SetShowGraphButtonActive()
    {
        showGraphButon.SetActive(true);
    }
    public void SetShowGraphButtonUnactive()
    {
        showGraphButon.SetActive(false);
    }
    public void SetHideGraphButtonActive()
    {
        hideGraphButton.SetActive(true);
    }
    public void SetHideGraphButtonUnactive()
    {
        hideGraphButton.SetActive(false);
    }
    public void SetButtonsUnactive()
    {
        continueButton.SetActive(false);
        replayButton.SetActive(false);
    }
    public void SetSliderUnactive()
    {
        slider.gameObject.SetActive(false);
    }
    public void SetSliderActive()
    {

        slider.gameObject.SetActive(true);
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

