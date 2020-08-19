using System.Collections;
using System.Collections.Generic;
using MLAgents;
using UnityEngine;

public class Flappy : Agent {

    public float speed = 10;

    [SerializeField] private Pipe[] pipes;

    private Vector2 startPosition;
    private bool collidedPipe = false;
    private bool collidedGround = false;

    private new Rigidbody2D rigidbody;
    private RayPerception2D rayPerception;

    // Start is called before the first frame update
    void Start() {
        startPosition = transform.position;
        rigidbody = GetComponent<Rigidbody2D>();
        rayPerception = GetComponent<RayPerception2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "wall") {
            collidedPipe = true;
        } else if (other.tag == "ground") {
            collidedGround = true;
        }
    }

    //Details regarding machine learning 
    public override void AgentReset() {
        FlappyManager.Instance.SetScore(0);
        collidedPipe = false;
        collidedGround = false;
        transform.position = startPosition;
        rigidbody.velocity = Vector2.zero;

        for (int i = 0; i < pipes.Length; i++) {
            pipes[i].ResetPipe();
        }
    }

    public override void CollectObservations() {
        float rayDistance = 10f;
        float[] rayAngles = { 90f, 80f, 70f, 60f, 50f, 40f, 30f, 20f, 10f, 0f, -10f, -20f, -30f, -40f, -50f, -60f, -70f, -80f, -90f };
        string[] detectableObjects = { "wall" };

        AddVectorObs(rayPerception.Perceive(rayDistance, rayAngles, detectableObjects));
        AddVectorObs(transform.position.y);
        AddVectorObs(rigidbody.velocity.y);
    }


    public override void AgentAction(float[] vectorAction, string textAction) {
        var action = vectorAction[0];

        switch (action) {
            case 0: {
                    break;
                }
            case 1: {
                    rigidbody.velocity = Vector2.up * speed;
                    break;
                }
        }

        // Rewards
        AddReward(0.001f);
        if (collidedGround || collidedPipe) {
            AddReward(-1);
            Done();
        }
    }
}
