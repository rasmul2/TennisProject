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

    // Start is called before the first frame update
    void Start()
    {

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
