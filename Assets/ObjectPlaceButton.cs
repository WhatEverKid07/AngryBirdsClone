using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlaceButton : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private int itemPrefabIndex;
    [SerializeField] private RectTransform myButtonRect;

    public void IndexPlacing()
    {
        levelManager.PlaceObject(itemPrefabIndex, myButtonRect);
    }
}