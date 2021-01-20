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

    void OnTriggerExit2D(Collider2D col)
    {

        if (col.CompareTag("Player"))
        {
            playerMovement.SetIsClimbing(false);
        }

    }
}
