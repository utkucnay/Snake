using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour, IFollow {
    enum ObjectHierarchy : byte
    {
        GFX,
        Collider,
        Tails
    }

    Vector3 dir = Vector3.zero;
    Vector3 prevDir = Vector3.zero;

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
        if(Input.GetKey(KeyCode.W) && dir != Vector3.down)
            dir = Vector3.up;
        if(Input.GetKey(KeyCode.S))
            dir = Vector3.down;
        if(Input.GetKey(KeyCode.A))
            dir = Vector3.left;
        if(Input.GetKey(KeyCode.D))
            dir = Vector3.right;
    }

    private void Move() {
        prevDir = transform.position;
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

    public Vector3 GetPrevPosition()
    {
        return prevDir;
    }
}
