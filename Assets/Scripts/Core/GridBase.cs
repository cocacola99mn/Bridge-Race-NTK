using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBase : BrickAbstract
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
        
        if(RandomizeBrick == 1)
        {
            if (objPool.poolDictionary[BLUE_TAG].Count == 0)
                RandomizeBrick++;
            else
                objPool.Spawn(BLUE_TAG, spawnPostion, Quaternion.identity);
        }

        if (RandomizeBrick == 2)
        {
            if (objPool.poolDictionary[GREEN_TAG].Count == 0)
                RandomizeBrick++;
            else
                objPool.Spawn(GREEN_TAG, spawnPostion, Quaternion.identity);
        }

        if (RandomizeBrick == 3)
        {
            if (objPool.poolDictionary[RED_TAG].Count == 0)
                RandomizeBrick++;
            else
                objPool.Spawn(RED_TAG, spawnPostion, Quaternion.identity);
        }

        if (RandomizeBrick == 4)
        {
            if (objPool.poolDictionary[YELLOW_TAG].Count == 0)
            {
                RandomizeBrick = 1;
                if (RandomizeBrick == 1)
                {
                    if (objPool.poolDictionary[BLUE_TAG].Count == 0)
                        RandomizeBrick++;
                    else
                        objPool.Spawn(BLUE_TAG, spawnPostion, Quaternion.identity);
                }

                if (RandomizeBrick == 2)
                {
                    if (objPool.poolDictionary[GREEN_TAG].Count == 0)
                        RandomizeBrick++;
                    else
                        objPool.Spawn(GREEN_TAG, spawnPostion, Quaternion.identity);
                }

                if (RandomizeBrick == 3)
                {
                    if (objPool.poolDictionary[RED_TAG].Count == 0)
                        RandomizeBrick++;
                    else
                        objPool.Spawn(RED_TAG, spawnPostion, Quaternion.identity);
                }
            }
            else
                objPool.Spawn(YELLOW_TAG, spawnPostion, Quaternion.identity);
        }
    }
}
