using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private GameObject nodePrefab;
    [SerializeField] private float gap = 0.5f;
    [SerializeField] private Sprite[] numberSprites;

    public Dictionary<Vector2, GameObject> tileMap = new Dictionary<Vector2, GameObject>();

    private int[,] boardData;
    public int gridSize { get; set; } = 4;

    private Transform _blocks;

    private void Start()
    {
        _blocks = transform.Find("Blocks");

        GenerateTilemap();
        SpawnNode();
        SpawnNode();
    }

    public void GenerateTilemap()
    {
        float xOffset = gridSize * 0.5f - 0.5f;
        float yOffset = gridSize * 0.5f - 0.5f;
        for (int y = 0; y < gridSize; ++y)
        {
            for (int x = 0; x < gridSize; ++x)
            {
                Vector3 position = new Vector3(x - xOffset, y - yOffset, 0f);
                position += new Vector3(gap * x - 0.75f, gap * y - 0.75f);
                SpawnBlock(position);
            }
        }
        boardData = new int[gridSize, gridSize];
    }

    private void SpawnBlock(Vector3 position)
    {
        GameObject block = Instantiate(blockPrefab, _blocks);
        block.transform.localPosition = position;
        // GameObject block = Instantiate(blockPrefab, position, Quaternion.identity);
        // block.transform.SetParent(transform);
    }

    public void SpawnNode()
    {
        // 빈 위치 저장 - 노드 생성 전 - 생성 후 위치 저장 
        List<Vector2> emptyPositions = new List<Vector2>();
        for (int x = 0; x < gridSize; ++x)
        {
            for (int y = 0; y < gridSize; ++y)
            {
                Vector2 pos = new Vector2(x, y);
                if (!tileMap.ContainsKey(pos))
                {
                    emptyPositions.Add(pos);
                }
            }
        }

        // 빈 위치가 없으면 종료
        if (emptyPositions.Count == 0) return;

        // 2개의 위치 랜덤으로 선택
        int index = Random.Range(0, emptyPositions.Count);

        // 랜덤한 값(2 또는 4) 생성
        int Value = Random.value < 0.9f ? 2 : 4;

        // 노드 생성
        GameObject nodeClone = Instantiate(nodePrefab, Vector3.zero, Quaternion.identity);
        nodeClone.transform.SetParent(transform);
        nodeClone.transform.localPosition = GetNodePosition(emptyPositions[index]);
        nodeClone.GetComponentInChildren<SpriteRenderer>().sprite = numberSprites[Value / 2 - 1];
        tileMap.Add(emptyPositions[index], nodeClone);

      //  boardData[(int)emptyPositions[index].x, (int)emptyPositions[index].y] = Value;
    }

    private Vector3 GetNodePosition(Vector2 tilePos)
    {
        float xOffset = gridSize * 0.5f - 0.5f;
        float yOffset = gridSize * 0.5f - 0.5f;
        Vector3 position = new Vector3(tilePos.x - xOffset, tilePos.y - yOffset, 0f);
        position += new Vector3(gap * tilePos.x - 0.75f, gap * tilePos.y - 0.75f, -0.05f);

        return _blocks.GetChild((int)(tilePos.y * gridSize + tilePos.x)).position;
    }

    //public int[,] GetBoardData()
    //{
    //    return boardData;
    //}
}
