using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Panning Settings")]
    [SerializeField] private float panSpeed = 5f;

    [Header("Bounds")]
    [SerializeField] private float minX = -10f;
    [SerializeField] private float maxX = 10f;
    [SerializeField] private float minY = -5f;
    [SerializeField] private float maxY = 5f;

    
    [Header("Zoom Settings")]
    private Camera Cam;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;
    

    private Vector3 dragOrigin;

    private void Start()
    {
        //Cam = Camera.main;
        //Zoom = Cam.orthographicSize;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(2))
        {
            Vector3 difference = dragOrigin - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += difference;
            transform.position = new Vector3
            (
                Mathf.Clamp(transform.position.x, minX, maxX),
                Mathf.Clamp(transform.position.y, minY, maxY),
                transform.position.z
            );
        }
        /*
        float scroll = Input.GetAxis("Mouse ScrollWheel"); // scroll input
        if (scroll != 0.0f)
        {
            float newSize = Cam.orthographicSize - scroll * zoomSpeed;
            Cam.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
        }
        */
    }
}