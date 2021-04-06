///BSD 3 - Clause License

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
///OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QueenSpawner : MonoBehaviour
{
    [SerializeField] private GameObject queenPiecePrefab;
    public int cellSize;
    [SerializeField] GameObject spawnPoint;
    private CanvasScaler canvasScaler;
    private Vector2 ScreenScale;
    private GameObject latestQueen;

    public void SpawnQueen() 
    {
       GameObject queen = (GameObject)Instantiate(queenPiecePrefab, spawnPoint.transform.position, transform.rotation);
       GameObject crossFade = GameObject.Find("CrossFade");
       queen.transform.SetParent(crossFade.transform);
       queen.transform.SetSiblingIndex(3);
       QueenScaleAdjustments(queen);
       latestQueen = queen;
    }

    /// <summary>
    /// Scales queens relative to the screen size
    /// </summary>
    /// <param name="queen">Queen object to scale</param>
    private void QueenScaleAdjustments(GameObject queen)
    {
        RectTransform rectTransform = queen.GetComponent<RectTransform>();
        Vector2 scaleFactor = CalculateScreenScale(queen) / 2f;
        float width = Screen.width;
        float height = Screen.height;
        if (width / height > 1.65f)
        {
            scaleFactor += new Vector2(0f, 0.16f);
        }
        if (width / height > 1.99f)
        {
            scaleFactor += new Vector2(0f, 0.16f);
        }
        rectTransform.sizeDelta *= scaleFactor;
    }

    public void SpawnQueenOnBoard(Vector3 boardPosition)
    {
        GameObject queen = (GameObject)Instantiate(queenPiecePrefab, boardPosition, transform.rotation);
        GameObject chessBoard = GameObject.Find("ChessBoard");
        queen.transform.SetParent(chessBoard.transform);
        QueenScaleAdjustments(queen);
        latestQueen = queen;
    }

    public GameObject GetLatestQueen()
    {
        return this.latestQueen;
    }

    /// <summary>
    /// Calculates the aspect ratio of the screen, and the scale
    /// </summary>
    /// <param name="queen">Queen object to find the canvas it is placed on</param>
    /// <returns>A vector with the ratio between x and y length of canvas</returns>
    private Vector2 CalculateScreenScale(GameObject queen)
    {
        if(canvasScaler == null)
             {
            canvasScaler = queen.transform.parent.gameObject.GetComponentInParent<CanvasScaler>();
        }

        if (canvasScaler)
        {
            return new Vector2( Screen.width/canvasScaler.referenceResolution.x ,  Screen.height/canvasScaler.referenceResolution.y );

        }
        else
        {
            return Vector2.one;
        }
    }
}
