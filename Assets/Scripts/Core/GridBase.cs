using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBase : Singleton<GridBase>
{
    public int GridX;
    public int GridZ;

    private int RandomizeBrick;

    GameObject[,] Grid;
    GameObject brick;
    
    public float GridSpacingOffset;
    
    public Vector3 GridOrigin;

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
                Grid[x,z] = PickandSpawn(spawnPostion, Quaternion.identity, ObjectPooling.Ins);
            }
        }
    }

    private GameObject PickandSpawn(Vector3 spawnPostion, Quaternion spawnRotation,ObjectPooling objPool)
    {
        RandomizeBrick = UnityEngine.Random.Range(1, 5);

        brick = randomizeBrick (objPool, spawnPostion);
        return brick;
    }

    private GameObject randomizeBrick(ObjectPooling objPool, Vector3 spawnPostion)
    {
        if (RandomizeBrick == 1)
            brick = randomSpawnCondition(GameConstant.BLUE_TAG, spawnPostion);

        if (RandomizeBrick == 2)
            brick = randomSpawnCondition(GameConstant.GREEN_TAG, spawnPostion);

        if (RandomizeBrick == 3)
            brick = randomSpawnCondition(GameConstant.RED_TAG, spawnPostion);

        if (RandomizeBrick == 4)
        {
            if (objPool.poolDictionary[GameConstant.YELLOW_TAG].Count == 0)
            {
                RandomizeBrick = 1;
                if (RandomizeBrick == 1)
                    brick = randomSpawnCondition(GameConstant.BLUE_TAG, spawnPostion);

                if (RandomizeBrick == 2)
                    brick = randomSpawnCondition(GameConstant.GREEN_TAG, spawnPostion);

                if (RandomizeBrick == 3)
                    brick = randomSpawnCondition(GameConstant.RED_TAG, spawnPostion);
            }
            else
                brick = objPool.Spawn(GameConstant.YELLOW_TAG, spawnPostion, Quaternion.identity);
        }
        return brick;
    }

    private GameObject randomSpawnCondition(string tag,Vector3 spawnPostion)
    {
        if (ObjectPooling.Ins.poolDictionary[tag].Count == 0)
            RandomizeBrick++;
        else
            brick = ObjectPooling.Ins.Spawn(tag, spawnPostion, Quaternion.identity);
        return brick;
    }
}
