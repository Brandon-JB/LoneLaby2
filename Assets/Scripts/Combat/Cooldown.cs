using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Cooldown
{
    [SerializeField] private float cooldownTime;
    private float _nextFireTime;

    //Returns true if the current active game time is less than the _nextFireTime.
    public bool isCoolingDown => Time.time < _nextFireTime;

    //Sets the _nextFireTime variable to the set "cooldownTime" plus the current active game time.
    public void StartCooldown() => _nextFireTime = Time.time + cooldownTime;
}
