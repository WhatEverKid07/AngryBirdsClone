using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomLevelManager : MonoBehaviour
{
    [SerializeField] private GameObject newLevelIcon;

    [SerializeField] private GameObject[] possibleIconLocation;

    public void CreateNewLevel()
    {
        foreach (GameObject location in possibleIconLocation)
        {
            if (location.transform.childCount == 0)
            {
                GameObject newIcon = Instantiate(newLevelIcon, location.transform.position, location.transform.rotation);
                newIcon.transform.SetParent(location.transform, true);
                break;
            }
        }
    }
}
