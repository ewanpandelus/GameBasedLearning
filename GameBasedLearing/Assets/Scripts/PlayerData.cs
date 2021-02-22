using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public float[] position;
    public int totalCherries;

    public PlayerData(PlayerMovement playerMovement)
    {
        this.position = new float[3];
        this.position[0] = playerMovement.GetPosition().x;
        this.position[1] = playerMovement.GetPosition().y;
        this.position[2] = playerMovement.GetPosition().z;
        
    }
}
