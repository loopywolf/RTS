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

    // Start is called before the first frame update
    void Start()
    {
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
}//class
