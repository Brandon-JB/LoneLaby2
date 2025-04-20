using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ivarText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] inGameText1, inGameText2;
    [SerializeField] string[] textbatch1, textbatch2, textbatch3, textbatch4;

    private int ivarCounter;
    private float totalDuration;
    private float intervalDuration;
    private float timer;
    private int intervalsTriggered;
    private int area;

    private List<string> unusedStrings = new List<string>();

    public void openText(int area, float duration = 30f)
    {
        this.area = area;
        // Set timer variables
        totalDuration = duration;
        intervalDuration = totalDuration / 4f;
        timer = 0f;
        intervalsTriggered = 0;

        // Prepare string pool
        unusedStrings = new List<string>(textbatch1);

        // Start coroutine to update over time
        StartCoroutine(TextOverTime());
    }

    private IEnumerator TextOverTime()
    {
        while (timer < totalDuration)
        {
            timer += Time.deltaTime;

            if (timer >= intervalDuration * (intervalsTriggered + 1))
            {
                //[condition] ? [code to be run if condition is true] : [code to be run if condition is false];
                Debug.Log("Text should be changed");
                TriggerTextUpdate(area == 1 ? inGameText1 : inGameText2);
                intervalsTriggered++;
                switch (intervalsTriggered)
                {
                    case 0:
                        break;
                    case 1:
                        unusedStrings = new List<string>(textbatch1);
                        break;
                    case 2:
                        unusedStrings = new List<string>(textbatch2);
                        break;
                    case 3:
                        unusedStrings = new List<string>(textbatch3);
                        break;
                    case 4:
                        unusedStrings = new List<string>(textbatch4);
                        break;
                }
            }

            yield return null;
        }
    }

    private void TriggerTextUpdate(TextMeshProUGUI[] textToModify)
    {
        foreach (var textElement in textToModify)
        {
            string chosen = GetUniqueRandomString();
            textElement.text = chosen;
        }
    }


    private string GetUniqueRandomString()
    {
        if (unusedStrings.Count == 0)
        {
            switch (intervalsTriggered)
            {
                case 0:
                    break;
                case 1:
                    unusedStrings = new List<string>(textbatch1);
                    break;
                case 2:
                    unusedStrings = new List<string>(textbatch2);
                    break;
                case 3:
                    unusedStrings = new List<string>(textbatch3);
                    break;
                case 4:
                    unusedStrings = new List<string>(textbatch4);
                    break;
            }
        }
       
        int index = Random.Range(0, unusedStrings.Count);
        Debug.Log(index);
        Debug.Log(unusedStrings.Count);
        string selected = unusedStrings[index];
        unusedStrings.RemoveAt(index);
        return selected;
    }
}
