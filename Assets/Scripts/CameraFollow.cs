using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    public Vector3 offset = new Vector3(0.2f, 0.0f, -10f);

    [SerializeField]
    float dampingTime = 0.3f;

    [SerializeField]
    public Vector3 cameraSpeed = Vector3.zero;
    void Awake()
    {
        Application.targetFrameRate = 60;    
    }

    void Start()
    {
        
    }

    void Update()
    {
        MoveCamera(true);
    }

    public void ResetCameraPosition()
    {
        MoveCamera(false);
    }

    void MoveCamera(bool smooth)
    {
        Vector3 destination = new Vector3(target.position.x - offset.x, offset.y, offset.z);

        if (smooth)
        {
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref cameraSpeed, dampingTime);
        }
        else
        {
            transform.position = destination;
        }
    }
}
