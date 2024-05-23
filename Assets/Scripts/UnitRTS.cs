using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRTS : MonoBehaviour {

    private GameObject selectedGameObject;
    private IMovePosition movePosition;
    //AP
    public bool selected { get; internal set; } //Hi .. I'm just testing Git - Git why you so mean to me?

    private void Awake() {
        selectedGameObject = transform.Find("selection-box").gameObject;
        movePosition = GetComponent<IMovePosition>();
        SetSelectedVisible(false);
    }

    public void SetSelectedVisible(bool visible) {
        selectedGameObject.SetActive(visible);
    }

    public void MoveTo(Vector3 targetPosition) {
        Debug.Log("MT called");
        movePosition.SetMovePosition(targetPosition);
    }

    void Update()
    {
        if (selectedGameObject != null)
            selectedGameObject.SetActive(selected);
    }//update

}//class
