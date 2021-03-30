using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestLadderBehaviour
    {
        // A Test behaves as an ordinary method
        [Test]
        public void PlayerEnteringLadderAndJumpingWithNegativeVelocityStartsClimbState()
        {
            HandleLadder ladder = new HandleLadder();
            var gameObject = new GameObject();
            PlayerMovement playerMovement = gameObject.AddComponent<PlayerMovement>();
            ladder.HandleCharacterEnteredNegativeVelocityAndJumping(playerMovement);
            Assert.IsTrue(playerMovement.GetIsClimbing());
        }
        [Test]
        public void PlayerEnteringLadderAndJumpingWithPositiveVelocityDoesntClimb()
        {
            HandleLadder ladder = new HandleLadder();
            var gameObject = new GameObject();
            PlayerMovement playerMovement = gameObject.AddComponent<PlayerMovement>();
            ladder.HandleCharacterEnteredPositiveVelocityAndJumping(playerMovement);
            Assert.IsFalse(playerMovement.GetIsClimbing());
        }
        [Test]
        public void PlayerEnteringLadderNotJumpingStartsClimbing()
        {
            HandleLadder ladder = new HandleLadder();
            var gameObject = new GameObject();
            PlayerMovement playerMovement = gameObject.AddComponent<PlayerMovement>();
            ladder.HandleCharacterEnteredNotJumping(playerMovement);
            Assert.IsTrue(playerMovement.GetIsClimbing());
        }
    }
}

