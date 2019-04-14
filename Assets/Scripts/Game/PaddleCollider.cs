using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleCollider : MonoBehaviour
{

    public bool swung = false;
    public float cooldown = 50;
    public float swingTime = 10;

    public float swungtime = 0;

    public GameObject text;
    public int score;

    private float timeToDist = 5;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = gameObject.transform.position;
    }

    public void tryToSwing()
    {
        if(cooldown == 0)
        {
            swung = true;
            cooldown = 50;
            swungtime = Time.time;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(timeToDist == 0)
        {
            Debug.Log("That arbitrary distance measure" + Vector3.Distance(startPos, gameObject.transform.position));
            if(Vector3.Distance(startPos, gameObject.transform.position) > 0.5f)
            {
                tryToSwing();
            }

            timeToDist = 50;
            startPos = gameObject.transform.position;
        }
        if(swung == true)
        {
            swingTime--;
        }

        if(swingTime == 0)
        {
            swung = false;
            swingTime = 10;
        }

        if(cooldown > 0)
        {
            cooldown--;
        }
    }

}
