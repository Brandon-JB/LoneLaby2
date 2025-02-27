using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarknessManager : MonoBehaviour
{
    public Image DarknessImage;
    public Sprite[] sprites;
    public float ChangeTime = 30f;
    public float timer = 0f;

    private int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= ChangeTime) 
        {
            timer = 0f;
            currentIndex++;
            DarknessImage.sprite = sprites[currentIndex];
        }

        if(InputManager.magicPressed)
        {
            currentIndex = 0;
            timer = 0f;
            DarknessImage.sprite = sprites[currentIndex];
        }
    }


}
