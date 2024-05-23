using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class DragSelection : MonoBehaviour
{
    private bool isDragSelect = false;
    private Vector3 mousePositionInitial;
    private Vector3 mousePositionEnd;
    public RectTransform selectionBox;
    //public List<UnitRTS> mobsSelected;
    public List<UnitRTS> selectedUnitRTSList;

    private void Awake()
    {
        selectedUnitRTSList = new List<UnitRTS>();
    }//Start

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            mousePositionInitial = Input.mousePosition;
            isDragSelect = false;
        }//if

        if(Input.GetMouseButton(0))
        {
            if(!isDragSelect && (mousePositionInitial - Input.mousePosition).magnitude > 30)
            {
                isDragSelect = true;
            }//if

            if(isDragSelect)
            {
                mousePositionEnd = Input.mousePosition;
                UpdateSelectionBox();
            }//if
        }//LMB 1

        if(Input.GetMouseButtonUp(0))
        {
            if (isDragSelect)
            {
                isDragSelect = false;
                UpdateSelectionBox();
                SelectUnits();
                /* //the fancier way - still not working
                Collider2D[] collider2Darray = Physics2D.OverlapAreaAll(mousePositionInitial, UtilsClass.GetMouseWorldPosition());
                selectedUnitRTSList.Clear();
                foreach(Collider2D c2d in collider2Darray)
                {
                    Debug.Log(c2d);
                    UnitRTS urts = c2d.GetComponent<UnitRTS>();
                    if(urts!=null)
                    {
                        selectedUnitRTSList.Add(urts);
                    }
                    Debug.Log("rtslist " + selectedUnitRTSList.Count);
                } */
            }//if
        }//if LMB 0

        //RMB 1 to move .. should this be here?
        if (Input.GetMouseButtonUp(1))
        {
            Vector3 movePosition = UtilsClass.GetMouseWorldPosition();  //swell =( 12:21
            Debug.Log("Moveposition=" + movePosition);

            for(int i=0; i<selectedUnitRTSList.Count; i++)
            {
                UnitRTS urts = selectedUnitRTSList[i];
                urts.MoveTo(movePosition);
            }
        }//RMB
    }//Update

    void UpdateSelectionBox()
    {
        //throw new NotImplementedException();
        selectionBox.gameObject.SetActive(isDragSelect);

        float width = mousePositionEnd.x - mousePositionInitial.x;
        float height = mousePositionEnd.y - mousePositionInitial.y;

        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.anchoredPosition = new Vector2(mousePositionInitial.x, mousePositionInitial.y) + new Vector2(width / 2, height / 2);
    }//F

    void SelectUnits()
    {
        //throw new NotImplementedException();
        Vector2 minValue = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 maxValue = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2); //sophisticated

        /* can't get this to work
         * this works and it's a neat and simple way- probably fast too 
        Debug.Log("overlap area all");

        Collider2D[] allUnits = Physics2D.OverlapAreaAll(minValue, maxValue);
        Debug.Log(allUnits.Length + " collider2Ds found");
        foreach(Collider2D c2d in allUnits)
        {
            BoxCollider2D bc2d = (BoxCollider2D)c2d;
            Debug.Log(bc2d);
        }
        // CAN replace this with https://www.youtube.com/watch?v=mCIkCXz9mxI&t=1s at 5:54 about?

        Debug.Log("Fancy collider detect end"); */

        GameObject[] selectedUnits = GameObject.FindGameObjectsWithTag("selectable");
        selectedUnitRTSList.Clear();

        foreach( GameObject su in selectedUnits)
        {
            UnitRTS urts = su.GetComponent<UnitRTS>();
            if (urts != null)
                urts.selected = false;

            Vector3 screenPosition = Camera.main.WorldToScreenPoint(su.transform.position);
            if( screenPosition.x > minValue.x && screenPosition.x < maxValue.x &&
                screenPosition.y > minValue.y && screenPosition.y < maxValue.y)
            {
                Debug.Log("XSelected " + su.name);
                //su.GetComponent<Renderer>().material.color = Color.red;
                if (urts != null)
                {
                    urts.selected = true;
                    selectedUnitRTSList.Add(urts);
                }
            }//if
        }//for
    }//F

}//class
