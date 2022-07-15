using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBase : Singleton<GridBase>
{
    public int GridX;
    public int GridZ;

    GameObject[,] Grid;
    
    public float GridSpacingOffset;
    
    public Vector3 GridOrigin = Vector3.zero;

    void Start()
    {
        Grid = new GameObject[GridX, GridZ];
        
        SpawnGrid(GameConstant.BLUE_TAG);
    }

    private void SpawnGrid(string tag)
    {
        for(int x = 0; x < GridX; x++)
        {
            for(int z = 0; z < GridZ; z++)
            {
                Vector3 spawnPostion = new Vector3(x * GridSpacingOffset, 0, z * GridSpacingOffset) + GridOrigin;
                if (ObjectPooling.Ins.poolDictionary[tag].Count > 0)
                {
                    Grid[x, z] = ObjectPooling.Ins.Spawn(tag, spawnPostion, Quaternion.identity);                  
                }
            }
        }
    }

    /*private void PickandSpawn(Vector3 spawnPostion, Quaternion spawnRotation,ObjectPooling objPool)
    {
        int RandomizeBrick = UnityEngine.Random.Range(1, 5);
        
        if(RandomizeBrick == 1)
        {
            if (objPool.poolDictionary[GameConstant.BLUE_TAG].Count == 0)
                RandomizeBrick++;
            else
                objPool.Spawn(GameConstant.BLUE_TAG, spawnPostion, Quaternion.identity);
        }

        if (RandomizeBrick == 2)
        {
            if (objPool.poolDictionary[GameConstant.GREEN_TAG].Count == 0)
                RandomizeBrick++;
            else
                objPool.Spawn(GameConstant.GREEN_TAG, spawnPostion, Quaternion.identity);
        }

        if (RandomizeBrick == 3)
        {
            if (objPool.poolDictionary[GameConstant.RED_TAG].Count == 0)
                RandomizeBrick++;
            else
                objPool.Spawn(GameConstant.RED_TAG, spawnPostion, Quaternion.identity);
        }

        if (RandomizeBrick == 4)
        {
            if (objPool.poolDictionary[GameConstant.YELLOW_TAG].Count == 0)
            {
                RandomizeBrick = 1;
                if (RandomizeBrick == 1)
                {
                    if (objPool.poolDictionary[GameConstant.BLUE_TAG].Count == 0)
                        RandomizeBrick++;
                    else
                        objPool.Spawn(GameConstant.BLUE_TAG, spawnPostion, Quaternion.identity);
                }

                if (RandomizeBrick == 2)
                {
                    if (objPool.poolDictionary[GameConstant.GREEN_TAG].Count == 0)
                        RandomizeBrick++;
                    else
                        objPool.Spawn(GameConstant.GREEN_TAG, spawnPostion, Quaternion.identity);
                }

                if (RandomizeBrick == 3)
                {
                    if (objPool.poolDictionary[GameConstant.RED_TAG].Count == 0)
                        RandomizeBrick++;
                    else
                        objPool.Spawn(GameConstant.RED_TAG, spawnPostion, Quaternion.identity);
                }
            }
            else
                objPool.Spawn(GameConstant.YELLOW_TAG, spawnPostion, Quaternion.identity);
        }
    }*/
}
