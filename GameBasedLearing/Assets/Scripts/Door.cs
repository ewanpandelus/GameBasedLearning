using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private string soundName;
    private LevelLoader levelLoader;
    AudioManager AudioManagement;


    // Start is called before the first frame update
    void Start()
    {
        levelLoader = this.GetComponent<LevelLoader>();

        this.animator.speed = 0f;
        if (!AudioManagement)
        {
            AudioManagement = AudioManager.instance;
        }
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            this.animator.speed = 1f;
           
        }
      
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (Input.GetKeyDown("e"))
            {
                levelLoader.LoadLevel();
            }
           

        }

    }
    void OnTriggerExit2D(Collider2D col)
    {

        if (col.CompareTag("Player"))
        {
            this.animator.speed = 0f;
            AudioManagement.Play(soundName);
        }

    }
}
