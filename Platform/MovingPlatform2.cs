using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform2 : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 4f;

    private Vector3 nextPosition;
    private Vector3 initialPosition;

    public float interactionDistance = 2f; // Distance within which you can interact with the lever

    public float moveTimer = 0;
    public Boolean isMoving;
    public Boolean continueMoving; 

    public Vector3 leverNextPosition;
    public Transform lever;
    public Transform lever2;
    public Transform movingLeverPointA;
    public Transform movingLeverPointB;

    [SerializeField] private float x1;
    [SerializeField] private float y1;
    [SerializeField] private float x2;
    [SerializeField] private float y2;


    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        nextPosition = pointB.position;
        Application.targetFrameRate = 60; 

        leverNextPosition = movingLeverPointB.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);
        lever2.transform.position  = Vector3.MoveTowards(lever2.transform.position, leverNextPosition, moveSpeed * Time.deltaTime);

        //if ((Input.GetKeyUp(KeyCode.W) && IsPlayerNearLever()) || (Input.GetKeyUp(KeyCode.UpArrow) && IsPlayerNearLever())
        if ((IsPlayerNearLever() && Input.GetKeyUp(KeyCode.W)) || (IsPlayerNearLever() && Input.GetKeyUp(KeyCode.UpArrow))  )
        {
            continueMoving = false;
            moveTimer = 60;
            nextPosition = transform.position;
            leverNextPosition = lever2.transform.position;
        }

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
               // leverNextPosition = new Vector3(-15,5,0);
                leverNextPosition = new Vector3(x1,y1,0);
            }
        }

        if (moveTimer > 5 && continueMoving == true ){
            nextPosition = pointA.position;
            //leverNextPosition = new Vector3(15,5,0);
            leverNextPosition = new Vector3(x2,y2,0);
        }

        Debug.Log(moveTimer);

        // if(transform.position == pointA.position && continueMoving == true)
        // {
        //     moveTimer = 120;
        //     continueMoving = false;
        // }

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