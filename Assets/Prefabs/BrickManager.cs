using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    [SerializeField] internal bool buildMode = false;
    [SerializeField] private Brick brickScript;
    [SerializeField] private ObjectMov objMove;

    Rigidbody2D rb;
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        objMove.enabled = false;
        brickScript.enabled = false;
        if (buildMode == true)
        {
            rb.simulated = false;
            brickScript.enabled = false;
            objMove.enabled = true;
        }
    }

    public void SwitchMode()
    {
        if (buildMode)
        {
            rb.simulated = false;
            brickScript.enabled = false;
            objMove.enabled = true;
        }
        else if (!buildMode)
        {
            rb.simulated = true;
            brickScript.enabled = true;
            objMove.enabled = false;
        }
    }
}
