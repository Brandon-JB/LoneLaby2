using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DemoCheck
{
    private static bool isDemo = true;

    public static bool getDemo()
    {
        return isDemo;
    }

    //No set because this is set by us
}
