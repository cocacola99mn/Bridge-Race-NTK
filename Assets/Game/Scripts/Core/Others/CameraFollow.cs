using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target, Camera;
    float deltaZ;

    void Start()
    {
        deltaZ = transform.position.z - target.position.z;
    }

    void LateUpdate()
    {
        Camera.position = new Vector3(target.position.x, target.position.y + 2.5f, target.position.z + deltaZ);
    }
}
