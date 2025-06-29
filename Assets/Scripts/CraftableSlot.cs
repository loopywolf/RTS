using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftableSlot : MonoBehaviour, IPointerUpHandler
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

        //AARRGH
        /*Button btn = GetComponent<Button>();
        if (btn != null) {
            btn.enabled = false;
            Debug.Log("Disabled button component on " + gameObject.name);
        }
        Button button = GetComponent<Button>();
        if (button != null) {
            button.onClick.AddListener(() => {
                Debug.Log("BUTTON CLICKED: " + gameObject.name);
            });
        } 
        // Re-enable the button
        Button btn = GetComponent<Button>();
        if (btn != null) {
            btn.enabled = true;
            btn.onClick.RemoveAllListeners(); // Clear any existing listeners
            btn.onClick.AddListener(OnButtonClick);
        } */

        /* Button btn = GetComponent<Button>();
        if (btn != null) {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(OnButtonClick);
            Debug.Log("Button listener added to " + gameObject.name);
        } */

        //Clicks not being given
        //GetComponent<Button>().enabled = false;
    }//start

    // Update is called once per frame
    void Update()
    {
        /* if (Input.GetMouseButtonDown(0)) {
            Button btn = GetComponent<Button>();
            if (btn != null) {
                Debug.Log($"Button - Enabled: {btn.enabled}, Interactable: {btn.interactable}, Active: {btn.gameObject.activeInHierarchy}");
            }
        } */
    }

    /* void OnMouseDown() { 
        Debug.Log("I love that rat!" + this.name);
        //throw new System.NotImplementedException();
    }

    public void ClickedByGod() {
        Debug.Log("Quarantine");
    }

    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log("*** OnPointerClick: " + gameObject.name);
    }

    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("*** OnPointerDown: " + gameObject.name);
    } */

    public void OnPointerUp(PointerEventData eventData) {
        Debug.Log("*** OnPointerUp: " + gameObject.name);
    }

    /* public void OnPointerEnter(PointerEventData eventData) {
        Debug.Log("*** OnPointerEnter: " + gameObject.name);
    }

    public void OnPointerExit(PointerEventData eventData) {
        Debug.Log("*** OnPointerExit: " + gameObject.name);
    }

    public void OnButtonClick() {
        Debug.Log("*** BUTTON CLICK: " + gameObject.name);
    } */

}//class
