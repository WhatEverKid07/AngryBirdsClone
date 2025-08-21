using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLevelManager : MonoBehaviour
{
    [System.Serializable]
    private class SavedObject
    {
        public GameObject prefab;
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
    }

    private List<SavedObject> birdList = new List<SavedObject>();
    private List<SavedObject> brickList = new List<SavedObject>();

    // --- Call this to SAVE ---
    public void SaveLevel()
    {
        birdList.Clear();
        brickList.Clear();

        // Save Birds
        GameObject[] birds = GameObject.FindGameObjectsWithTag("Bird");
        foreach (GameObject b in birds)
        {
            SavedObject so = new SavedObject();
            so.prefab = b;
            so.position = b.transform.position;
            so.rotation = b.transform.rotation;
            so.scale = b.transform.localScale;
            birdList.Add(so);
        }

        // Save Bricks
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
        foreach (GameObject br in bricks)
        {
            SavedObject so = new SavedObject();
            so.prefab = br;
            so.position = br.transform.position;
            so.rotation = br.transform.rotation;
            so.scale = br.transform.localScale;
            brickList.Add(so);
        }
    }

    // --- Call this to LOAD ---
    public void LoadLevel()
    {
        // Clear existing Birds & Bricks
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Bird"))
            Destroy(b);
        foreach (GameObject br in GameObject.FindGameObjectsWithTag("Brick"))
            Destroy(br);

        // Restore Birds
        foreach (SavedObject so in birdList)
        {
            GameObject obj = Instantiate(so.prefab, so.position, so.rotation);
            obj.transform.localScale = so.scale;
        }

        // Restore Bricks
        foreach (SavedObject so in brickList)
        {
            GameObject obj = Instantiate(so.prefab, so.position, so.rotation);
            obj.transform.localScale = so.scale;
        }
    }
}
