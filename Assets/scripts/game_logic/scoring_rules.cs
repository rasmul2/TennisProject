using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoring_rules : MonoBehaviour
{
    public GameObject attacker;

    public GameObject player_1_area;
    public GameObject player_2_area;
    public GameObject out_of_bounds_area;

    public Score score;

    private int ball_bounce_count;
    
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
    public void ball_collided_player_area(GameObject collided_area) {
        //area is not attacker's
        if (true) {
            ball_bounce_count++;
            if (ball_bounce_count > 1) {
                //POINT for attacker
                score.increment_player_score(attacker.name);
                // attacker serves
            }
        } else {
            // POINT for defender
        }   
    }

    public void ball_collided_out_of_bounds() {
        // ignoring possible edge case of out of bounds happening on the attacker's side
        if (ball_bounce_count > 1) {
            //POINT for attacker
        } else {
            //POINT for defender
        }
    }

}
