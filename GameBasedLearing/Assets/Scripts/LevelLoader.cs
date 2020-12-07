﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private DynamicUI dynamicUI;

    private void Start()
    {

    }
    public void LoadGraph()
    {
        dynamicUI.FadeOutWinningPathtext();
        StartCoroutine(LoadChooseGraphScene());
    }
    public void LoadHardTSPButton()
    {
        StartCoroutine(LoadHardTSP());
    }

    public void LoadEasyTSPButton()
    {
        StartCoroutine(LoadEasyTSP());
    }
    public void LoadNQueens2Button()
    {
        StartCoroutine(loadNQueensLevel2());
    }
    public void LoadNQueens1Button()
    {
        StartCoroutine(loadNQueensLevel1());
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

    private IEnumerator loadNQueensLevel2()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene("NQueensLevel2");
    }

    private IEnumerator loadNQueensLevel1()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene("NQueensLevel1");
    }



}


