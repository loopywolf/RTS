using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CraftingCrosses : MonoBehaviour
{
    public GameObject craftingCrossSprite; //used to populate the matrix;
    public static int span = 5;

    // Start is called before the first frame update
    void Start()
    {
        //populate the matrix
        for(int i=-span;i<=span;i++) {
            for(int j=-span;j<=span;j++) {
                if (i == 0 && j == 0) continue; //don't put a cross over the fab-o-mat
                //TODO: What about collision

                GameObject go = Instantiate(craftingCrossSprite, new Vector3(i, j, 0), Quaternion.identity);
                go.transform.SetParent(this.transform, worldPositionStays:false);
            }//j
        }//i

        gameObject.SetActive(false);  //turns off the crosses
    }//F

    internal void craftHere(Vector3 position) {
        //throw new NotImplementedException();
        Debug.Log("Placing at " + position.x + " " + position.y);
        crossesOn(false);
        //Next steps
        //1. place a block at that location
        Assert.IsNotNull(Camera.main.GetComponent<CraftingUI>());
        Camera.main.GetComponent<CraftingUI>().placeBlock( position ); //eg. -2 1
        //2. identify which block was to be placed
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void crossesOn(bool v) {
        Debug.Log("CrossesOn " + v);
        //throw new NotImplementedException();
        gameObject.SetActive(v);
    }
}//class
