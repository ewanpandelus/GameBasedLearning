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
[System.Serializable]
public class Book : MonoBehaviour
{
    AudioManager AudioManagement;
    public Animator animator;
    [SerializeField] TextMeshProUGUI[] page1Texts = new TextMeshProUGUI[4];
    [SerializeField] TextMeshProUGUI[] page2Texts = new TextMeshProUGUI[4];
    Color black = new Color(0f, 0f, 0f, 255f);
    private bool turned = false;  

    private void Start()
    {
        AudioManagement = AudioManager.instance;
        this.animator.speed = 1.25f;
    }

    private void PageTurnAnim()
    {
        AudioManagement.Play("FlipPage");
        animator.SetBool("Turn", true);
        animator.SetBool("TurnBack", false);
    }

    private void PageTurnBackAnim()
    {
        AudioManagement.Play("FlipPage");
        animator.SetBool("TurnBack", true);
        animator.SetBool("Turn", false);
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