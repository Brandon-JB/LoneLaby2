using System;
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

    public static int GetNumberOfBossesObtained()
    {
        return Convert.ToInt32((bossStates["Ivar"] != 0)) + Convert.ToInt32((bossStates["Lucan"] != 0)) + Convert.ToInt32((bossStates["Viin"] != 0));
    }

    public static int GetNumberOfCondemned() //GET PEOPLE KILLED
    {
        return Convert.ToInt32((bossStates["Ivar"] == 1)) + Convert.ToInt32((bossStates["Lucan"] == 1)) + Convert.ToInt32((bossStates["Viin"] == 1));
    }
    public static int GetNumberOfSaved() // GET PEOPLE WHO LIVED
    {
        return Convert.ToInt32((bossStates["Ivar"] == 2)) + Convert.ToInt32((bossStates["Lucan"] == 2)) + Convert.ToInt32((bossStates["Viin"] == 2));
    }
}
