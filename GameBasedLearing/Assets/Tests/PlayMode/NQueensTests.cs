using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class NQueensTest
    {

        [UnityTest]
        public IEnumerator UsingSolveFunctionSolvesPuzzle()
        { 
            var gameObject = new GameObject();
            ChessBoard board = gameObject.AddComponent<ChessBoard>();
            board.SetProblemSize(4);
            board.Create();
            NQueens nQueens = gameObject.AddComponent<NQueens>();
            
            nQueens.Solve();
            yield return new WaitForSeconds(10f);
            Assert.IsTrue(nQueens.GetSafe());
        }
    }
}
