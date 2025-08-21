using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLevelManager : MonoBehaviour
{
    [System.Serializable]
    private class SavedObject
    {
        public string prefabName;
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
    }

    // --- Lists of prefabs you want to save/load ---
    [Header("Prefabs to Save/Load")]
    public List<GameObject> birdPrefabs = new List<GameObject>();
    public List<GameObject> brickPrefabs = new List<GameObject>();

    private List<SavedObject> birdList = new List<SavedObject>();
    private List<SavedObject> brickList = new List<SavedObject>();

    private List<GameObject> placedObjects = new List<GameObject>(); // Keep track of instantiated objects

    // --- Call this to SAVE ---
    public void SaveLevel()
    {
        birdList.Clear();
        brickList.Clear();

        // Save Birds
        foreach (GameObject prefab in birdPrefabs)
        {
            if (prefab == null) continue;

            SavedObject so = new SavedObject();
            so.prefabName = prefab.name;
            so.position = prefab.transform.position;
            so.rotation = prefab.transform.rotation;
            so.scale = prefab.transform.localScale;
            birdList.Add(so);
        }

        // Save Bricks
        foreach (GameObject prefab in brickPrefabs)
        {
            if (prefab == null) continue;

            SavedObject so = new SavedObject();
            so.prefabName = prefab.name;
            so.position = prefab.transform.position;
            so.rotation = prefab.transform.rotation;
            so.scale = prefab.transform.localScale;
            brickList.Add(so);
        }

        Debug.Log("Level saved.");
    }

    // --- Call this to LOAD ---
    public void LoadLevel()
    {
        // Destroy existing objects
        foreach (GameObject obj in placedObjects)
        {
            if (obj != null) Destroy(obj);
        }
        placedObjects.Clear();

        // Restore Birds
        foreach (SavedObject so in birdList)
        {
            GameObject prefab = birdPrefabs.Find(p => p.name == so.prefabName);
            if (prefab != null)
            {
                GameObject obj = Instantiate(prefab, so.position, so.rotation);
                obj.transform.localScale = so.scale;

                // Make sure the object and all components are enabled
                obj.SetActive(true);
                foreach (var comp in obj.GetComponents<MonoBehaviour>())
                    comp.enabled = true;

                placedObjects.Add(obj);
            }
        }

        // Restore Bricks
        foreach (SavedObject so in brickList)
        {
            GameObject prefab = brickPrefabs.Find(p => p.name == so.prefabName);
            if (prefab != null)
            {
                GameObject obj = Instantiate(prefab, so.position, so.rotation);
                obj.transform.localScale = so.scale;

                // Make sure the object and all components are enabled
                obj.SetActive(true);
                foreach (var comp in obj.GetComponents<MonoBehaviour>())
                    comp.enabled = true;

                placedObjects.Add(obj);
            }
        }

        Debug.Log("Level loaded.");
    }
}
