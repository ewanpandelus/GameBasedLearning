using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder2 : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerMovement playerMovement;
    private void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
       
        if (col.CompareTag("Player"))
        {
            playerMovement.SetIsClimbing(true);
      
        }
    }
   private void OnTriggerStay2D(Collider2D col)
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
    private void OnTriggerExit2D(Collider2D col)
    {

        if (col.CompareTag("Player"))
        {
            playerMovement.SetIsClimbing(false);
        }

    }
}
