using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBase : MonoBehaviour
{
    public static string BLUE_TAG = "Blue";
    public static string RED_TAG = "Red";
    public static string GREEN_TAG = "Green";
    public static string YELLOW_TAG = "Yellow";

    public GameObject item;
    public int GridX;
    public int GridZ;
    public float GridSpacingOffset;
    public Vector3 GridOrigin = Vector3.zero;

    void Start()
    {
        SpawnGrid();
    }

    private void SpawnGrid()
    {
        for(int x = 0; x < GridX; x++)
        {
            for(int z = 0; z < GridZ; z++)
            {
                Vector3 spawnPostion = new Vector3(x * GridSpacingOffset, 0, z * GridSpacingOffset) + GridOrigin;
                PickandSpawn(spawnPostion,Quaternion.identity, ObjectPooling.Ins);
            }
        }
    }

    private void PickandSpawn(Vector3 spawnPostion, Quaternion spawnRotation,ObjectPooling objPool)
    {
        int RandomizeBrick = UnityEngine.Random.Range(1, 5);
        switch (RandomizeBrick)
        {
            case 1:
                objPool.SpawnFromPool(BLUE_TAG, spawnPostion, Quaternion.identity);
                break;
            case 2:
                objPool.SpawnFromPool(GREEN_TAG, spawnPostion, Quaternion.identity);
                break;
            case 3:
                objPool.SpawnFromPool(RED_TAG, spawnPostion, Quaternion.identity);
                break;
            case 4:
                objPool.SpawnFromPool(YELLOW_TAG, spawnPostion, Quaternion.identity);
                break;
            default:
                Debug.Log("None");
                break;
        }       
    }
}
