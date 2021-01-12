using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelChooseAnimation : Selectable
{

    [SerializeField] private new Animator animator;
    [SerializeField] private string soundName;
    AudioManager AudioManagement;
  
   
  
    private void Update()
    {
        if (!AudioManagement)
        {
            AudioManagement = AudioManager.instance;
        }
       if(IsHighlighted()){
            this.animator.speed = 1f;
            AudioManagement.Play(soundName);
        }
     
        else
        {
            this.animator.speed = 0f;
        }
    }

}
