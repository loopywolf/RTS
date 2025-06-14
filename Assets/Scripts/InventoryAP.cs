using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryAP : MonoBehaviour
{
    List<ItemAP> inventorySlot;
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

    private void refreshDisplay()
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

    internal void addMat(int material) {
        //throw new NotImplementedException();

        //Add this mat to your inventory display
        //first, we need a key-value pair for inventory..done

        //1. If we already have some of this material, increment it
        //1a.  Find this material in our inventory

        MaterialSlot ms = getMaterialSlot(material);
        if(ms==null) { //so make a new one
            //materialsInventory[material] = 1;  //amount has to be set to 1 - it is set by default
            GameObject go = Instantiate(uiMatSlotPrefab, uiMaterialsPanel.transform);
            ms = go.GetComponent<MaterialSlot>();
            ms.setMaterialIndex(material);
            ms.setAmount(1);    //could be by default - the prefab starts at 1
            Sprite s = MaterialSprites[material].GetComponent<SpriteRenderer>().sprite;
            go.GetComponent<Image>().sprite = s;
        } else { //it found the slot
            //if(materialsInventory.ContainsKey(material)) {
            ms.increment();
        }//if

        Debug.Log("added mat=" + material);
    }//F

    private MaterialSlot getMaterialSlot(int material) {
        //throw new NotImplementedException();

        //searches all existing MaterialSlots for one with this material as its materialIndex
        foreach (Transform child in uiMaterialsPanel.transform) {
            // Do something with the child
            MaterialSlot ms = child.GetComponent<MaterialSlot>();
            if (ms.materialIndex == material)
                return ms;
        }//for
        
        return null;
    }//F
}//class
