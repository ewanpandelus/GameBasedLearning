using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private DynamicUI dynamicUI;
    public string levelName;
    public string destinationLevel;
    
    AudioManager AudioManagement;
    GlobalDataHolder globalDataHolder;
    Scene scene;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        AudioManagement = AudioManager.instance;
        //globalDataHolder = GameObject.Find("GlobalDataHolder").GetComponent<GlobalDataHolder>();

    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevel(levelName));
    }
    private IEnumerator LoadLevel(string levelName)
    {
        
        transition.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(1f);
        if (scene.name.Contains("Overview")&&!this.name.Contains("Back"))
        {
            SceneManager.LoadScene(globalDataHolder.GetDestinationLevel());
        }
        else
        {
            SceneManager.LoadScene(levelName);
        }
        globalDataHolder.SetDestinationLevel(destinationLevel);

    }



    public void PlaySceneChangeSound()
    {
        AudioManagement.Play("ButtonPress");
    }

}


