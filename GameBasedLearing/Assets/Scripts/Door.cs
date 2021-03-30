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
    private GlobalDataHolder globalDataHolder;

    void Start()
    {
        enterText = GameObject.Find("EnterText").GetComponent<TextMeshProUGUI>();
        enterText.text = "";
        levelLoader = this.GetComponent<LevelLoader>();
        globalDataHolder = GameObject.Find("GlobalDataHolder").GetComponent<GlobalDataHolder>();
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

    void OnTriggerEnter2D(Collider2D col)
    {
        enterText.text = "Press E to Enter";
        if (col.CompareTag("Player"))
        {
            this.animator.speed = 1f;
        }
    }
        void OnTriggerStay2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
            if (this.name == "NQueens2Door")
            {
                if (globalDataHolder.GetNQueensLevel1() == false)
                {
                    enterText.text = "You must visit NQueens level 1 first";
                    playerAtDoor = false;
                    return;
                }
            }
            if (this.name == "TB2Door")
            {
                if (globalDataHolder.GetEasyTSP() == false)
                {
                    enterText.text = "You must visit Travelling Bee level 1 first";
                    playerAtDoor = false;
                    return;
                }
            }
                playerAtDoor = true;
                enterText.text = "Press E to Enter";
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

