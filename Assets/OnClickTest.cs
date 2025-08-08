using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnAndDragWorld : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject prefabToSpawn;
    public LayerMask raycastLayerMask; // Layer(s) you want to place the prefab on

    private GameObject currentDraggedObject;
    private bool isDragging;

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 spawnPos = GetMouseWorldPosition();
        currentDraggedObject = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        currentDraggedObject = null;
    }

    void Update()
    {
        if (isDragging && currentDraggedObject != null)
        {
            Vector3 targetPos = GetMouseWorldPosition();
            currentDraggedObject.transform.position = targetPos;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, raycastLayerMask))
        {
            return hitInfo.point;
        }
        return Vector3.zero; // fallback if nothing is hit
    }
}
