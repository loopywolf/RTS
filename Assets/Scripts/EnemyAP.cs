using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAP : MonoBehaviour
{
    public int health = 100;
    public GameObject deathEffect;
    public int h2hDamage = 1;
    private GameObject selectionBox;
    public bool selected = false;
    //RTS tutorial
    IMovePosition movePosition;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health<=0)
        {
            Die();
        }
    }//F
    
    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }//F
    
    // Start is called before the first frame update
    void Start() {
        selectionBox = transform.Find("selection-box").transform.gameObject; // bug is for those enemies that don't have one
        //selectionBox.SetActive(selected);
        Debug.Log("I turned selection box of " + this.name + " to " + selected);
        movePosition = GetComponent<IMovePosition>(); //RTS tutorial
    }//start

    // Update is called once per frame
    void Update() {
        if(selectionBox!=null)
            selectionBox.SetActive(selected);
    }//update

    public void MoveTo(Vector3 targetPosition)
    {
        Debug.Log("movePosition=" + movePosition);
        movePosition.SetMovePosition(targetPosition);
    }//F
}//class
