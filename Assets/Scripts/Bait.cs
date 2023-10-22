using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class Bait : MonoBehaviour
{
    static readonly int boundaryX = 10;
    static readonly int boundaryY = 7;

    [Inject] Tails tails; 
    [Inject] HUDCanvasPresenter hudCanvas;

    private void Awake() {
        transform.position = Vector3.up * 1000;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        tails.SpawnTail();
        ChangePosBait();
        hudCanvas.ScoreModel.Score.Value += 5;
    }

    private void OnEnable() {
        GameManager.OnStartGame += ChangePosBait;
    }

    private void OnDisable() {
        GameManager.OnStartGame -= ChangePosBait;
    }

    public void ChangePosBait(GameManager gameManager = null) {
        List<Vector2Int> avaiableGrid = new();

        for(int y = -boundaryY; y < boundaryY; y++) {
            for (int x = -boundaryX; x < boundaryX; x++) {
                Vector2Int pos = new Vector2Int(x, y);
                if(!Physics2D.OverlapCircle(pos, .2f)) {
                    avaiableGrid.Add(pos);
                }
            }
        }

        avaiableGrid = avaiableGrid.Randomize().ToList();
        transform.position = (Vector3Int)avaiableGrid[0];
    }
}
