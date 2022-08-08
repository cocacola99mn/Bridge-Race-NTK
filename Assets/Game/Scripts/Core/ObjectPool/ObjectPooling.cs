using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;

        public Pool(string tag, GameObject prefab, int size)
        {
            this.tag = tag;
            this.prefab = prefab;
            this.size = size;
        }
    }

    private int Counter;

    public Dictionary<string, int> SpawnedCounter;
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0;i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            
            poolDictionary.Add(pool.tag, objectPool);
        }
        
        SpawnedCounter = new Dictionary<string, int>();
        SpawnedCounter.Add(GameConstant.BLUE_TAG, 0);
        SpawnedCounter.Add(GameConstant.RED_TAG, 0);
        SpawnedCounter.Add(GameConstant.GREEN_TAG, 0);
        SpawnedCounter.Add(GameConstant.YELLOW_TAG, 0);
    }

    public GameObject Spawn(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("Error");
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        SpawnedCounterControl(tag, 1);
        
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        return objectToSpawn;
    }

    public void Despawn(string tag, GameObject prefab)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("None");
        }
        
        prefab.SetActive(false);
        
        poolDictionary[tag].Enqueue(prefab);

        SpawnedCounterControl(tag, -1);
    }

    public void DespawnAll()
    {
        poolDictionary.Clear();
    }

    public void SpawnedCounterControl(string tag, int num)
    {
        Counter = SpawnedCounter[tag];
        Counter += num;
        SpawnedCounter[tag] = Counter;
    }
}
