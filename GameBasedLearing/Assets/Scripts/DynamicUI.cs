///BSD 3 - Clause License

/// Copyright(c) 2021, ewanpandelus
///All rights reserved.

///Redistribution and use in source and binary forms, with or without
///modification, are permitted provided that the following conditions are met:

///1.Redistributions of source code must retain the above copyright notice, this
///list of conditions and the following disclaimer.

///2. Redistributions in binary form must reproduce the above copyright notice,
///this list of conditions and the following disclaimer in the documentation
///and/or other materials provided with the distribution.

///3. Neither the name of the copyright holder nor the names of its
///contributors may be used to endorse or promote products derived from
///this software without specific prior written permission.

///THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
///AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
///IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
///DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
///FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
///DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
///SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
///CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
///OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
///OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE

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

