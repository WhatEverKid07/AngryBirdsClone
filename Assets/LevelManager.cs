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
    [SerializeField] private Camera worldCamera;  // Camera rendering your 3D world
    [SerializeField] private Canvas canvas;       // UI canvas
    [SerializeField] private float offsetY = 0.5f;

    [Header("Available Objects to Place")]
    [SerializeField] private GameObject[] placeablePrefabs;

    [Header("Save Settings")]
    [SerializeField] private string saveFileName = "customLevel.json";

    private List<GameObject> placedObjects = new List<GameObject>();

    [Header("Play game settings")]
    // Fpr bricks
    private BrickManager brickManager;
    private List<BrickManager> brickManagerScripts = new List<BrickManager>();

    // For Pigs
    private PigManager pigManager;
    private List<PigManager> pigManagerScripts = new List<PigManager>();

    public void PlaceObject(int prefabIndex, RectTransform uiButtonRect)
    {
        if (prefabIndex < 0 || prefabIndex >= placeablePrefabs.Length) return;

        Vector3 worldPos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            uiButtonRect,
            RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, uiButtonRect.position),
            worldCamera,
            out worldPos))
        {
            Vector3 spawnPos = worldPos + Vector3.up * offsetY;
            spawnPos.z = 0f;

            GameObject obj = Instantiate(placeablePrefabs[prefabIndex], spawnPos, Quaternion.identity);
            obj.transform.localScale = Vector3.one * 0.25f;
            placedObjects.Add(obj);

            brickManager = obj.GetComponent<BrickManager>();
            pigManager = obj.GetComponent<PigManager>();
            if (brickManager != null)
            {
                brickManagerScripts.Add(brickManager);
                brickManager = null;
            }
            else if (pigManager != null)
            {
                pigManagerScripts.Add(pigManager);
                pigManager = null;
            }
        }
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

    public void PlayGame()
    {
        foreach (BrickManager brickScript in brickManagerScripts)
        {
            brickScript.SetLocation();
            brickScript.buildMode = !brickScript.buildMode;
        }
        foreach (PigManager pigScript in pigManagerScripts)
        {
            pigScript.SetLocation();
            pigScript.buildMode = !pigScript.buildMode;
        }
    }

    public void BuildGame()
    {
        foreach (BrickManager brickScript in brickManagerScripts)
        {
            brickScript.ResetLocation();
            brickScript.buildMode = !brickScript.buildMode;
        }
        foreach (PigManager pigScript in pigManagerScripts)
        {
            pigScript.ResetLocation();
            pigScript.buildMode = !pigScript.buildMode;
        }
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