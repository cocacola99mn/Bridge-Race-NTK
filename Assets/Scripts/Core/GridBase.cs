using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBase : Singleton<GridBase>
{
    public int GridX;
    public int GridZ;

    private int RandomizeBrick;

    Vector3[,] Grid;
    
    public float GridSpacingOffset;
    
    public Vector3 GridOrigin;

    void Start()
    {
        Grid = new Vector3 [GridX, GridZ];

        SpawnGrid();
        
    }

    private void Update()
    {
        if (LevelManager.Ins.IsState(LevelState.SecondState) == true)
            SpawnNextStateGrid(5, 15.5f);
        if(LevelManager.Ins.IsState(LevelState.ThirdState) == true)
            SpawnNextStateGrid(10, 31f);
            
    }

    private void SpawnGrid()
    {
        for(int x = 0; x < GridX; x++)
        {
            for(int z = 0; z < GridZ; z++)
            {
                Vector3 spawnPosition = new Vector3(x * GridSpacingOffset, 0, z * GridSpacingOffset) + GridOrigin;
                PickandSpawn(spawnPosition, Quaternion.identity, ObjectPooling.Ins);
                Grid[x, z] = spawnPosition;
                Debug.Log(Grid[x, z]);
            }
        }
    }

    private void SpawnNextStateGrid(float PosY, float PosZ)
    {
        GridOrigin += new Vector3(0, PosY, PosZ);

        for (int x = 0; x < GridX; x++)
        {
            for (int z = 0; z < GridZ; z++)
            {
                    Vector3 spawnPosition = new Vector3(x * GridSpacingOffset, 0, z * GridSpacingOffset) + GridOrigin;
                    PickandSpawn(spawnPosition, Quaternion.identity, ObjectPooling.Ins);                
            }
        }
    }

    private void PickandSpawn(Vector3 spawnPosition, Quaternion spawnRotation,ObjectPooling objPool)
    {
        RandomizeBrick = UnityEngine.Random.Range(1, 5);

        randomizeBrick (objPool, spawnPosition);
    }

    private void randomizeBrick(ObjectPooling objPool, Vector3 spawnPosition)
    {
        if (RandomizeBrick == 1)
            randomSpawnCondition(GameConstant.BLUE_TAG, spawnPosition);

        if (RandomizeBrick == 2)
            randomSpawnCondition(GameConstant.GREEN_TAG, spawnPosition);

        if (RandomizeBrick == 3)
            randomSpawnCondition(GameConstant.RED_TAG, spawnPosition);

        if (RandomizeBrick == 4)
        {
            if (objPool.poolDictionary[GameConstant.YELLOW_TAG].Count == 0)
            {
                RandomizeBrick =1;
                if (RandomizeBrick == 1)
                    randomSpawnCondition(GameConstant.BLUE_TAG, spawnPosition);

                if (RandomizeBrick == 2)
                    randomSpawnCondition(GameConstant.GREEN_TAG, spawnPosition);

                if (RandomizeBrick == 3)
                    randomSpawnCondition(GameConstant.RED_TAG, spawnPosition);
            }
            else
                objPool.Spawn(GameConstant.YELLOW_TAG, spawnPosition, Quaternion.identity);
        }
    }

    private void randomSpawnCondition(string tag,Vector3 spawnPosition)
    {
        if (ObjectPooling.Ins.poolDictionary[tag].Count == 0)
            RandomizeBrick++;
        else
            ObjectPooling.Ins.Spawn(tag, spawnPosition, Quaternion.identity);
    }
}
