using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabOMat : MonoBehaviour
{
    CraftingUI cUi;
    // Start is called before the first frame update
    void Start()
    {
        cUi = Camera.main.GetComponent<CraftingUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D hitInfo) {
        //Debug.Log("something touched fab-o-mat");
        if (hitInfo.gameObject.GetComponent<MovePositionDirect>() != null ) { //only the main player mob has this component
            //Debug.Log("Player touched Fab-o-Mat");
            //CraftingUI cUi = Camera.main.GetComponent<CraftingUI>();
            if (cUi != null)
                cUi.displayEkey(true);
        }//if
    }//F

    private void OnTriggerExit2D(Collider2D collision) {
        if (cUi != null)
            cUi.displayEkey(false);
    }//F
}//class
