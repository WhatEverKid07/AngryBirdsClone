using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ItemManagement : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public List<Button> buttons;
    public List<GameObject> objects;

    private void Start()
    {
        // Make sure both lists are the same length
        if (buttons.Count != objects.Count)
        {
            Debug.LogError("Buttons and Objects lists must have the same number of elements!");
            return;
        }

        // Add listeners dynamically
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i; // Local copy to avoid closure issue
            buttons[i].onClick.AddListener(() => ToggleObject(index));
        }
    }

    private void ToggleObject(int index)
    {
        if (!objects[index].activeSelf)
        {
            // Disable all
            foreach (var obj in objects)
                obj.SetActive(false);

            // Enable this one
            objects[index].SetActive(true);
        }
        else
        {
            // If it's already enabled, turn it off
            objects[index].SetActive(false);
        }
    }
}
