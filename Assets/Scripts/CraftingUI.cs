using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

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
    public GameObject craftSlotPrefab;
    public Transform craftingDesignsPanel;

    // Start is called before the first frame update
    void Start()
    {
        //you must build the crafting table from the elements;
        //UpdateCraftingDisplay();    //is done at start and then each time you pick something up
        craftingDesignsPanel = craftingPanel.transform.GetChild(0);
        Assert.IsNotNull(craftingDesignsPanel);
        SetupCraftingDisplay();
    }//Start

    // Update is called once per frame
    void Update()
    {
        eKeyPanel.gameObject.SetActive(eKeyDisplaying);
        craftingPanel.gameObject.SetActive(eCraftingDisplaying);
        inventoryPanel.gameObject.SetActive(eInventoryDisplaying);
        //for debugging
        //SetupCraftingDisplay();
    }//Update

    internal void displayEkey(bool displayOrNot) {
        eKeyDisplaying = displayOrNot;
    }//F

    internal void craftingOnOff() {
        //throw new NotImplementedException();
        //if(eKeyDisplaying)
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

    private void UpdateCraftingDisplay() {
        //throw new NotImplementedException();
        //1. for each craftable. add a tile to the CraftingPanel
    }

    private void SetupCraftingDisplay() {
        //throw new NotImplementedException();
        //Done once - finds all the craftable elements in the game and adds them to the panel

        //GameObject[] AllCraftables = GameObject.FindGameObjectsWithTag("craftable"); //these will be prefab tiles that can be placed

        /*GameObject[] Craftables = Resources.FindObjectsOfTypeAll<GameObject>()
            .Where(go => go.CompareTag("craftables") &&
            PrefabUtility.IsPartOfPrefabAsset(go))
            .ToArray();*/

        //Get all objects in the Prefabs folder
        GameObject[] allPrefabs = Resources.LoadAll<GameObject>("craftables");

        // Filter by tag
        List<GameObject> AllCraftables = new List<GameObject>();
        foreach (GameObject prefab in allPrefabs) {
            if (prefab.CompareTag("craftable")) {
                AllCraftables.Add(prefab);
            }
        }
        Assert.IsNotNull(AllCraftables); //this cannot happen
        Assert.AreNotEqual(AllCraftables.Count, 0);

        craftingDesignsPanel.DetachChildren();

        for(int i=0;i<AllCraftables.Count;i++) {
            GameObject craftSlot = Instantiate(craftSlotPrefab, craftingDesignsPanel);
            if (craftSlot != null) {
                Image iconImage = craftSlot.GetComponent<Image>();
                if (iconImage != null) {
                    iconImage.sprite = AllCraftables[i].GetComponent<SpriteRenderer>().sprite;
                }// if not null
            }//if not null

        }//for

        void OnPointerClick(GameObject go) {
            Debug.Log("clicked a blueprint");
        }

    }//class
}
