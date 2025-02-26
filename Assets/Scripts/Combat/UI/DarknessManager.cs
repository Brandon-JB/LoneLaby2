using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarknessManager : MonoBehaviour
{
    public Image DarknessImage;
    public Sprite[] sprites;
    public float ChangeTime = 5f;

    private int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AdvanceDarkness());
    }

    void Update()
    {
        if(InputManager.magicPressed)
        {
            currentIndex = 0;
            DarknessImage.sprite = sprites[currentIndex];
        }
    }

    IEnumerator AdvanceDarkness()
    {
        while (true)
        {
            yield return new WaitForSeconds(ChangeTime);
            currentIndex++;
            DarknessImage.sprite = sprites[currentIndex];
        }
    }
}
