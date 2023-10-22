using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Snake : MonoBehaviour {
    enum ObjectHierarchy : byte {
        GFX,
        Collider,
        Tails
    }

    Vector3 dir = Vector3.zero;
    Vector3 prevPos = Vector3.left;
    Vector3 prevDir = Vector3.zero;

    [Inject] GameManager gameManager;

    Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        GameManager.update += GameUpdate;    
    }

    private void OnDisable() {
        GameManager.update -= GameUpdate;
    }

    private void Update() {
        InputHandle();
    }

    private void FixedUpdate() {
        //For Sync Physics System
        rb.position = transform.position;
    }

    private void GameUpdate() {
        Move();
        TailMove();
    }

    private void InputHandle() {
        if(Input.GetKey(KeyCode.W) && prevDir != Vector3.down)
            dir = Vector3.up;
        if(Input.GetKey(KeyCode.S) && prevDir != Vector3.up)
            dir = Vector3.down;
        if(Input.GetKey(KeyCode.A) && prevDir != Vector3.right)
            dir = Vector3.left;
        if(Input.GetKey(KeyCode.D) && prevDir != Vector3.left)
            dir = Vector3.right;
        if(dir != Vector3.zero && prevDir == Vector3.zero) {
            gameManager.StartGame();
        }

    }

    private void Move() {
        prevDir = dir;
        prevPos = transform.position;
        transform.position += dir;
    }

    private void TailMove() {
        var tails = transform.GetChild((int)ObjectHierarchy.Tails).GetComponentsInChildren<Tail>();

        tails[0].transform.position = this.GetPrevPosition();
        for (int i = 1; i < tails.Length; i++)
        {
            tails[i].transform.position = tails[i - 1].GetPrevPosition();    
        }
        for (int i = 0; i < tails.Length; i++)
        {
            tails[i].UpdatePrevDir();    
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Tail") || other.CompareTag("Boundary")) {
            Debug.Log("EndGame");
            gameManager.RequestEndGame();
        }
    }

    public Vector3 GetPrevPosition()
    {
        return prevPos;
    }
}
