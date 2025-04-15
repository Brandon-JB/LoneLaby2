using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteraction : CombatInteraction
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject ObjectSpawnPosition;
    [SerializeField] private DropManager dropManager;
    [SerializeField] private EquipmentManager equipmentManager;
    [SerializeField] private string whichRing;
    public int itemNumber;
    [SerializeField] private ItemMenu itemMenu;

    [SerializeField] private GameObject Door;

    // Start is called before the first frame update
    void Start()
    {
        itemMenu = FindObjectOfType<ItemMenu>();
        dropManager = FindObjectOfType<DropManager>();
        equipmentManager = FindObjectOfType<EquipmentManager>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (DistanceBetweenObjectAndPlayer <= interactRange)
        {
            if (!animator.GetBool("Open") && InputManager.interactPressed)
            {
                //Put interactions here
                animator.SetBool("Open", true);
            }
        }
    }

    public void ChestDrop()
    {
        switch (itemNumber)
        {
            case 0:
                //Large HP
                dropManager.SpecificDrop(ObjectSpawnPosition.transform.position, "Large HP");
                break;
            case 1:
                //Large MP
                dropManager.SpecificDrop(ObjectSpawnPosition.transform.position, "Large MP");
                break;
            case 3:
                //Ring
                //ATKRing
                //MPRing
                //HPRing
                itemMenu.openItemMenu(whichRing);
                break;
            case 4:
                //Enemies
                break;
            case 5:
                Door.SetActive(false);
                audioManager.Instance.playSFX(43);
                break;
            default:
                Debug.Log("Haha pranked idiot");
                break;
        }
    }
}
