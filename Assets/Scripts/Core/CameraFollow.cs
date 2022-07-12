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
        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z + deltaZ);
    }
}
