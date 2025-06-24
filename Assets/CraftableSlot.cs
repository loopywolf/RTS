using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftableSlot : MonoBehaviour
{
    Outline selectionOutline;
    // Start is called before the first frame update
    void Start()
    {
        selectionOutline = transform.GetComponent<Outline>();//.gameObject;
        //Debug.Log("start: o=" + o);
        Assert.IsNotNull(selectionOutline);
        //o.SetActive(false); //by default, turned off
        //o.enabled = false; //turning off for now
        selectionOutline.effectColor = new Color(selectionOutline.effectColor.r, selectionOutline.effectColor.g, selectionOutline.effectColor.b, 0f); //cmon WORK! IT WORKED!

        //Canvas.ForceUpdateCanvases(); did not work
        /*gameObject.SetActive(false);
        gameObject.SetActive(true); did not work */

        /* Outline[] outlines = GetComponents<Outline>();
        foreach (Outline outline in outlines) {
            outline.enabled = false;
        } did not work */
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PointerClick() {
        Debug.Log("I love that rat!"+this.name);
        //selectionOutline.effectColor = new Color(selectionOutline.effectColor.r, selectionOutline.effectColor.g, selectionOutline.effectColor.b, 1.0f);
    }//F

}//class
