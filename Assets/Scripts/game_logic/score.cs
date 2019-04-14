using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    int player_1_score;
    int player_2_score;
    

    // Start is called before the first frame update
    void Start() {
        player_1_score = 0;
        player_2_score = 0;
    }

    // Update is called once per frame
    void Update() {
        
    }

    public int get_player_score(string player_name) {
        if (player_name.Equals("player1")) {
            return player_1_score;
        } else {
            return player_2_score;
        }
    }

    public void increment_player_score(string player_name) {
        Debug.Log("Score is updated");
        if (player_name.Equals("player1")) {
            player_1_score++;
        } else {
            player_2_score++;
        }
    }

    public void reset_score() {
        player_1_score = 0;
        player_2_score = 0;
    }
}
