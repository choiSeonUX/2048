using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private Spawner spawner; 
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button tryAgainButton;
    [SerializeField] private Button clearButton;
    [SerializeField] private Sprite[] numberSprites;

    private int score;
    private int bestScore;
    public bool isMove = false;
    private float gap = 0.5f;

    private const string BestScoreKey = "BestScore";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        spawner.GenerateTilemap();
        tryAgainButton.onClick.AddListener(TryAgain);
        clearButton.onClick.AddListener(Clear);
        newGameButton.onClick.AddListener(NewGame);

        bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
        UpdateBestScoreText();
    }
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = score.ToString();

        // BestScore 갱신
        UpdateBestScore();
    }

    // BestScore 갱신
    private void UpdateBestScore()
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt(BestScoreKey, bestScore);
            UpdateBestScoreText();
        }
    }

    // BestScore UI 텍스트 갱신
    private void UpdateBestScoreText()
    {
        bestScoreText.text = bestScore.ToString();
    }

    // 새 게임 시작
    public void StartNewGame()
    {
        score = 0;
        scoreText.text = score.ToString();
        UpdateBestScoreText(); // BestScore UI 텍스트 초기화

        spawner.GenerateTilemap();
        tryAgainButton.gameObject.SetActive(false);
        clearButton.gameObject.SetActive(false);

        NewGame(); // 노드 생성
    }

    public void NewGame()
    {
        spawner.SpawnNode();
    }

    // 다시 시도
    public void TryAgain()
    {
        StartNewGame();
    }

    // 클리어
    public void Clear()
    {
        StartNewGame();
    }

    public void UpdateNodeValue(Node node, int value)
    {
        node.value = value;
        node.GetComponent<Image>().sprite = numberSprites[value - 1];
    }


}
