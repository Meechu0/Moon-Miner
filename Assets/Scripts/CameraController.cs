using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;  
    public Vector3 offset;

    [SerializeField]
    private float zoomSpeed;
    [SerializeField]
    private float minZoomDist;
    [SerializeField]
    private float maxZoomDist;

    private float currentZoomDistance = 10f;  

    void LateUpdate()
    {
        processCamera();
    }

    void processCamera()
    {
        Vector3 cameraTargetPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, cameraTargetPosition, Time.deltaTime); // move camera towards cameraTargetPosition

        // camera zoom - mouse scrollwheel
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        currentZoomDistance -= scrollWheel * zoomSpeed;
        currentZoomDistance = Mathf.Clamp(currentZoomDistance, minZoomDist, maxZoomDist);

        // camera position based on zoom distance
        transform.position = target.position - transform.forward * currentZoomDistance;

        //clamp camera's y position
        transform.position = new Vector3(transform.position.x, Mathf.Max(transform.position.y, 2.8f), transform.position.z);

        if (Input.GetMouseButton(1)) // rotate camera using right click
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            transform.RotateAround(target.position, Vector3.up, mouseX * 2f);
            transform.RotateAround(target.position, transform.right, -mouseY * 2f);
        }
        transform.LookAt(target);
    }
}