using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataHolder:MonoBehaviour

{
    private string destinationLevel = "";
    private void Awake() 
    {
    DontDestroyOnLoad(transform.gameObject);
    }
    public string GetDestinationLevel()
    {
        return this.destinationLevel;
    }
    public void SetDestinationLevel(string destinationLevelName)
    {
        destinationLevel = destinationLevelName;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
