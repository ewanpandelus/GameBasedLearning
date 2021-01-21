using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private IPlayerMovement playerMovement;
    private Ladder ladder;
    private void Start()
    {
        playerMovement = player.GetComponent<IPlayerMovement>();
        ladder = new Ladder();

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

        if (col.CompareTag("Player"))
        {
            if (playerMovement.GetVerticalVelocity() < 0.01 && playerMovement.GetIsJumping() == true)
            {
                ladder.HandleCharacterEnteredNegativeVelocityAndJumping(playerMovement);
            }
            if (playerMovement.GetVerticalVelocity() > 0.01 && playerMovement.GetIsJumping() == true)
            {
                ladder.HandleCharacterEnteredPositiveVelocityAndJumping(playerMovement);
            }
            else
            {
                ladder.HandleCharacterEnteredNotJumping(playerMovement);
            }
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
public class Ladder{
    public void HandleCharacterEnteredNegativeVelocityAndJumping(IPlayerMovement playerMovement)
    {
        playerMovement.SetIsClimbing(true);
        playerMovement.SetIsJumping(false);

    }
    public void HandleCharacterEnteredPositiveVelocityAndJumping(IPlayerMovement playerMovement)
    {
        playerMovement.SetIsClimbing(false);
    }
    public void HandleCharacterEnteredNotJumping(IPlayerMovement playerMovement)
    {
        playerMovement.SetIsClimbing(true);
    }
}
