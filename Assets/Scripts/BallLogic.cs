using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLogic : MonoBehaviour
{
    private bool movingtowards2 = true;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(index == 1000) {
            index = 0;
            gameObject.transform.position = new Vector3(0, 0.5f, -25f);
        }
        index++;
        if(GameObject.Find("Player1(Clone)") != null && GameObject.Find("Player2(Clone)") != null)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(0, 0.5f, 25f), Time.deltaTime);
            //Debug.Log("Instantiated player count" + instantiatedplayers.Count);
            /*
            if (movingtowards2 == false)
            {

                float distcov = (Time.time - 10) * 0.5f;
                gameObject.transform.position = Vector3.Lerp(GameObject.Find("Player1(Clone)").transform.position, GameObject.Find("Player2(Clone)").transform.position, 1- Vector3.Distance(gameObject.transform.position, GameObject.Find("Player2(Clone)").transform.position) / Vector3.Distance(GameObject.Find("Player1(Clone)").transform.position, GameObject.Find("Player2(Clone)").transform.position));

                if (Vector3.Distance(gameObject.transform.position, GameObject.Find("Player2(Clone)").transform.position) < 1)
                {
                    if (GameObject.Find("Player1(Clone)").GetComponentInChildren<PaddleCollider>().swung == false)
                    {
                        int score = int.Parse(GameObject.Find("Player1(Clone)").GetComponentInChildren<PaddleCollider>().GetComponentInParent<TextMesh>().text);
                        score++;
                        GameObject.Find("Player1(Clone)").GetComponentInChildren<PaddleCollider>().GetComponentInParent<TextMesh>().text = score.ToString();
                    }

                    movingtowards2 = true;

                }

            }
            else
            {
                if (movingtowards2 == true)
                {
                    float distcov = (Time.time - 10) * 0.5f;
                    gameObject.transform.position = Vector3.Lerp(GameObject.Find("Player2(Clone)").transform.position, GameObject.Find("Player1(Clone)").transform.position, 1 - Vector3.Distance(gameObject.transform.position, GameObject.Find("Player1(Clone)").transform.position) / Vector3.Distance(GameObject.Find("Player1(Clone)").transform.position, GameObject.Find("Player2(Clone)").transform.position));

                    if (Vector3.Distance(gameObject.transform.position, GameObject.Find("Player1(Clone)").transform.position) < 1)
                    {
                        if (GameObject.Find("Player2(Clone)").GetComponentInChildren<PaddleCollider>().swung == false)
                        {
                            int score = int.Parse(GameObject.Find("Player2(Clone)").GetComponentInChildren<PaddleCollider>().GetComponentInParent<TextMesh>().text);
                            score++;
                            GameObject.Find("Player2(Clone)").GetComponentInChildren<PaddleCollider>().GetComponentInParent<TextMesh>().text = score.ToString();

                        }
                        movingtowards2 = false;
                        Debug.Log("Should move the other direction");

                    }
                }

            }
            */
        }

    }
}

