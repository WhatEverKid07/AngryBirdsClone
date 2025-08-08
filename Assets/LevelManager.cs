using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class LevelObjectData
{
    public string prefabName;
    public Vector3 position;
    public Quaternion rotation;
}

[System.Serializable]
public class LevelData
{
    public List<LevelObjectData> objects = new List<LevelObjectData>();
}


public class LevelManager : MonoBehaviour
{
    [Header("Available Objects to Place")]
    public GameObject[] placeablePrefabs; // Assign in Inspector

    [Header("Save Settings")]
    public string saveFileName = "customLevel.json";

    private List<GameObject> placedObjects = new List<GameObject>();

    // Called by UI button to place a specific prefab
    public void PlaceObject(int prefabIndex)
    {
        if (prefabIndex < 0 || prefabIndex >= placeablePrefabs.Length) return;

        Vector3 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnPos.z = 0f;
        spawnPos.y += 0.5f;
        GameObject obj = Instantiate(placeablePrefabs[prefabIndex], spawnPos, Quaternion.identity);
        obj.transform.localScale = Vector3.one * 0.25f;
        placedObjects.Add(obj);
    }

    // Save all placed objects to JSON
    public void SaveLevel()
    {
        LevelData data = new LevelData();

        foreach (var obj in placedObjects)
        {
            LevelObjectData objData = new LevelObjectData();
            objData.prefabName = obj.name.Replace("(Clone)", "").Trim();
            objData.position = obj.transform.position;
            objData.rotation = obj.transform.rotation;

            data.objects.Add(objData);
        }

        string json = JsonUtility.ToJson(data, true);
        string path = Path.Combine(Application.persistentDataPath, saveFileName);
        File.WriteAllText(path, json);

        Debug.Log("Level saved to: " + path);
    }

    // Load a level from JSON
    public void LoadLevel()
    {
        string path = Path.Combine(Application.persistentDataPath, saveFileName);
        if (!File.Exists(path))
        {
            Debug.LogWarning("No saved level found.");
            return;
        }

        // Clear existing objects
        foreach (var obj in placedObjects)
        {
            Destroy(obj);
        }
        placedObjects.Clear();

        // Load and spawn
        string json = File.ReadAllText(path);
        LevelData data = JsonUtility.FromJson<LevelData>(json);

        foreach (var objData in data.objects)
        {
            GameObject prefab = FindPrefabByName(objData.prefabName);
            if (prefab != null)
            {
                GameObject obj = Instantiate(prefab, objData.position, objData.rotation);
                placedObjects.Add(obj);
            }
        }

        Debug.Log("Level loaded.");
    }

    // Helper: find prefab by name
    private GameObject FindPrefabByName(string name)
    {
        foreach (var prefab in placeablePrefabs)
        {
            if (prefab.name == name)
                return prefab;
        }
        Debug.LogWarning("Prefab not found for: " + name);
        return null;
    }
}