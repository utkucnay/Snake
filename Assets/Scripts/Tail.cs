using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    Vector3 prevDir = Vector3.zero;

    private void Awake() {
        prevDir = transform.position;
    }

    public void UpdatePrevDir() {
        prevDir = transform.position;
    }

    public Vector3 GetPrevPosition()
    {
        return prevDir;
    }
}
