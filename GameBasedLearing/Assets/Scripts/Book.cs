using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Book : MonoBehaviour
{
    AudioManager AudioManagement;
    public Animator animator;
    [SerializeField] TextMeshProUGUI[] page1Texts = new TextMeshProUGUI[4];
    [SerializeField] TextMeshProUGUI[] page2Texts = new TextMeshProUGUI[4];
    Color black = new Color(0f, 0f, 0f, 255f);
    private bool turned = false;

    void Start()
    {
        AudioManagement = AudioManager.instance;
        this.animator.speed = 1.25f;
    }

    private void  PageTurnAnim()
    {
        AudioManagement.Play("FlipPage");
        animator.SetBool("Turn",true);
        animator.SetBool("TurnBack", false);
    }
    private void PageTurnBackAnim()
    {
        AudioManagement.Play("FlipPage");
        animator.SetBool("TurnBack", true);
        animator.SetBool("Turn", false) ;
    }
    public void PageTurn()
    {
      
        if (!turned)
        {
            FadeOutBookText(page1Texts);
            PageTurnAnim();
            FadeInBookText(page2Texts);
            turned = true;
        }
      
      
   
    }
    public void PageTurnBack()
    {
  
        if (turned)
        {
            FadeOutBookText(page2Texts);
            PageTurnBackAnim();
            FadeInBookText(page1Texts);
            turned = false;
        }


    }
   
    private void FadeOutBookText(TextMeshProUGUI[] fadeOutTexts)
    {
        foreach (TextMeshProUGUI text in fadeOutTexts)
        {
            if (text != null)
            {
                StartCoroutine(FadeOutRoutine(text, 0.25f));
            }
        }
       
    }
    private void FadeInBookText(TextMeshProUGUI[] fadeInTexts)
    {
        foreach (TextMeshProUGUI text in fadeInTexts)
        {
            if (text != null)
            {
                StartCoroutine(FadeInRoutine(text, 0.25f));
            }
        
        }
   
    }
    private IEnumerator FadeOutRoutine(TextMeshProUGUI text, float waitTime)
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
    private IEnumerator FadeInRoutine(TextMeshProUGUI text, float waitTime)
    {

        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(waitTime);
        }
        Color originalColor = text.color;
        for (float t = 0.01f; t < 0.6f; t += Time.deltaTime)
        {
            text.color = Color.Lerp(originalColor, Color.black, Mathf.Min(1, t / 0.6f));
            yield return null;
        }
    }
}