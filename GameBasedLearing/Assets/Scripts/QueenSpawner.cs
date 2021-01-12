using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QueenSpawner : MonoBehaviour
{
    [SerializeField] private GameObject queenPiecePrefab;
    public void SpawnQueen()
    {
        GameObject gameObject = (GameObject)Instantiate(queenPiecePrefab, transform.position,transform.rotation);
        GameObject parentCanvas = GameObject.Find("ParentCanvas");
        gameObject.transform.SetParent(parentCanvas.transform);
        GameObject queenSpawnButton = GameObject.Find("SpawnQueenButton");
        gameObject.transform.position = queenSpawnButton.transform.position- new Vector3(0,100);
    }
}
