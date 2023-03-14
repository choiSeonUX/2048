using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject tile;
    [SerializeField] float moveSpeed = 5f;
    public int Width { private set; get; } = 4;
    public int Height { private set; get; } = 4;

    private void Start()
    {
        GenerateTilemap(); 
    }

    public void GenerateTilemap()
    {
        for(int y = 0; y < Height; ++y)
        {
            for(int x = 0; x < Width; ++x)
            {
                Vector3 position = new Vector3((-Width * -0.5f + 0.5f) + x, (Height * 0.5f - 0.5f) - y);
                SpwanTile(position);
            }
        }
    }

    private void SpwanTile(Vector3 position)
    {
        GameObject clone = Instantiate(tile, position, Quaternion.identity);
        clone.transform.SetParent(transform); // Tilemap2D 오브젝트 밑으로 넣
    }
}