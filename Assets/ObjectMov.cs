using Unity.VisualScripting;
using UnityEngine;

public class ObjectMov : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;

    [Header("Rotation Settings")]
    public bool snapRotation = true;
    public float rotationStep = 45f;
    public float rotationSpeed = 90f;
    public float snapCooldown = 0.2f;

    private float snapTimer = 0f;

    void Update()
    {
        // Drag movement
        if (dragging)
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = transform.position.z;
            transform.position = mouseWorld + offset;
        }

        // Snap cooldown countdown
        if (snapTimer > 0f)
            snapTimer -= Time.deltaTime;
    }

    private void OnMouseDown()
    {
        // If right-click over object → rotate
        if (Input.GetMouseButton(1))
        {
            RotateObject();
            return; // Don't start dragging
        }

        // If left-click over object → start drag
        if (Input.GetMouseButton(0))
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = transform.position.z;
            offset = transform.position - mouseWorld;
            dragging = true;
        }
    }

    private void OnMouseUp()
    {
        dragging = false;
    }

    private void OnMouseOver()
    {
        // Optional: hold right-click while hovering to rotate continuously
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
        else
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime, Space.World);
        }
    }
}