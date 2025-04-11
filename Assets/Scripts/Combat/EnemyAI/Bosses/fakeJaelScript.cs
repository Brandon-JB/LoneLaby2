using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fakeJaelScript : MonoBehaviour
{
    [SerializeField] public Animator animator;

    [SerializeField] private GameObject fakeAxePrefab;
    [SerializeField] private GameObject axeSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopAttackAnim()
    {
        animator.SetBool("Attacking", false);
    }

    public void TriggerAttackAnim()
    {
        animator.SetBool("Attacking", true);
    }

    public void ThrowAxe()
    {
        GameObject Axe = Instantiate(fakeAxePrefab, axeSpawnPoint.transform.position, Quaternion.identity);
        JaelAxe axeScript = Axe.GetComponent<JaelAxe>();
    }


}
