using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisLogic : MonoBehaviour
{
    public GameObject[] players;
    public GameObject court;

    public int[] score;

    private bool[] side;
    public GameObject ballPrefab;
    public bool isMoving;

    private GameObject ball;
    public GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        score = new int[2] { 0, 0 };
        side = new bool[2] { true, false };
        ball = PhotonNetwork.Instantiate(this.ballPrefab.name, players[0].transform.position += new Vector3(0, 0, 3), Quaternion.identity, 0);

        side[0] = false;
        side[1] = true;

        players = new GameObject[2];


    }

    public void BeginMovingBall()
    {
        ball.AddComponent<Rigidbody>();
        isMoving = true;
    }

    private void freezeBall()
    {
        Destroy(ball.GetComponent<Rigidbody>());
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(players[0] != null || players[1] != null) {
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
                    ball.transform.position = players[1].transform.position + new Vector3(0, 0, -3f);
                    freezeBall();
                }
                else
                {
                    score[1]++;
                    side[0] = false;
                    side[1] = true;

                    ball.transform.position = players[0].transform.position + new Vector3(0, 0, 3f);
                    freezeBall();
                }
            }
        }
    }
}
