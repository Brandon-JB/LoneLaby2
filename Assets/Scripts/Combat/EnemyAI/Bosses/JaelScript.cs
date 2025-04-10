using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaelScript : MonoBehaviour
{
    [SerializeField] private Vector2 bottomLeftBounds;
    [SerializeField] private Vector2 topRightBounds;

    [SerializeField] private JaelChar jaelChar;

    [SerializeField] private int axeThrowCount;
    [SerializeField] private Cooldown throwingCooldown;
    [SerializeField] private GameObject axePrefab;
    [SerializeField] private GameObject axeSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ThrowAxe()
    {
        GameObject Axe = Instantiate(axePrefab, axeSpawnPoint.transform.position, Quaternion.identity);
        JaelAxe axeScript = Axe.GetComponent<JaelAxe>();
        axeScript.parentChar = this.jaelChar;
    }
}
