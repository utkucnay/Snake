using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

public class HUDCanvasPresenter : MonoBehaviour
{
    static readonly string HighScore = "High Score ";
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    public ScoreModel ScoreModel { get; set; }

    private void Awake() {
        ScoreModel = new();
    }

    private void Start() {
        ScoreModel.Score.Subscribe( x => scoreText.text = x.ToString());
        ScoreModel.HighScore.Subscribe(x => highScoreText.text = HighScore + x);
    }

    private void OnEnable() {
        GameManager.OnEndGame += OnEndGame; 
    }

    private void OnDisable() {
        GameManager.OnEndGame -= OnEndGame; 
    }

    public void OnEndGame(GameManager gameManager) {
        var newHighScore = ScoreModel.Score.Value;
        if(ScoreModel.HighScore.Value < newHighScore) {
            ScoreModel.SetHighScore(newHighScore);
            gameManager.EndGame(GameManager.EndGameStatus.NewHighScore);
        }
        else {
            gameManager.EndGame(GameManager.EndGameStatus.EndGame);
        }
    }
}

public class ScoreModel {
    public ReactiveProperty<int> Score { get; set; }
    public ReactiveProperty<int> HighScore{ get; private set; }
    public void SetHighScore(int highScore) {
        PlayerPrefs.SetInt("HighScore", highScore);
        HighScore.Value = highScore;
    }

    public ScoreModel()
    {
        if(!PlayerPrefs.HasKey("HighScore"))
            PlayerPrefs.SetInt("HighScore", default(int));
        HighScore ??= new(PlayerPrefs.GetInt("HighScore"));
        Score = new();
    }
}
