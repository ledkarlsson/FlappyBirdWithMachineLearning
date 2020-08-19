using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {
    [SerializeField] private float speed = 5;
    [SerializeField] private float resetPoint = -7;
    [SerializeField] private Transform spawnerPosition;

    private Vector2 startPosition;

    private float height;
    private float maxHeight = 4f;
    private float minHeight = -3f;

    private void Awake() {
        startPosition = transform.position;
        ResetPipe();
    }

    void Update() {
        transform.Translate(Vector2.left * Time.deltaTime * speed);

        if (transform.position.x <= resetPoint) {
            height = Random.Range(minHeight, maxHeight);
            transform.position = new Vector2(spawnerPosition.position.x, height);
            FlappyManager.Instance.IncreaseScore();
        }
    }

    public void ResetPipe() {
        height = Random.Range(minHeight, maxHeight);
        transform.position = new Vector2(startPosition.x, height);
    }
}
