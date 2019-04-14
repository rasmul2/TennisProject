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
        Debug.Log("Something collided with me");
        if(collision.collider.name == ball.GetComponent<Collider>().name)
        {
            Debug.Log(collision.collider.name);
            if (!tennisLogic.GetComponent<TennisLogic>().isMoving)
            {
                tennisLogic.GetComponent<TennisLogic>().BeginMovingBall();
                

            }
            Vector3 tempVect = new Vector3(collision.rigidbody.velocity.x * -1, collision.rigidbody.velocity.y * -1, collision.rigidbody.velocity.z*-1);
            ball.GetComponent<Rigidbody>().AddForce(tempVect*500, ForceMode.VelocityChange);
        }
    }
}
