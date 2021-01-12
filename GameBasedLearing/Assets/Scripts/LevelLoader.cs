using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private DynamicUI dynamicUI;
    public string levelName;
    AudioManager AudioManagement;

    private void Start()
    {
        AudioManagement = AudioManager.instance;
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



    public void PlaySceneChangeSound()
    {
        AudioManagement.Play("ButtonPress");
    }

}


