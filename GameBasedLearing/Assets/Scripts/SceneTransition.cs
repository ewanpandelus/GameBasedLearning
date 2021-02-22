﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Animator transition;
    private GlobalDataHolder globalDataHolder;
    private GameObject cherryIcon;
    public Animator GetAnimator()
    {
        return this.transition;
    }
    private void Awake()
    {
     
        globalDataHolder = GameObject.Find("GlobalDataHolder").GetComponent<GlobalDataHolder>();
        globalDataHolder.LoadGlobalDataHolder();

        cherryIcon = GameObject.FindGameObjectWithTag("CherryIcon");
        globalDataHolder.SetCherryIcon(cherryIcon);
        globalDataHolder.DisplayCherryCount();
    }

}
