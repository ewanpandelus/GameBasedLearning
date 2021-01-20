using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private GameObject player;
    PlayerMovement playerMovement;
    private void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
       
        if (col.CompareTag("Player"))
        {
            playerMovement.SetIsClimbing(true);
      
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (playerMovement.GetVerticalVelocity() < 0.01 && playerMovement.GetIsJumping() == true)
        {
            playerMovement.SetIsClimbing(true);
            playerMovement.SetIsJumping(false);

        }
        if (playerMovement.GetVerticalVelocity() > 0.01 && playerMovement.GetIsJumping() == true)
        {
            playerMovement.SetIsClimbing(false);
        }
        else
        {
            playerMovement.SetIsClimbing(true);
        }

    }
    void OnTriggerExit2D(Collider2D col)
    {

        if (col.CompareTag("Player"))
        {
            playerMovement.SetIsClimbing(false);
        }

    }
}
