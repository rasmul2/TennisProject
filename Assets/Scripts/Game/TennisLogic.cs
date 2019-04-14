using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisLogic : MonoBehaviour
{
    public GameObject[] players;
    public GameObject court;

    public int[] score;

    private bool[] side;
    public GameObject ball;
    public bool isMoving = false;

    public GameObject gameManager;
    private Vector3 startPosition;
    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        score = new int[2] { 0, 0 };
        
        players = new GameObject[2];

        
    }

    public void ReadyToStart()
    {
        side = new bool[2] { true, false };

        side[0] = false;
        side[1] = true;
        ball.transform.position = players[0].transform.position + new Vector3(0, -3f, 3f);

    }

    public void BeginMovingBall()
    {
        ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        isMoving = true;
        startPosition = ball.transform.position;
        startTime = Time.deltaTime;
    }
    private void freezeBall()
    {
        ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(players[0] != null) {
            //if ball is greater than the x or z max/ or ball is less than the x or z minus of the court then whoever's side it isn't gets the point
            if (ball.transform.position.x > court.GetComponent<BoxCollider>().bounds.max.x ||
                ball.transform.position.z > court.GetComponent<BoxCollider>().bounds.max.z ||
                ball.transform.position.x < court.GetComponent<BoxCollider>().bounds.min.x ||
                ball.transform.position.z < court.GetComponent<BoxCollider>().bounds.min.z)
            {
                if (side[0] == true)
                {
                    score[0]++;
                    side[0] = false;
                    side[1] = true;

                    //move ball position in front of player who lost point
                    ball.transform.position = players[1].transform.position + new Vector3(0, -3f, -3f);
                    freezeBall();
                }
                else
                {
                    score[1]++;
                    side[0] = false;
                    side[1] = true;

                    ball.transform.position = players[0].transform.position + new Vector3(0, -3f, 3f);
                    freezeBall();
                }
            }
            else if (isMoving)
            {
                float distCovered = (Time.time - startTime)*2;
                

                if (side[0] == true)
                {
                    float totalDistance = Vector3.Distance(startPosition, new Vector3(0, 0, -50f));
                    ball.transform.position = Vector3.Lerp(startPosition, new Vector3(0, 0, -50f), distCovered / totalDistance);
                    ball.transform.position = Vector3.Lerp(startPosition, players[0].transform.position, 50*Time.deltaTime);
                }
                else
                {
                    float totalDistance = Vector3.Distance(startPosition, new Vector3(0, 0, 50f));
                    ball.transform.position = Vector3.Lerp(startPosition, new Vector3(0, 0, 50f), distCovered/totalDistance);
                }
            }
        }
    }
}
