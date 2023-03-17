using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private GameObject nodePrefab;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] private Sprite[] numberSprites;
    private float gap = 0.5f;
    public int value { get; set; }
    public bool isMove { get; set; } = false;

    public Node[,] gameBoard; // 게임 보드의 노드 배열
    public enum MoveState
    {
        Left,
        Right,
        Up,
        Down,
        None
    }

    private void Start()
    {
        gameBoard = new Node[4, 4];
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                gameBoard[i, j] = null;
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (transform.localPosition.x > -1.5f)
                Move(MoveState.Left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (transform.localPosition.x < 1.5f)
                Move(MoveState.Right);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (transform.localPosition.y < 1.5f)
                Move(MoveState.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (transform.localPosition.y > -1.5f)
                Move(MoveState.Down);
        }
    }
    public void Move(MoveState moveState)
    {
        if (isMove) return;

        isMove = true;
        float moveDistance = gap + 1f;

        Vector3 currentPos = new Vector3(transform.localPosition.x, transform.localPosition.y, -0.05f);
        Vector3 targetPos = currentPos;

        switch (moveState)
        {
            case MoveState.Left:
                targetPos.x -= moveDistance;
                break;
            case MoveState.Right:
                targetPos.x += moveDistance;
                break;
            case MoveState.Up:
                targetPos.y += moveDistance;
                break;
            case MoveState.Down:
                targetPos.y -= moveDistance;
                break;
        }

        //int x1 = Mathf.RoundToInt(transform.localPosition.x + 1f);
        //int y1 = Mathf.RoundToInt(transform.localPosition.y + 1f);
        //int x2 = Mathf.RoundToInt(targetPos.x + 1f);
        //int y2 = Mathf.RoundToInt(targetPos.y + 1f);

        //if (x1 != x2 || y1 != y2)
        //{
        //    gameBoard[x1, y1] = null;
        //    gameBoard[x2, y2] = this;
        //}

        StartCoroutine(MoveCoroutine(targetPos));
    }

    public IEnumerator MoveCoroutine(Vector3 targetPos)
    {
        while ((Vector3)transform.localPosition != targetPos)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        isMove = false;
        //int x = Mathf.RoundToInt(transform.localPosition.x + 1f);
        //int y = Mathf.RoundToInt(transform.localPosition.y + 1f);

        //if (gameBoard[x, y] == this)
        //{
        //    isMove = false;
        //}
        //else
        //{
        //    MoveOrCombined(x, y, Mathf.RoundToInt(targetPos.x + 1f), Mathf.RoundToInt(targetPos.y + 1f));
        //}
    }

    //public void MoveOrCombined(int x1, int y1, int x2, int y2)
    //{
    //    int value1 = gameBoard[x1, y1].value;
    //    int value2 = gameBoard[x2, y2].value;

    //    if (value1 == 0) // 이동할 블럭이 없으면
    //    {
    //        gameBoard[x1, y1].SetValue(value2, numberSprites[value2 - 1]);
    //        gameBoard[x2, y2].SetValue(0, null);
    //        gameBoard[x1, y1].Move(GetMoveState(x1, y1, x2, y2));
    //    }
    //    else if (value1 == value2) // 두 블럭의 값이 같으면
    //    {
    //        int combinedValue = value1 + value2;
    //        gameBoard[x1, y1].SetValue(combinedValue, numberSprites[combinedValue - 1]);
    //        gameBoard[x2, y2].SetValue(0, null);
    //        gameBoard[x1, y1].Move(GetMoveState(x1, y1, x2, y2));
    //        GameManager.instance.AddScore(combinedValue);
    //        gameBoard[x1, y1].Remove();
    //    }
    //    else // 두 블럭의 값이 다르면
    //    {
    //        if (x1 == x2) // 같은 열에서 움직일 경우
    //        {
    //            if (y1 < y2) // 밑에서 위로 움직일 경우
    //            {
    //                gameBoard[x2, y2].Move(MoveState.Down);
    //                gameBoard[x1, y1] = gameBoard[x2, y2];
    //                gameBoard[x2, y2] = new Node(0, null);
    //                gameBoard[x1, y1].Move(MoveState.Up);
    //            }
    //            else // 위에서 밑으로 움직일 경우
    //            {
    //                gameBoard[x2, y2].Move(MoveState.Up);
    //                gameBoard[x1, y1] = gameBoard[x2, y2];
    //                gameBoard[x2, y2] = new Node(0, null);
    //                gameBoard[x1, y1].Move(MoveState.Down);
    //            }
    //        }
    //        else if (y1 == y2) // 같은 행에서 움직일 경우
    //        {
    //            if (x1 < x2) // 왼쪽에서 오른쪽으로 움직일 경우
    //            {
    //                gameBoard[x2, y2].Move(MoveState.Left);
    //                gameBoard[x1, y1] = gameBoard[x2, y2];
    //                gameBoard[x2, y2] = new Node(0, null);
    //                gameBoard[x1, y1].Move(MoveState.Right);
    //            }
    //            else // 오른쪽에서 왼쪽으로 움직일 경우
    //            {
    //                gameBoard[x2, y2].Move(MoveState.Right);
    //                gameBoard[x1, y1] = gameBoard[x2, y2];
    //                gameBoard[x2, y2] = new Node(0, null);
    //                gameBoard[x1, y1].Move(MoveState.Left);
    //            }
    //        }
    //    }
    //}

    public Node(int value, Sprite[] sprites)
    {
        this.value = value;
      //  this.numberSprites = sprites[value-1];
    }

    MoveState GetMoveState(int x1, int y1, int x2, int y2)
    {
        if (x1 == x2)
        {
            if (y1 < y2)
            {
                return MoveState.Up;
            }
            else
            {
                return MoveState.Down;
            }
        }
        else if (y1 == y2)
        {
            if (x1 < x2)
            {
                return MoveState.Right;
            }
            else
            {
                return MoveState.Left;
            }
        }
        else
        {
            return MoveState.None;
        }
    }


    public void SetNumber(int num)
    {
        GameObject Node = transform.Find("Node").gameObject;
        SpriteRenderer spriteRenderer = Node.GetComponent<SpriteRenderer>(); 
        spriteRenderer.sprite = numberSprites[num];
    }

    public void SetValue(int _value, Sprite sprite)
    {
        value = _value;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
    public void Remove()
    {
        Destroy(gameObject);
    }


}
