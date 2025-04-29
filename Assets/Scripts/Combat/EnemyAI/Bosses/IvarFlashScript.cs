using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvarFlashScript : MonoBehaviour
{
    public bool expand;
    public Transform flashTransform;

    private void Start()
    {
        expand = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (expand)
        {
            flashTransform.localScale = new Vector2 (flashTransform.localScale.x + 3, flashTransform.localScale.y + 3);
        }
        else
        {
            flashTransform.localScale = Vector2.zero;
        }
    }
}
