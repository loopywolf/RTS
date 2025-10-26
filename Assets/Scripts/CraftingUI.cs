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
    //public List<GameObject> CraftableBlocks;    
    public GameObject craftSlotPrefab;
    public Transform craftingDesignsPanel;
    private CraftableSlot currentlySelectedBlueprint;
    //int selectedBlueprint = -1;
    //private List<GameObject> AllCraftables;
    public List<CraftableData> AllCraftables;
    public GameObject requirementsSlotsParent;
    public GameObject inventorySlotPrefab;
    public GameObject crossesDisplay;
    private CraftableData chosenRecipe = null;

    [System.Serializable]
    public class CraftableData
    {
        public Sprite sprite;
        public GameObject prefab;
    }//class

    // Start is called before the first frame update
    void Start()
    {
        //you must build the crafting table from the elements;
        //UpdateCraftingDisplay();    //is done at start and then each time you pick something up
        craftingDesignsPanel = craftingPanel.transform.GetChild(0);
        Assert.IsNotNull(craftingDesignsPanel);
        SetupCraftingDisplay(); //ya it was resetting itself every tick - ayoy

        //good to check
        Assert.IsNotNull(this.crossesDisplay);
        Assert.IsNotNull(crossesDisplay.GetComponent<CraftingCrosses>());

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
        /* CraftableData[] allPrefabs = Resources.LoadAll<CraftableData>("craftables");  //kind of like magic

        // Filter by tag
        //List<GameObject>
        AllCraftables = new List<CraftableData>();
        foreach (CraftableData ap in allPrefabs) {
            if (ap.prefab.CompareTag("craftable")) {
                AllCraftables.Add(ap);
            }
        } 
        Assert.IsNotNull(AllCraftables); //this cannot happen
        Assert.AreNotEqual(AllCraftables.Count, 0); */

        LoadAllCraftables();

        craftingDesignsPanel.DetachChildren();

        for(int i=0;i<AllCraftables.Count;i++) {
            GameObject craftSlot = Instantiate(craftSlotPrefab, craftingDesignsPanel.transform);
            Assert.IsNotNull(craftSlot.transform.parent);
            if (craftSlot != null) {
                //Image iconImage = craftSlot.GetComponent<Image>();
                Button iconButton = craftSlot.GetComponent<Button>();
                /* if (iconImage != null) {
                    iconImage.sprite = AllCraftables[i].GetComponent<SpriteRenderer>().sprite; */
                if (iconButton != null) {
                    iconButton.image.sprite = AllCraftables[i].sprite;
                    }// if not null
                }//if not null

        }//for

    }//class

    internal void setSelectedBlueprint(CraftableSlot craftableSlot) {
        //throw NotImplementedException();
        //1. resets the highlight on all blueprints
        CraftableSlot[] allCraftableSlots = craftingDesignsPanel.transform.GetComponentsInChildren<CraftableSlot>();
        foreach(CraftableSlot cs in allCraftableSlots) {
            cs.setHighlight(false);
        }
        //craftableSlot.setHighlight(true);
        //2. records which one is highlighted
        currentlySelectedBlueprint = craftableSlot;

        //3. Update REQUIRED display
        updateRequiredDisplay();
    }

    private void updateRequiredDisplay() {  //try this a totally different way
        //throw new NotImplementedException();
        //1. Check if Fab-o-Mat is nearby
        if(!eKeyDisplaying) { //then Fab-o-Mat is not near
            //add Fab-o-Mat icon
        }
        //2. Find list of required mats from currentlySelectedBluePrint
        if(currentlySelectedBlueprint!=null) {
            Image i = currentlySelectedBlueprint.GetComponent<Image>();
            Assert.IsNotNull(i);
            Sprite s = i.sprite;
            Assert.IsNotNull(s);

            //now we need to find the tile that has that exact same sprite = i.sprite
            CraftableData craftable = findCraftableBySprite(s); //a prefab
            Debug.Log("Craftable found " + craftable.sprite.name);
            //craftable is a gameobject with a MyTile and a sprite
            MyTile mt = craftable.prefab.GetComponent<MyTile>();
            Assert.IsNotNull(mt);
            this.chosenRecipe = craftable;  //sets the block to be created - should now be a prefab
            Debug.Log("requirements=" + mt.formulaForCrafting); //1silicate

            //1. clear requirements display
            /* foreach (Transform child in requirementsSlotsParent.transform) {
                GameObject.Destroy(child.gameObject);
            }//for clear
            Debug.Log("craftUIreq:removed children"); */

            //2. For each requirement, add to the panel
            //for -there is only one for now
            String materialName = mt.formulaForCrafting.Substring(1); //skip the amount
            GameObject foundMaterial = null;

            //find the material
            //Assert.IsNotNull(Camera.main.GetComponent<InventoryAP>());  //it should never be
            //Assert.IsNotNull(Camera.main.GetComponent<InventoryAP>().MaterialSprites);  //it should never be

            //I am a component of a child of CameraController
            Assert.IsNotNull(Camera.main.GetComponent<CameraController>());
            InventoryAP iap = Camera.main.GetComponent<CameraController>().getInventoryAp();
            Assert.IsNotNull(iap);

            // Before accessing MaterialSprites: Claude debug
            Debug.Log($"Accessing MaterialSprites on {gameObject.name}");
            Debug.Log($"MaterialSprites null check: {iap.MaterialSprites == null}");
            Debug.Log($"This object null check: {this == null}");
            Debug.Log($"GameObject null check: {gameObject == null}");

            // Then try to access it in a try-catch:
            try {
                GameObject cmgcims = iap.MaterialSprites[0];
                Debug.Log("Trying to match mat=" + cmgcims.name + " to " + materialName);
            } catch (MissingReferenceException e) {
                Debug.LogError($"MissingReferenceException caught: {e.Message}");
                Debug.LogError($"Stack trace: {e.StackTrace}");
            }
            // Claude debug */


            for (int i2 = 0; i2 < iap.MaterialSprites.Count; i2++) {

                Debug.Log($"MaterialSprites[{i2}] null check: {iap.MaterialSprites[i2] == null}");

                // Try to access just the reference without assigning
                var testRef = iap.MaterialSprites[i2];
                Debug.Log($"Retrieved reference, is null: {testRef == null}");

                // Now try to access the name
                if (testRef != null) {
                    Debug.Log($"About to access .name property");
                    string testName = testRef.name;
                    Debug.Log($"Name accessed successfully: {testName}");
                }

                if (iap.MaterialSprites[i2] != null) {
                    // This might throw the exception even though the null check passed
                    bool isDestroyed = iap.MaterialSprites[i2] == null; // Unity's overloaded null check
                    Debug.Log($"Unity null check (destroyed): {isDestroyed}");
                }
                GameObject cmgcims = iap.MaterialSprites[i2];
                Debug.Log("Trying to match mat=" + cmgcims.name + " to " + materialName);
                if (cmgcims.name.Equals(materialName)) 
                    foundMaterial = cmgcims;
            }//for

            Assert.IsNotNull(foundMaterial);
            Debug.Log("I have the material");

            iap.addRequired(foundMaterial); //attempt number 2

            /*
             * //and add it to the display
            GameObject newSlot = Instantiate(inventorySlotPrefab, requirementsSlotsParent.transform);
            //DEBUG
            Debug.Log("Grid cell size: " + requirementsSlotsParent.GetComponent<GridLayoutGroup>().cellSize);

            Transform icontf = newSlot.transform.GetChild(0); //clumsy... //TODO
            //DEBUG
            Debug.Log("Icon RectTransform size: " + icontf.GetComponent<RectTransform>().sizeDelta);

            if (icontf != null) {
                Image iconImage = icontf.gameObject.GetComponent<Image>();
                if (iconImage != null) {
                    iconImage.sprite = foundMaterial.GetComponent<SpriteRenderer>().sprite;
                    Debug.Log("craft:display changed " + iconImage.sprite.name);
                }//if iconImage OK
            }//if icon OK
            */

            //Now, we must update the display with the (List of) material(s)
        }
        //3. Add sprits to the required display (clear it first.)
    }//F

    /* private GameObject findCraftableBySpriteOld(Sprite s) {
        //throw new NotImplementedException();
        foreach( GameObject go in AllCraftables) {
            //MyTile mt = go.GetComponent<MyTile>();
            //Assert.IsNotNull(mt);
            SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
            Assert.IsNotNull(sr);
            if (sr.sprite == s) return go;
        }

        return null;
    }//F */
    private CraftableData findCraftableBySprite(Sprite s) {
        foreach (CraftableData data in AllCraftables) {
            if (data.sprite == s) {
                return data; //now it should be a prefab
            }
        }
        return null;
    }

    public void pickCraftingPlace() { //from button
        Debug.Log("I picked a place");
        //first click lights up the crosses
        crossesDisplay.GetComponent<CraftingCrosses>().crossesOn(true);   //lights up the crosses
        //second click is from crosses, and it places the block
    }
    internal void placeBlock(Vector3 position) {
        //throw new NotImplementedException();//continuity!
        //we have the position
        //Assert.IsNotNull(chosenRecipe);

        GameObject go = Instantiate(chosenRecipe.prefab, position, Quaternion.identity);//chosen recipe must be a prefab (CS03111)
        //OLD GameObject go = Instantiate(craftingCrossSprite, new Vector3(i, j, 0), Quaternion.identity);
        GameObject tm = Camera.main.GetComponent<CameraController>().getTileMap();
        Assert.IsNotNull(tm);
        //go.transform.SetParent( tm.transform, worldPositionStays: false); 
    }
    private void LoadAllCraftables() {
        AllCraftables.Clear();

        // Load all prefabs from Resources folder with tag "craftable"
        GameObject[] prefabs = Resources.LoadAll<GameObject>("craftables");

        foreach (GameObject prefab in prefabs) {
            if (prefab.CompareTag("craftable")) {
                CraftableData data = new CraftableData();
                data.prefab = prefab;

                // Get the sprite from the prefab
                SpriteRenderer sr = prefab.GetComponentInChildren<SpriteRenderer>();
                if (sr != null) {
                    data.sprite = sr.sprite;
                    AllCraftables.Add(data);    //populates AllCraftables
                }
            }
        }//foreach - thanks Claude

        Debug.Log($"Loaded {AllCraftables.Count} craftable prefabs");
    }

}//class
