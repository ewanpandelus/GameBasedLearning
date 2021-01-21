using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayerMovementTests
    {

        [UnityTest]
        public IEnumerator PlayerJumpMovesPlayerVertically()
        {
            IPlayerMovement playerMovement = Substitute.For<IPlayerMovement>();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
