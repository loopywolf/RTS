using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public bool isMoving;
    private Vector2 input;
    public LayerMask wallsLayer;
    public List<string> inventory = new List<string>(); //old inventory - to remove?
    public int health = 100;
    public GameObject powEffect;
    public EnemyAP myEnemyMySelf;
    private Vector3 destination;
    private bool headedForDestination;
    //trying to make inventory
    //private Inventory inventory2;// = new Inventory();
    //[SerializeField] private UI_Inventory uiInventory;

    // Start is called before the first frame update
    void Start()
    {
        powEffect = Camera.main.GetComponent<CameraController>().powEffect; //TODO not efficient, you do the same in Bullet
        myEnemyMySelf = transform.GetComponent<EnemyAP>();
        headedForDestination = false;
    }//Start

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //pathing - if this works it's been done the correct way
            if (headedForDestination && input == Vector2.zero)
            {
                Debug.Log("Pathing " + transform.position + " to " + destination);
                if (destination == transform.position)
                {
                    headedForDestination = false;   //stop moving
                } else {
                    if (destination.x < transform.position.x)
                        input.x = -1.0f;
                    else
                    if (destination.x > transform.position.x)
                        input.x = 1.0f;

                    if (destination.y < transform.position.y)
                        input.y = -1.0f;
                    else
                    if (destination.y > transform.position.y)
                        input.y = 1.0f;
                }//if destination = position
            }//if headedForDestination

            if (input != Vector2.zero)
            {
                //Debug.Log("x=" + input.x + " y=" + input.y);
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if( isWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
            }

        }//if !isMoving

        /* if(headedForDestination)
        {
            //the player must move towards its destination
            //if (isWalkable(destination))
            StartCoroutine(Move(destination)); //note: this is too fast, but it works!
        }//if */
    }//Update

    IEnumerator Move(Vector3 targetPos) //is it OK this is not FixedUpdate
    {
        isMoving = true;

        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }//F.cr

    private bool isWalkable(Vector3 targetPos)
    {
        if( Physics2D.OverlapCircle(targetPos, 0.2f, wallsLayer) != null )
        {
            return false;
        }
        return true;
    }//F

    internal void addItem(string v)
    {
        inventory.Add(v);
        //throw new NotImplementedException();
    }//F

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log("hitme " + hitInfo.name);

        //instantiate POW! - how??

        EnemyAP e = hitInfo.GetComponent<EnemyAP>();
        if (e != null) //it is an enemy
        {
            Vector2 midwayPoint = new Vector2(
                (transform.position.x + e.transform.position.x)/2.0f,
                (transform.position.y + e.transform.position.y)/2.0f);
            Instantiate(powEffect, midwayPoint, Quaternion.identity);
            if(myEnemyMySelf!=null)
                myEnemyMySelf.TakeDamage(e.h2hDamage);  //enemy needs to become a more generic "mob" class
            Debug.Log("Hit! " + e.h2hDamage + " health=" + health);
        }//if
        //Destroy(gameObject); */
    }//OnTriggerEnter2D

    internal void SetDestination(Vector3 mousePos)
    {
        destination = new Vector3( Mathf.Round(mousePos.x), Mathf.Round(mousePos.y), 0f);
        headedForDestination = true;
        //sets the player mob's destination
        Debug.Log(name + " headed for destination " + destination);
    }

    private void Awake()
    {
        //inventory2 = new Inventory();
        //uiInventory.SetInventory(inventory2);

    }//F

}//Class
