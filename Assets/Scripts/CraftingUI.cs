using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUI : MonoBehaviour
{
    public GameObject eKeyPanel;
    public GameObject craftingPanel;
    public GameObject inventoryPanel;
    private bool eKeyDisplaying = false;
    private bool eInventoryDisplaying = false;
    private bool eCraftingDisplaying = false;
    public static int MAX_CRAFTING_RANGE = 7;
    public List<GameObject> CraftableBlocks;

    // Start is called before the first frame update
    void Start()
    {
        //you must build the crafting table from the elements;
    }//Start

    // Update is called once per frame
    void Update()
    {
        eKeyPanel.gameObject.SetActive(eKeyDisplaying);
        craftingPanel.gameObject.SetActive(eCraftingDisplaying);
        inventoryPanel.gameObject.SetActive(eInventoryDisplaying);
    }//Update

    internal void displayEkey(bool displayOrNot) {
        eKeyDisplaying = displayOrNot;
    }//F

    internal void craftingOnOff() {
        //throw new NotImplementedException();
        if(eKeyDisplaying)
            eCraftingDisplaying = !eCraftingDisplaying;
        eInventoryDisplaying = !eInventoryDisplaying;
        Debug.Log("Crafting On");
    }

    void updateCraftingTable() {
        //whenever player picks up or drops a material, this gets called
        //this fills the crafting table with all the possible blocks the player can put down
        //II. If the fab-o-mat is destroyed, the only thing the player can craft is the fab-o-mat
        //for each of the craftable blocks below, it checks for the necessary materials and returns a) craftable(normal) b) missing some materials(grey) c) not shown? materials

        //0. Wall - requires 1 silicate
        //if (hasMats(Material.Silicate, 1)) Instantiate(WallPrefab, craftingPanel.transform, Quaternion.identity);
    }

}//class
