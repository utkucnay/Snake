using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour {
    public static event Action update;
    [SerializeField] float timeStap = .3f;

    IEnumerator Start() {
        while (true) {
            update.Invoke();
            yield return new WaitForSeconds(timeStap);
        }
    }

    private void Reset() {
        timeStap = .3f;
    }
}