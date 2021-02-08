using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DynamicUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winningText;
    [SerializeField] private TextMeshProUGUI incorrectText;
    [SerializeField] private GameObject replayButton;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject showGraphButon;
    [SerializeField] private GameObject hideGraphButton;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject cherryAddition;
    private GlobalDataHolder globalDataHolder;
    private string originalWinningPathText;
    private string originalWrongPathText;
    // Start is called before the first frame update
    void Start()
    {
        globalDataHolder = GameObject.Find("GlobalDataHolderData").GetComponent<GlobalDataHolder>();
        originalWinningPathText = winningText.text;
        originalWrongPathText = incorrectText.text;
        winningText.text = "";
        incorrectText.text = "";
        SetButtonsUnactive();
        SetSliderUnactive();
        SetShowGraphButtonUnactive();
        SetHideGraphButtonUnactive();
        cherryAddition.SetActive(false);
        
    }
    public void SetWinningPathText()
    {
        winningText.color = Color.white;
        winningText.text = originalWinningPathText;
    }
    public void FadeOutWinningPathtext()
    {
        StartCoroutine(FadeOutRoutine(winningText,0.2f));
    }
    public void SetWrongPathText()
    {
        incorrectText.color = Color.white;
        incorrectText.text = originalWrongPathText;
        StartCoroutine(FadeOutRoutine(incorrectText,1f));
    }
    public void SetShowGraphButtonActive()
    {
        if(hideGraphButton.activeInHierarchy == false)
        {
            showGraphButon.SetActive(true);
        }
      
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
    public void ShowCherryAdd(int number)
    {
        cherryAddition.SetActive(true);
        cherryAddition.GetComponentInChildren<TextMeshProUGUI>().text = number + "+";
        globalDataHolder.SetCherries(globalDataHolder.GetCherries() + number);
        
       
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

