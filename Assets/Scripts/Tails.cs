using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tails : MonoBehaviour
{
    [SerializeField] GameObject tailPrefab;
    public void SpawnTail() {
        var goTail = Instantiate(tailPrefab, transform);
        goTail.transform.position = transform.GetChild(transform.childCount - 2).position;
    }
}
