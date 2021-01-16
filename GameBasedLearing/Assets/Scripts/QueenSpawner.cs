using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QueenSpawner : MonoBehaviour
{
    [SerializeField] private GameObject queenPiecePrefab;
    public int cellSize;
    public void SpawnQueen()
    {
        GameObject gameObject = (GameObject)Instantiate(queenPiecePrefab, transform.position,transform.rotation);
        GameObject chessBoard = GameObject.Find("CrossFade");
        gameObject.transform.SetParent(chessBoard.transform);
        GameObject queenSpawnButton = GameObject.Find("SpawnQueenButton");
        gameObject.transform.position = queenSpawnButton.transform.position- new Vector3(0,100);
    }

}
