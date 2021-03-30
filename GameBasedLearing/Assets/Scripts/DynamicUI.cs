using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DynamicUI : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TextMeshProUGUI incorrectText;
    [SerializeField] private GameObject showGraphButon;
    [SerializeField] private GameObject hideGraphButton;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject cherryAddition;
    private GlobalDataHolder globalDataHolder;
    private string originalWrongPathText;
    private int cherryCount = 0;

    private void Start()
    {
        globalDataHolder = GameObject.Find("GlobalDataHolder").GetComponent<GlobalDataHolder>();
        cherryCount = globalDataHolder.GetCherries();
        globalDataHolder.DisplayCherryCount();
        originalWrongPathText = incorrectText.text;
        incorrectText.text = "";
        SetSliderUnactive();
        SetShowGraphButtonUnactive();
        SetHideGraphButtonUnactive();
        cherryAddition.SetActive(false);
    }

    public void WinGame()
    {
        StartCoroutine("WaitToWin");
    }

    private IEnumerator WaitToWin()
    {
        yield return new WaitForSeconds(3f);
        winPanel.SetActive(true);
    }

    public void ReplayGame()
    {
        winPanel.SetActive(false);
    }
 
    public void SetWrongPathText()
    {
        incorrectText.color = Color.white;
        incorrectText.text = originalWrongPathText;
        StartCoroutine(FadeOutRoutine(incorrectText,1f));
    }

    public void ChangeWrongPathText(string message,float time)
    {
        incorrectText.color = Color.white;
        incorrectText.text = message;
        StartCoroutine(FadeOutRoutine(incorrectText, time));
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

    public void SetSliderUnactive()
    {
        slider.gameObject.SetActive(false);
    }

    public void SetSliderActive()
    {

        slider.gameObject.SetActive(true);
    }

    public void ShowCherryAdd(int number)
    {
        cherryAddition.SetActive(true);
        cherryAddition.GetComponentInChildren<TextMeshProUGUI>().text = number + "+";
        cherryCount+=number;
        globalDataHolder.SetCherries(cherryCount);
        globalDataHolder.DisplayCherryCount();    
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

