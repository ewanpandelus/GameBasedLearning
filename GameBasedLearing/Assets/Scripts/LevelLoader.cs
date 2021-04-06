///BSD 3 - Clause License

/// Copyright(c) 2021, ewanpandelus
///All rights reserved.

///Redistribution and use in source and binary forms, with or without
///modification, are permitted provided that the following conditions are met:

///1.Redistributions of source code must retain the above copyright notice, this
///list of conditions and the following disclaimer.

///2. Redistributions in binary form must reproduce the above copyright notice,
///this list of conditions and the following disclaimer in the documentation
///and/or other materials provided with the distribution.

///3. Neither the name of the copyright holder nor the names of its
///contributors may be used to endorse or promote products derived from
///this software without specific prior written permission.

///THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
///AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
///IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
///DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
///FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
///DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
///SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
///CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
///OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
///OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private DynamicUI dynamicUI;
    public string levelName;
    public string destinationLevel;
    private SceneTransition sceneTransition;
    [SerializeField] private AudioManager AudioManagement;
    private GlobalDataHolder globalDataHolder;
    private Scene scene;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        AudioManagement = AudioManager.instance;
        globalDataHolder = GameObject.Find("GlobalDataHolder").GetComponent<GlobalDataHolder>();
        sceneTransition = GameObject.Find("TransitionObject").GetComponent<SceneTransition>();

    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void LoadLevel()
    {
        StartCoroutine(LoadLevel(levelName));
        globalDataHolder.SetLevelToAssessComplexity(SceneManager.GetActiveScene().name);
    }
    private IEnumerator LoadLevel(string levelName)
    {
        if(scene.name == "Platformer")
        {
             SaveSystem.SavePlayer(playerMovement);
        }
        SaveSystem.SaveTotalCherries(globalDataHolder);
        SaveSystem.SaveLevelVisitData(globalDataHolder);
        sceneTransition.GetAnimator().SetTrigger("Start");
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


