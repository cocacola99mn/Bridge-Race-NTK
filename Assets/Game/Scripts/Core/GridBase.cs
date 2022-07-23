using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBase : MonoBehaviour
{
    public int GridX;
    public int GridZ;

    private int RandomizeBrick;

    bool isSpawnedState2, isSpawnedState3;
    
    public float GridSpacingOffset;
    
    public Vector3 GridOrigin;

    void Start()
    {
        isSpawnedState2 = isSpawnedState3 = false;

        SpawnGrid(30);
    }

    private void Update()
    {
        if (LevelManager.Ins.IsState(LevelState.SecondState) == true)
        {
            if(isSpawnedState2 == false)
            {
                SpawnNextStateGrid(4.9f, 15.2f, 60);
                isSpawnedState2 = true;
            }
        }

        if (LevelManager.Ins.IsState(LevelState.ThirdState) == true)
        {
            if (isSpawnedState3 == false)
            {
                SpawnNextStateGrid(4.95f, 15.5f, 90);
                isSpawnedState3 = true;
            }
        }                     
    }

    private void SpawnGrid(int limit)
    {
        for(int x = 0; x < GridX; x++)
        {
            for(int z = 0; z < GridZ; z++)
            {
                Vector3 spawnPosition = new Vector3(x * GridSpacingOffset, 0, z * GridSpacingOffset) + GridOrigin;
                PickandSpawn(spawnPosition, Quaternion.identity, ObjectPooling.Ins,limit);
            }
        }
    }

    private void SpawnNextStateGrid(float PosY, float PosZ, int limit)
    {
        GridOrigin += new Vector3(0, PosY, PosZ);

        for (int x = 0; x < GridX; x++)
        {
            for (int z = 0; z < GridZ; z++)
            {
                    Vector3 spawnPosition = new Vector3(x * GridSpacingOffset, 0, z * GridSpacingOffset) + GridOrigin;
                    PickandSpawn(spawnPosition, Quaternion.identity, ObjectPooling.Ins, limit);                
            }
        }
    }

    private void PickandSpawn(Vector3 spawnPosition, Quaternion spawnRotation,ObjectPooling objPool, int limit)
    {
        RandomizeBrick = UnityEngine.Random.Range(1, 5);

        randomizeBrick (objPool, spawnPosition, limit);
    }

    private void randomizeBrick(ObjectPooling objPool, Vector3 spawnPosition, int limit)
    {
        if (RandomizeBrick == 1)
            randomSpawnCondition(GameConstant.BLUE_TAG, spawnPosition, limit);

        if (RandomizeBrick == 2)
            randomSpawnCondition(GameConstant.GREEN_TAG, spawnPosition, limit);

        if (RandomizeBrick == 3)
            randomSpawnCondition(GameConstant.RED_TAG, spawnPosition, limit);

        if (RandomizeBrick == 4)
        {
            if (objPool.SpawnedCounter[GameConstant.YELLOW_TAG] == limit)
            {
                RandomizeBrick =1;
                if (RandomizeBrick == 1)
                    randomSpawnCondition(GameConstant.BLUE_TAG, spawnPosition, limit);

                if (RandomizeBrick == 2)
                    randomSpawnCondition(GameConstant.GREEN_TAG, spawnPosition, limit);

                if (RandomizeBrick == 3)
                    randomSpawnCondition(GameConstant.RED_TAG, spawnPosition, limit);
            }
            else
                objPool.Spawn(GameConstant.YELLOW_TAG, spawnPosition, Quaternion.identity);
                AITargetPoint.Ins.getTargetPointByColor(GameConstant.YELLOW_TAG, spawnPosition);
        }
    }

    private void randomSpawnCondition(string tag,Vector3 spawnPosition,int limit)
    {
        if (ObjectPooling.Ins.SpawnedCounter[tag] == limit)
            RandomizeBrick++;
        else
        {
            ObjectPooling.Ins.Spawn(tag, spawnPosition, Quaternion.identity);
            AITargetPoint.Ins.getTargetPointByColor(tag, spawnPosition);
        }
            
    }
}
