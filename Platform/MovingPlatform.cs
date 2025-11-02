using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 4f;

    private Vector3 nextPosition;
    private Vector3 initialPosition;
    public Transform lever;
    public float interactionDistance = 1f; // Distance within which you can interact with the lever
    public float moveTimer = 0;
    public Boolean isMoving;
    public Boolean continueMoving; 


    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        nextPosition = pointB.position;
        Application.targetFrameRate = 60; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);

        if ((IsPlayerNearLever() && Input.GetKey(KeyCode.W)) || (IsPlayerNearLever() && Input.GetKey(KeyCode.UpArrow))  )
        {
          //  nextPosition = pointA.position;
            continueMoving = true;
           if(moveTimer < 6)
                moveTimer++;
        }
        else{
            if (moveTimer >= -1)
                moveTimer--;
            if (moveTimer == -2)
            {
                nextPosition = pointB.position;
            }
        }

        if (moveTimer > 5){
            nextPosition = pointA.position;
        }

        if(transform.position == pointA.position && continueMoving == true)
        {
            moveTimer = 120;
            continueMoving = false;
        }

    }

     bool IsPlayerNearLever()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Assuming your player has the tag "Player"
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, lever.position);
            return distance <= interactionDistance;
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.transform.parent = transform;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.gameObject.transform.parent = null;
    }
}
