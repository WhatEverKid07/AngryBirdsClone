using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    [SerializeField] internal bool buildMode = false;
    [SerializeField] private Brick brickScript;
    [SerializeField] private ObjectMovRot objMove;

    private Vector3 cachPosition;
    private Vector3 cachRotation;

    Rigidbody2D rb;
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (buildMode == true)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            brickScript.enabled = false;
            objMove.enabled = true;
        }
    }
    private void Update()
    {
        if (buildMode)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.bodyType = RigidbodyType2D.Kinematic;
            brickScript.enabled = false;
            objMove.enabled = true;
        }
        else if (!buildMode)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.bodyType = RigidbodyType2D.Dynamic;
            brickScript.enabled = true;
            objMove.enabled = false;
        }
    }
    public void SetLocation()
    {
        cachPosition = transform.position;
        cachRotation = transform.rotation.eulerAngles;
    }
    public void ResetLocation()
    {
        transform.position = cachPosition;
        transform.rotation = Quaternion.Euler(cachRotation);
    }
}