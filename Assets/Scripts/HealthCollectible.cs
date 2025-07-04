using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    //if the collectible is a material, this is the material number
    public int material = -1;  //if not set by something, this is -1, 0 and up is a material

    void OnTriggerEnter2D(Collider2D other)
    {
        //Do something to gain this object
        PlayerController pc = other.gameObject.GetComponent<PlayerController>();

        if (pc != null)
        {
            pc.addItem(this.name); //old inventory code
            //if(this.material>=0)
            Camera.main.GetComponent<InventoryAP>().addMat(this.gameObject); //this needs fixed //this is where this belongs
            Destroy(gameObject);
        }//if
        //Debug.Log("Object that touched box " + other);//Excellent
    }//onTriggerEnter2D

}//class
