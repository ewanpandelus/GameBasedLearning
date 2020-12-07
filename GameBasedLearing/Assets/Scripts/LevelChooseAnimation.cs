using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelChooseAnimation : Selectable
{

    [SerializeField] private new Animator animator;


    private void Update()
    {
       if(IsHighlighted()||IsPressed()){
            this.animator.speed = 1f;
        }
     
        else
        {
            this.animator.speed = 0f;
        }
    }

}
