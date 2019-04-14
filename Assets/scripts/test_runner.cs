using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_runner : MonoBehaviour
{
    public Score score;
    private int winning_score = 5;

    // Start is called before the first frame update
    void Start()
    {
        score = new Score();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) {
            score.increment_player_score("player1");
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            score.increment_player_score("player2");
        }

        if (score.get_player_score("player1") >= winning_score || score.get_player_score("player2") >= winning_score) {
            Debug.Log("baba is winner");
            score.reset_score();
        }
    }
}
