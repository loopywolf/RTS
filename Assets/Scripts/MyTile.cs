using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MyTile : MonoBehaviour
{
    //Tiles understand how they are made, and what they drop

    static GameObject gatherEffect;
    public bool hasItem = true;
    //public GameObject materialCollectible;  //not good enough
    public string formulaForCrafting;
    public string materialsDropped; // this is where the formula/requirements are stored

    // Start is called before the first frame update
    void Start()
    {
        gatherEffect = Camera.main.GetComponent<CameraController>().gatherEffect; //has to work //this is dumb, it should be set here, but w/e

        //now we must figure out what mats are gathered and dropped for each
    }//start

    // Update is called once per frame
    void Update()
    {
        
    }//update

    private void OnMouseUp()
    {
        //Debug.Log("clicked on a mytile! "+this.name+" material:"+this.materialGiven); //not sure I need this one
        //the player shoots at this tile with a "gatherer" bolt (to be replaced)
        
    }//F

    public void gather()
    {
        //gathers this tile to the player's inventory
        //1. spawn animation on the tile
        Instantiate(gatherEffect, transform.position, Quaternion.identity); //need to replace this with a different animation DONE
        //2.OLD add tile to player's inventory
        Debug.Log("attempting to add item from " + gameObject.name);
        Camera.main.GetComponent<CameraController>().addToInventory(gameObject);
        //2.NEW create a new item at this location, to be picked up.
        Debug.Log("Tile's drop mat is " + this.materialsDropped);
        //if (this.materialsDropped.Length >= 0) { //-1 means no drop
            //GameObject dropMat = Camera.main.GetComponent<CameraController>().getDropMaterial(materialGiven); //TODO
            //GameObject dropMat = 
            dropPrefabMaterialGiven();//combines the two statements that were here
            //if(dropMat!=null)
                //Instantiate(dropMat, transform.position, Quaternion.identity); //fingers crossed
        //}//if
        //3. destroy this tile
        Destroy(gameObject);
    }//F

    private void dropPrefabMaterialGiven() {
        if (materialsDropped == null || materialsDropped.Length == 0) return;   //can't do this
        //throw new NotImplementedException();

        //1. find the prefab(s) and instantiate their collectible(s) - to start 1 only
        //to start, assuming it is one string of format nmmmmm - number, material name
        //to start, assume n is 1
        String materialName = this.materialsDropped.Substring(1).Trim();   //starting
        //Debug.Log("looking for material called " + materialName);

        //now, search all materials and determine which one matches
        //GameObject materialPrefab = null;
        GameObject[] AllMats = GameObject.FindGameObjectsWithTag("mat");
        Assert.IsNotNull(AllMats); //this cannot happen
        Assert.AreNotEqual(AllMats.Length, 0);

        for(int i=0;i<AllMats.Length;i++) {
            //Debug.Log("checking for match, mat " + i + " name=" + AllMats[i].name + " to match " + materialName);
            if (AllMats[i].name.Equals(materialName)) {
                //this is the dropped mat
                GameObject newCollectible = Instantiate(AllMats[i], this.transform.position, Quaternion.identity);
                //Debug.Log("Gathering " + this.name + " dropped " + AllMats[i].name);
                break;
            }//if
        }//for

    }//F

}//class
