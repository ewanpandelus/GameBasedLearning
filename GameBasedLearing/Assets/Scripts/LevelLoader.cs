using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    [SerializeField]private Animator transition;
    [SerializeField] private DynamicUI dynamicUI;

    private void Start()
    {
       
    }
    public void LoadGraph()
    {
        dynamicUI.FadeOutWinningPathtext();
        StartCoroutine(LoadGraphScene());
    }
    private IEnumerator LoadGraphScene()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene("ComplexityGraph");
    }

 

}
