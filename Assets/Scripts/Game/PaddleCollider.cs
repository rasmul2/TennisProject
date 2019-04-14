using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleCollider : MonoBehaviour
{
    public GameObject ball;
    public GameObject tennisLogic;
    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindGameObjectsWithTag("ball")[0];
        tennisLogic = GameObject.FindGameObjectsWithTag("Tennis Logic")[0];
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.name == ball.GetComponent<Collider>().name)
        {
            if (!tennisLogic.GetComponent<TennisLogic>().isMoving)
            {
                tennisLogic.GetComponent<TennisLogic>().BeginMovingBall();

            }
        }
    }
}
