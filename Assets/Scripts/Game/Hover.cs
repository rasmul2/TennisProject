using UnityEngine;
using System.Collections;

public class Hover : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.up * Mathf.Cos(Time.time);
    }
}
