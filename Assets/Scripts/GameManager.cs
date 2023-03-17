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

        // BestScore ����
        UpdateBestScore();
    }

    // BestScore ����
    private void UpdateBestScore()
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt(BestScoreKey, bestScore);
            UpdateBestScoreText();
        }
    }

    // BestScore UI �ؽ�Ʈ ����
    private void UpdateBestScoreText()
    {
        bestScoreText.text = bestScore.ToString();
    }

    // �� ���� ����
    public void StartNewGame()
    {
        score = 0;
        scoreText.text = score.ToString();
        UpdateBestScoreText(); // BestScore UI �ؽ�Ʈ �ʱ�ȭ

        spawner.GenerateTilemap();
        tryAgainButton.gameObject.SetActive(false);
        clearButton.gameObject.SetActive(false);

        NewGame(); // ��� ����
    }

    public void NewGame()
    {
        spawner.SpawnNode();
    }

    // �ٽ� �õ�
    public void TryAgain()
    {
        StartNewGame();
    }

    // Ŭ����
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
