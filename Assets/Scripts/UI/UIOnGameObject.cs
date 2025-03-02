using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIOnGameObject : MonoBehaviour
{
    public Canvas uiCanvas; // Assign your Canvas in the Inspector

    public Transform targetPos;

    public RectTransform spawnUiOnGameObject(string objectName)
    {
        uiCanvas = GameObject.FindGameObjectWithTag("BattleUI").GetComponent<Canvas>();

        // Convert world position to screen position
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        // Create a new UI GameObject
        GameObject uiElement = new GameObject("ClonedUIObject");
        uiElement.transform.SetParent(uiCanvas.transform, false);

        // Add an Image component
        Image uiImage = uiElement.AddComponent<Image>();

        // Copy the sprite from the original object
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            uiImage.sprite = spriteRenderer.sprite;
            uiImage.SetNativeSize(); // Makes the UI match the sprite’s natural size
        }

        // Get the RectTransform and set position
        RectTransform rectTransform = uiElement.GetComponent<RectTransform>();
        rectTransform.position = screenPos;

        // Fix size: Convert world size to screen size
        Vector3 worldSize = spriteRenderer.bounds.size;
        Vector3 screenSize = Camera.main.WorldToScreenPoint(transform.position + worldSize) - Camera.main.WorldToScreenPoint(transform.position);

        rectTransform.sizeDelta = new Vector2(screenSize.x * 0.5f, screenSize.y * 0.5f);
        return rectTransform;
    }
}
