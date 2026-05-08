using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomLevelManager : MonoBehaviour
{
    public static CustomLevelManager Instance;

    [SerializeField] private GameObject newLevelIcon;
    [SerializeField] internal Transform[] possibleIconLocation;
    public Transform iconParent;
    private void Awake()
    {
        Instance = this;
    }

    public void CreateNewLevel()
    {
        for (int i = 0; i < possibleIconLocation.Length; i++)
        {
            if (possibleIconLocation[i].childCount == 0)
            {
                LevelIconData icon = new LevelIconData();
                icon.placeholderIndex = i;

                icon.levelID = Random.Range(10000, 99999).ToString();

                int a = icon.placeholderIndex + 1;
                icon.iconName = "NewLevel_" + a;

                CL_ButtonManager.Instance.icons.Add(icon);

                // Save menu data only
                LevelManager.Instance.SaveLevelIcon(icon);

                CreateIconFromData(icon);
                break;
            }
        }
    }
    public void FreeSlot(int index)
    {
        // Just ensure the slot is empty
        if (possibleIconLocation[index].childCount > 0)
        {
            foreach (Transform child in possibleIconLocation[index])
                Destroy(child.gameObject);
        }

        Debug.Log("Slot freed: " + index);
    }

    public void CreateIconFromData(LevelIconData data)
    {
        Transform spot = possibleIconLocation[data.placeholderIndex];

        GameObject icon = Instantiate(newLevelIcon, spot);
        MenuLevelButton btn = icon.GetComponent<MenuLevelButton>();
        btn.Setup(data);
    }
}