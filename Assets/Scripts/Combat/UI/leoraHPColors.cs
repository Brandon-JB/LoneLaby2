using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class leoraHPColors : MonoBehaviour
{
    //on slider value changed
    [SerializeField] Color highHP, midHP, lowHP;
    private LeoraChar2 leora;
    [SerializeField] Image fillcolor;

    private void Start()
    {
        leora = new LeoraChar2();
    }

    public void checkHPAmount()
    {
        //If Leora wasn't found, LOOK AGAIN!
        if (leora == null)
        {
            leora = FindObjectOfType<LeoraChar2>();
        }

        //Find Leora's hp and check how high it is
        if (leora.GetHealth() >= leora.GetMaxHealth()*0.66)
        {
            //HP is really high
            fillcolor.color = highHP;
        }
        else if (leora.GetHealth() <= leora.GetMaxHealth() * 0.66 && leora.GetHealth() >= leora.GetMaxHealth() * 0.33)
        {
            //HP is mid
            fillcolor.color = midHP;
        }
        else
        {
            //HP is REALLY low
            fillcolor.color = lowHP;
        }
    }
}
