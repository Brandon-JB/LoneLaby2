using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ClickDebugger : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectClick();
        }
    }

    void DetectClick()
    {
        // Create a pointer event at the mouse position
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        // Raycast all objects under the pointer
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, raycastResults);

        if (raycastResults.Count > 0)
        {
            Debug.Log("Raycast hit:");
            foreach (var result in raycastResults)
            {
                Debug.Log($"- {result.gameObject.name}");
            }
        }
        else
        {
            Debug.Log("Raycast hit nothing.");
        }
    }
}
