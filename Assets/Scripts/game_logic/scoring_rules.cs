using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoring_rules : MonoBehaviour
{
    public GameObject attacker;

    public bool player_1_area_collided;
    public bool player_2_area_collided;
    public GameObject out_of_bounds_area;

    public Score score;

    private int ball_bounce_count;
    private string player_1_name = "player1";
    private string player_2_name = "player2";
    
    // Start is called before the first frame update
    void Start()
    {
        ball_bounce_count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void player_strikes_ball(GameObject player) {
        attacker = player;
        ball_bounce_count = 0;
    }

    // ball hits player area
    public void ball_collided_player_area() {
        bool ball_hit_defender_side = false;

        if (player_1_area_collided && attacker.name.Equals(player_2_name)) {
            ball_hit_defender_side = true;
        }
        else if (player_2_area_collided && attacker.name.Equals(player_1_name)) {
            ball_hit_defender_side = true;
        }
            
        //area is not attacker's
        if (ball_hit_defender_side) {
            ball_bounce_count++;
            if (ball_bounce_count > 1) {
                score.increment_player_score(attacker.name);
                // attacker serves
            }
        } else {
            // POINT! for defender
            give_point_to_defender();
        }   
    }

    public void ball_collided_out_of_bounds() {
        // ignoring possible edge case of out of bounds happening on the attacker's side
        if (ball_bounce_count > 1) {
            score.increment_player_score(attacker.name);
        } else {
            //POINT for defender
            give_point_to_defender();
        }
    }

    private void give_point_to_defender() {
        if (attacker.name.Equals(player_1_name)) {
            score.increment_player_score(player_2_name);
        }
        else {
            score.increment_player_score(player_1_name);
        }
    }

}

