using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public GamePlay gamePlay;
    public DrawPath path;
    public float speed = 0.1f;

    private Camera mainCamera;
    private BoxCollider2D boxCollider;
    private int moveIndex;
    private bool isBack;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        boxCollider = GetComponent<BoxCollider2D>();
        moveIndex = 0;
        isBack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gamePlay.isMove)
        {
            if (!isBack)
            {
                Vector2 curentPos = path.positions[moveIndex];
                transform.position = Vector2.MoveTowards(transform.position, curentPos, (speed * path.positions.Length) * Time.deltaTime);
                float distance = Vector2.Distance(curentPos, transform.position);
                if (distance <= 0.05f)
                {
                    moveIndex++;
                }
                if (moveIndex == path.positions.Length - 1)
                {
                    isBack = true;
                }
            }
            else
            {
                Vector2 curentPos = path.positions[moveIndex];
                transform.position = Vector2.MoveTowards(transform.position, curentPos, (speed * path.positions.Length) * Time.deltaTime);
                float distance = Vector2.Distance(curentPos, transform.position);
                if (distance <= 0.05f)
                {
                    moveIndex--;
                }
                if (moveIndex == 0)
                {
                    gamePlay.isMove = false;
                }
            }
        }
    }
}
