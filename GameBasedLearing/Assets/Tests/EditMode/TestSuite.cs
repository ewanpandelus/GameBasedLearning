using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestSuite
    {
        // A Test behaves as an ordinary method
        [Test]
        public void PlayerEnteringLadderAndJumpingWithNegativeVelocityStartsClimbState()
        {
            Ladder ladder = new Ladder();
            IPlayerMovement playerMovement = Substitute.For<IPlayerMovement>();
            ladder.HandleCharacterEnteredNegativeVelocityAndJumping(playerMovement);
            Assert.AreEqual(playerMovement.GetIsClimbing(), false);
          
        }
    }
}
