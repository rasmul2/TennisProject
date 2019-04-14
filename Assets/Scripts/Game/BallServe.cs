using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallServe : MonoBehaviour
{

    public GameObject ball;
    public Transform creation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            transform.position = Camera.main.transform.position + new Vector3(0, 3, 5);
            
//            Instantiate(creation, transform.position + transform.forward * 5, transform.rotation);
            //Instantiate(ball, transform.position + transform.forward * 5, transform.rotation);
        }

    }
}
