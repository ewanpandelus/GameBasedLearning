using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private string soundName;
    private LevelLoader levelLoader;
    AudioManager AudioManagement;
    private bool playerAtDoor = false;
    private TextMeshProUGUI enterText;

    // Start is called before the first frame update
    void Start()
    {
        enterText = GameObject.Find("EnterText").GetComponent<TextMeshProUGUI>();
        enterText.text = "";
        levelLoader = this.GetComponent<LevelLoader>();

        this.animator.speed = 0f;
        if (!AudioManagement)
        {
            AudioManagement = AudioManager.instance;
        }
    }
    private void Update()
    {

        if (Input.GetKeyDown("e") && playerAtDoor)
        {
            AudioManagement.Play("ButtonPress");
            levelLoader.LoadLevel();
        }
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        enterText.text = "Press E to enter";
        if (col.CompareTag("Player"))
        {
            this.animator.speed = 1f;
            playerAtDoor = true;


        }
    }
        void OnTriggerStay2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {

                playerAtDoor = true;

            }

        }
        void OnTriggerExit2D(Collider2D col)
        {

            if (col.CompareTag("Player"))
            {
                enterText.text = "";
                playerAtDoor = false;
                this.animator.speed = 0f;
                AudioManagement.Play(soundName);
            }

        }
    
}

