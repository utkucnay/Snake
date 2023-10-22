using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Zenject;

[System.Serializable]
public struct Canvases {
    public GameObject HUDCanvas;
    public GameObject EndGameCanvas;
    public GameObject HighScoreCanvas;
    public GameObject StartGameCanvas;
}

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour {
    public enum EndGameStatus {
        NewHighScore,
        EndGame,
    }

    public static event Action update;
    public static event Action<GameManager> OnEndGame;
    public static event Action<GameManager> OnStartGame;
    [SerializeField] float defaultTimeStap = .3f;
    float timeStap = -1;
    [SerializeField] Canvases canvases;

    IEnumerator Start() {
        while (true) {
            while(timeStap == -1) {
                yield return null;
            } 
            update.Invoke();
            yield return new WaitForSeconds(timeStap);
        }
    }

    public void StartGame() {
        OnStartGame?.Invoke(this);
        canvases.StartGameCanvas.SetActive(false);
        timeStap = defaultTimeStap;
    }   

    public void RequestEndGame() {
        OnEndGame.Invoke(this);
    }

    public void EndGame(EndGameStatus endGameStatus) {
        switch (endGameStatus)
        {
            case EndGameStatus.NewHighScore:
                canvases.HighScoreCanvas.SetActive(true);
                break;
            case EndGameStatus.EndGame:
                canvases.EndGameCanvas.SetActive(true);
                break; 
            default:
                break;
        }

        timeStap = -1;
        StartCoroutine(ResetGame());
    }

    public IEnumerator ResetGame() {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Reset() {
        timeStap = .3f;
    }
}