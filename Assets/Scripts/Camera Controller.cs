using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject targetObj;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - targetObj.transform.position;
    }

    void Update()
    {
        Vector3 pos = targetObj.transform.position + offset;
        transform.position = pos;
    }
}
