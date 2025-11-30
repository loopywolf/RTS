using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class InventoryAP : MonoBehaviour
{
    List<ItemAP> inventorySlot;    //deprecated
    private const int INVENTORY_SIZE = 12;
    public GameObject inventorySlotPrefab; //deprecated but still functional
    public GameObject inventorySlotsParent; //deprecated but still functional
    private static bool uiShowing = false;
    public GameObject inventoryUiPanel;
    public List<GameObject> MaterialSprites; //no, let's leave it here and leave the functions there
    //private Hashtable materialsInventory;
    //private Dictionary<int, int> materialsInventory;
    public GameObject uiMaterialsPanel; //panel
    public GameObject uiMatSlotPrefab;   //link to the prefab
    //Taking over the crafting requirements panel
    public GameObject uiRequiredPanel; //panel

    //TODO once it works, removes the dummy entry

    private void Awake()
    {
        inventorySlot = new List<ItemAP>(); //Item[INVENTORY_SIZE];        
        //Materials = new List<GameObject>(); 
        //materialsInventory = new Hashtable();
        //materialsInventory = new Dictionary<int, int>(); - we use the graphics instead
    }//F

    internal void add(GameObject go)
    {
        //1. add item to internal data (inventory) 
        /*Item i = go.GetComponent<Item>();
        if (i == null)
        {
            Debug.Log("No Item");
            return;
        }//if
        Debug.Log("i:inventory adding");
        Debug.Log("i:go=" + go.name);*/
        if(go.GetComponent<MyTile>() == null ||
            !go.GetComponent<MyTile>().hasItem)
        {
            Debug.Log("Not an item.  cannot add");
            return;
        }

        //i.amount = 1;   //work on stacking later //TODO
        ItemAP itemAdded = null;
        Sprite s = go.GetComponent<SpriteRenderer>().sprite; //would sprite name do for material?
        if (s != null)
        {
            Debug.Log("i:s=" + s.name);
            itemAdded = new ItemAP(s);
            inventorySlot.Add(itemAdded);
            Debug.Log("added inventory item " + itemAdded.icon.name);
            refreshDisplay();
        }
        else
        {
            Debug.Log("Sprite not found for go=" + go.name);
        }//if s OK

        /*  //try to give item a sprite based on the parent
            s = go.transform.parent.GetComponent<SpriteRenderer>().sprite;
            i.icon = s;
            if(s!=null)
            Debug.Log("i:2nd s=" + s.name); */


        //throw new NotImplementedException();
    }//F

    private void refreshDisplay() //this seems common between CraftingUI.. Maybe it could be abstracted
    {
        //2a. get container - got
        //2b. clear container
        foreach (Transform child in inventorySlotsParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }//for clear
        Debug.Log("removed children");
        //works to here!

        //2c. add one prefab per inventorySlot
        foreach (var inv in inventorySlot)
        {
            GameObject newSlot = Instantiate(inventorySlotPrefab, inventorySlotsParent.transform);
            //now I want to reference the Image component under Icon
            //Transform icon = newSlot.transform.Find("icon"); //this isn't working
            Transform icontf = newSlot.transform.GetChild(0); //clumsy... //TODO

            if (icontf != null)
            {
                Image iconImage = icontf.gameObject.GetComponent<Image>();
                if (iconImage != null)
                {
                    iconImage.sprite = inv.icon;
                    Debug.Log("display changed " + iconImage.sprite.name);
                }//if iconImage OK
            }//if icon OK
            //Image.sprite - remember */
        }//for 

    }//F

    public void switchInventoryOnOff()
    {
        //throw new NotImplementedException();
        uiShowing = !uiShowing;
        inventoryUiPanel.SetActive(uiShowing);
    }//F

    internal GameObject getDropMaterial(int index) {
        //throw new NotImplementedException();
        Debug.Log("Materials in memory " + MaterialSprites.Count);
        if (index < MaterialSprites.Count)
            return MaterialSprites[index];
        else
            return null;
    }//F

    internal void addMat(GameObject healthCollectible) {
        //throw new NotImplementedException();

        //Add this mat to your inventory display
        //first, we need a key-value pair for inventory..done

        //1. If we already have some of this material, increment it
        //1a.  Find this material in our inventory

        GameObject hc = healthCollectible;

        MaterialSlot ms = getMaterialSlot( hc );
        if(ms==null) { //so make a new one             
            //materialsInventory[material] = 1;  //amount has to be set to 1 - it is set by default
            GameObject go = Instantiate(uiMatSlotPrefab, uiMaterialsPanel.transform);   //adds the material slot prefab
            //go.transform.localScale = Vector3.one; //wasn't this
            //set sprite
            Sprite s = hc.GetComponent<SpriteRenderer>().sprite;
            Assert.IsNotNull(s);    //should never be null
            Assert.IsNotNull(go);   //should never be null;
            go.GetComponent<Image>().sprite = s;    //should be safe
            //amount has been set automatically to 1 for a new instantiation
        } else { //it found the slot
            //if(materialsInventory.ContainsKey(material)) {
            ms.increment();
        }//if

        Debug.Log("added mat=" + hc.name);
    }//F

    private MaterialSlot getMaterialSlot(GameObject healthCollectible) {    //identify the slot in the ui that houses this material
        //throw new NotImplementedException();

        //searches all existing MaterialSlots for one with this material as its materialIndex
        foreach (Transform child in uiMaterialsPanel.transform) {
            // Do something with the child
            Image i = child.GetComponent<Image>();
            Assert.IsNotNull(i);    //should never be null
            //Sprite hcs = healthCollectible.GetComponent<SpriteRenderer>().sprite; //this is your problem - There is no spriterenderer attached to MaterialSlot(clone)
            Sprite hcs = healthCollectible.GetComponent<Image>().sprite;

            //get the MaterialSlot.UI.Image;
            Assert.IsNotNull(hcs); //should never be null
            if (i.sprite == hcs)
                return child.GetComponent<MaterialSlot>();
        }//for
        
        return null;
    }//F

    public void addRequired(GameObject healthCollectible) {
        //trying to get this to work by duplicating Inventory

        GameObject hc = healthCollectible;

        //MaterialSlot ms = getMaterialSlot(hc); //not applicable
        //if (ms == null) { //so make a new one             
        //materialsInventory[material] = 1;  //amount has to be set to 1 - it is set by default

        clearRequired();
        GameObject go = Instantiate(uiMatSlotPrefab, uiRequiredPanel.transform);   //adds the material slot prefab
        Sprite s = hc.GetComponent<SpriteRenderer>().sprite;
        Assert.IsNotNull(s);    //should never be null
        Assert.IsNotNull(go);   //should never be null;
        go.GetComponent<Image>().sprite = s;    //should be safe

        //amount has been set automatically to 1 for a new instantiation
        /*  } else { //it found the slot
        //if(materialsInventory.ContainsKey(material)) {
        ms.increment();
        }//if */

        Debug.Log("added required=" + hc.name);

        //now check if we have those requirements to see if the button should be disabled
        //bool placeButtonEnabled = areRequirementsSatisfied(); //can I bring this out of this function?

        //TODO enable or disable button
    }//F

    private void clearRequired() {
        //throw new NotImplementedException();
        for (int i = uiRequiredPanel.transform.childCount - 1; i >= 0; i--) {
            Destroy(uiRequiredPanel.transform.GetChild(i).gameObject);
        }
    }//F

    public bool areRequirementsSatisfied() {
        for (int i = uiRequiredPanel.transform.childCount - 1; i >= 0; i--) {
            if (!hasEnoughOfMaterial(uiRequiredPanel.transform.GetChild(i).gameObject)) return false;
            MaterialSlot ms = getMaterialSlot(uiRequiredPanel.transform.GetChild(i).gameObject);
            if (ms == null) return false;
        }//for

        return true; //this is working (pretty sure)
    }//F

    private bool hasEnoughOfMaterial(GameObject gameObject) {
        //throw new NotImplementedException();
        Debug.Log("Do I have enough:" + gameObject.name);
        return true;
    }//F

    internal bool enablePlaceButton(GameObject foundMaterial) {
        //throw new NotImplementedException();
        //Assert.IsNotNull(this.inventorySlot);
        //if (this.inventorySlot.Count == 0) return false;    //we have NO materials
        Assert.IsNotNull(uiMaterialsPanel);
        for (int i = 0; i < uiMaterialsPanel.transform.childCount; i++) {
            Transform t = uiMaterialsPanel.transform.GetChild(i);
            MaterialSlot ms = t.GetComponent<MaterialSlot>();
            Debug.Log("Material Slot " + ms);
            Assert.IsNotNull(ms);
        }

        //attempt 2 - we try by name 9.9

        //by default return true.. but the first fail condition returns FALSE
        return false;
    }//F
}//class
