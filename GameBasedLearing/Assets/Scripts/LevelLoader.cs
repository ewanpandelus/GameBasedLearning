﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private DynamicUI dynamicUI;
    public string levelName;
    AudioManager AudioManagement;

    private void Awake()
    {
        AudioManagement = AudioManager.instance;
    }
    public void LoadGraph()
    {
        dynamicUI.FadeOutWinningPathtext();
        StartCoroutine(LoadChooseGraphScene());
    }
    public void LoadLevel()
    {
        StartCoroutine(LoadLevel(levelName));
    }
    private IEnumerator LoadLevel(string levelName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(levelName);
    }

    public void LoadEasyTSPButton()
    {
        StartCoroutine(LoadEasyTSP());
    }
    public void LoadNQueens2Button()
    {
        StartCoroutine(LoadNQueensLevel2());
    }
    public void LoadNQueens1Button()
    {
        StartCoroutine(LoadNQueensLevel1());
    }
    public void LoadBubbleSortButton()
    {
        StartCoroutine(LoadBubbleSortScene());
    }
    private IEnumerator LoadBubbleSortScene()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene("BubbleSort");
    }
    private IEnumerator LoadChooseGraphScene()
    { 
    transition.SetTrigger("Start");
    yield return new WaitForSecondsRealtime(1f);
    SceneManager.LoadScene("ComplexityGraph");
    }
    private IEnumerator LoadEasyTSP()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene("EasyTSP");
    }
    private IEnumerator LoadHardTSP()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene("HardTSP");
    }

    private IEnumerator LoadNQueensLevel2()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene("NQueensLevel2");
    }

    private IEnumerator LoadNQueensLevel1()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene("NQueensLevel1");
    }

    public void PlaySceneChangeSound()
    {
        AudioManagement.Play("ButtonPress");
    }

}


