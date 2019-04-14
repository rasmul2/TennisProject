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

    private GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        score = new int[2] { 0, 0 };
        side = new bool[2] { true, false };
        ball = PhotonNetwork.Instantiate(this.ballPrefab.name, players[0].transform.position += new Vector3(0, 0, 3), Quaternion.identity, 0);

        side[0] = false;
        side[1] = true;


    }

    // Update is called once per frame
    void Update()
    {
        //if ball is greater than the x or z max/ or ball is less than the x or z minus of the court then whoever's side it isn't gets the point
        if(ball.transform.position.x > court.GetComponent<BoxCollider>().bounds.max.x ||
            ball.transform.position.z > court.GetComponent<BoxCollider>().bounds.max.z ||
            ball.transform.position.x < court.GetComponent<BoxCollider>().)
    }
}
