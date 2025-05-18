using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTile : MonoBehaviour
{
    public int materialGiven = -1;
    static GameObject gatherEffect;
    public bool hasItem = true;
    //public GameObject materialCollectible;  //not good enough

    // Start is called before the first frame update
    void Start()
    {
        gatherEffect = Camera.main.GetComponent<CameraController>().gatherEffect; //has to work
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
        Debug.Log("Tile's drop mat is " + this.materialGiven);
        if (this.materialGiven >= 0) { //-1 means no drop
            GameObject dropMat = Camera.main.GetComponent<CameraController>().getDropMaterial(materialGiven);
            if(dropMat!=null)
                Instantiate(dropMat, transform.position, Quaternion.identity); //fingers crossed
        }//if
        //3. destroy this tile
        Destroy(gameObject);
    }//F

}//class
