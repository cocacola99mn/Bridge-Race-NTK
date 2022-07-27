using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    float deltaZ;

    void Start()
    {
        deltaZ = transform.position.z - target.position.z;
    }

    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y + 2.5f, target.position.z + deltaZ);
    }
}
