﻿///BSD 3 - Clause License

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

public class LadderBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerMovement playerMovement;
    private HandleLadder ladder;
    private void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        ladder = new HandleLadder();

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

public class HandleLadder
{
    /// <summary>
    /// Simulates ladder behaviour for testing purposes
    /// </summary>

    public void HandleCharacterEnteredNegativeVelocityAndJumping(PlayerMovement playerMovement)
    {
        playerMovement.SetIsClimbing(true);
        playerMovement.SetIsJumping(false);

    }
    public void HandleCharacterEnteredPositiveVelocityAndJumping(PlayerMovement playerMovement)
    {
        playerMovement.SetIsClimbing(false);
    }
    public void HandleCharacterEnteredNotJumping(PlayerMovement playerMovement)
    {
        playerMovement.SetIsClimbing(true);
    }
}
