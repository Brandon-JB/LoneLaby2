using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSaveData : MonoBehaviour
{
    /// <summary>
    /// 0 - Still alive
    /// 1 - Killed
    /// 2 - Spared
    /// </summary>
    static public Dictionary<string, int> bossStates = new Dictionary<string, int>
    {
        {"Ivar", 0 },
        {"Lucan", 0 },
        {"Viin", 0 }
    };
}
