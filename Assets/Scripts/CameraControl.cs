using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 initialOffset;

    void Start()
    {
        initialOffset = transform.position - playerTransform.position;
    }

    void Update()
    {
        transform.position = playerTransform.position + initialOffset;
    }
}
