using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUI : MonoBehaviour
{
    public GameObject eKeyPanel;
    public GameObject craftingPanel;
    private bool eKeyDisplaying = false;

    // Start is called before the first frame update
    void Start()
    {
    }//Start

    // Update is called once per frame
    void Update()
    {
        eKeyPanel.gameObject.SetActive(eKeyDisplaying);
        craftingPanel.gameObject.SetActive(eKeyDisplaying);
    }//Update

    internal void displayEkey(bool displayOrNot) {
        eKeyDisplaying = displayOrNot;
    }//F
}//class
