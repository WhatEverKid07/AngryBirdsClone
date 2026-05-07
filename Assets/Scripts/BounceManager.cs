using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceManager : MonoBehaviour
{
    [SerializeField] internal bool buildMode = false;
    [SerializeField] private ObjectMovRot objMove;

    private void Awake()
    {
        if (buildMode)
        {
            objMove.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (buildMode)
        {
            objMove.enabled = true;
        }
        if (!buildMode)
        {
            objMove.enabled = false;
        }
    }
}
