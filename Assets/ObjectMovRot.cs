using Unity.VisualScripting;
using UnityEngine;

public class ObjectMovRot : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;
    
    [Header("Rotation Settings")]
    [SerializeField] private bool snapRotation = true;
    [SerializeField] private float rotationStep = 45f;
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private float snapCooldown = 0.2f;

    private float snapTimer = 0f;

    void Update()
    {
        if (dragging)
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = transform.position.z;
            transform.position = mouseWorld + offset;
        }
        if (snapTimer > 0f)
            snapTimer -= Time.deltaTime;
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButton(1))
        {
            RotateObject();
            return;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = transform.position.z;
            offset = transform.position - mouseWorld;
            dragging = true;
        }
    }

    private void OnMouseUp()
    { dragging = false; }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(1) && !dragging)
        {
            RotateObject();
        }
    }

    void RotateObject()
    {
        if (snapRotation)
        {
            if (snapTimer <= 0f)
            {
                transform.rotation *= Quaternion.Euler(0, 0, rotationStep);
                snapTimer = snapCooldown;
            }
        } 
        else { transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime, Space.World); }
    }
}