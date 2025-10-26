using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private bool isMoving;
    private Vector2 input;
    public LayerMask wallsLayer;
    public List<string> inventory = new List<string>();
    public int health = 100;
    public GameObject powEffect;
    public EnemyAP myEnemyMySelf;
    private Vector3 destination;
    private bool headedForDestination;

    private Rigidbody2D rb;
    private Vector3 targetPosition;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        powEffect = Camera.main.GetComponent<CameraController>().powEffect;
        myEnemyMySelf = transform.GetComponent<EnemyAP>();
        headedForDestination = false;

        // Snap to grid at start
        Vector3 pos = transform.position;
        transform.position = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), pos.z);
    }

    void Update() {
        // Only handle input in Update, not movement
        if (!isMoving) {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            // Pathing logic
            if (headedForDestination && input == Vector2.zero) {
                if (Vector3.Distance(transform.position, destination) < 0.1f) {
                    headedForDestination = false;
                } else {
                    // Calculate direction to destination
                    if (destination.x < transform.position.x)
                        input.x = -1.0f;
                    else if (destination.x > transform.position.x)
                        input.x = 1.0f;
                    else
                        input.x = 0f;

                    if (destination.y < transform.position.y)
                        input.y = -1.0f;
                    else if (destination.y > transform.position.y)
                        input.y = 1.0f;
                    else
                        input.y = 0f;
                }
            }

            // Try to start moving
            if (input != Vector2.zero) {
                /* prevents diagonal // Only move in one direction at a time (no diagonals)
                if (Mathf.Abs(input.x) > 0)
                    input.y = 0;
                else if (Mathf.Abs(input.y) > 0)  // Added else here!
                    input.x = 0; */

                Vector3 intendedTarget = transform.position;
                intendedTarget.x += input.x;  // Don't use Mathf.Sign here
                intendedTarget.y += input.y;  // Don't use Mathf.Sign here

                // Round to whole numbers
                intendedTarget.x = Mathf.Round(intendedTarget.x);
                intendedTarget.y = Mathf.Round(intendedTarget.y);

                if (isWalkable(intendedTarget)) {
                    targetPosition = intendedTarget;
                    isMoving = true;
                } else {
                    Debug.Log("Path blocked!");
                }//if
            }//if not zero
        }//endif not moving

    }//F Update

    void FixedUpdate() {
        if (isMoving) {
            MoveTileToTile();
        }
    }

    void MoveTileToTile() {
        Vector2 currentPos = rb.position;
        Vector2 newPos = Vector2.MoveTowards(currentPos, targetPosition, moveSpeed * Time.fixedDeltaTime);

        rb.MovePosition(newPos);

        // Check if we've reached the target
        if (Vector2.Distance(newPos, targetPosition) < 0.01f) {
            // Snap to exact position (whole integers)
            rb.MovePosition(new Vector2(
                Mathf.Round(targetPosition.x),
                Mathf.Round(targetPosition.y)
            ));
            isMoving = false;
        }
    }

    private bool isWalkable(Vector3 targetPos) {
        Collider2D hit = Physics2D.OverlapCircle(targetPos, 0.2f, wallsLayer);

        //Debug.Log($"Checking position {targetPos}, hit: {(hit != null ? hit.name : "nothing")}, wallsLayer: {wallsLayer.value}");

        if (hit != null && hit.gameObject != gameObject) {
            return false;
        }
        return true;
    }

    internal void addItem(string v) {
        inventory.Add(v);
    }

    void OnTriggerEnter2D(Collider2D hitInfo) {
        Debug.Log("hitme " + hitInfo.name);

        EnemyAP e = hitInfo.GetComponent<EnemyAP>();
        if (e != null) {
            Vector2 midwayPoint = new Vector2(
                (transform.position.x + e.transform.position.x) / 2.0f,
                (transform.position.y + e.transform.position.y) / 2.0f);
            Instantiate(powEffect, midwayPoint, Quaternion.identity);
            if (myEnemyMySelf != null)
                myEnemyMySelf.TakeDamage(e.h2hDamage);
            Debug.Log("Hit! " + e.h2hDamage + " health=" + health);
        }
    }

    internal void SetDestination(Vector3 mousePos) {
        destination = new Vector3(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y), 0f);
        headedForDestination = true;
        Debug.Log(name + " headed for destination " + destination);
    }

    private void Awake() {
        // inventory setup if needed
    }

}//Class
