﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Animator transition;
    public Animator GetAnimator()
    {
        return this.transition;
    }


}
