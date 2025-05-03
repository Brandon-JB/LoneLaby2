using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bedQuickFix : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(BossSaveData.GetNumberOfBossesObtained() < 3)
        {
            this.gameObject.SetActive(false);
        }
    }
}
