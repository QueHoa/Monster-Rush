using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayTutorial : MonoBehaviour
{
    //[HideInInspector]
    public bool isMove;
    [HideInInspector]
    public int numberWin;
    [HideInInspector]
    public bool isLose;
    [HideInInspector]
    public bool isStop;
    [HideInInspector]
    public bool isEnd;

    public int rank;

    public DrawPath[] path;
    public PlayerControllerTutorial[] player;
    public GameObject[] line;

    private MainController mainController;
    private GameObject winGame;
    private GameObject loseGame;
    private int takeshot;
    private int move;
    
    // Start is called before the first frame update
    void Start()
    {
        mainController = GameManager.Instance.mainController;
        winGame = GameManager.Instance.winGame;
        loseGame = GameManager.Instance.loseGame;
        mainController.numberRank = rank;
        takeshot = 0;
        move = 0;
        isMove = false;
        numberWin = 0;
        isLose = false;
        isStop = false;
        isEnd = false;
        mainController.ui = false;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < path.Length; i++)
        {
            if (!path[i].locked)
            {
                isMove = false;
                break;
            }
            if(i == path.Length - 1 && path[i].locked)
            {
                isMove = true;
                mainController.ui = true;
            }
        }
        if (isMove && move == 0)
        {
            for (int i = 0; i < player.Length; i++)
            {
                player[i].MoveToEnd();
            }
            move++;
        }
        if (numberWin == player.Length)
        {
            StartCoroutine(WinGame());
        }
        if (isLose && takeshot == 0)
        {
            StartCoroutine(LoseGame());
            takeshot++;
        }
        if (mainController.swap)
        {
            for (int i = 0; i < player.Length; i++)
            {
                player[i].swap = true;
            }
            for (int i = 0; i < path.Length; i++)
            {
                path[i].locked = false;
                path[i].ResetLine();
            }
            mainController.swap = false;
        }
        for (int i = 0; i < line.Length; i++)
        {
            if (path[i].isDrawing)
            {
                for (int j = 0; j < line.Length; j++)
                {
                    line[j].SetActive(false);
                }
                break;
            }
            if (i == line.Length - 1 && !path[i].isDrawing)
            {
                for (int j = 0; j < line.Length; j++)
                {
                    line[j].SetActive(true);
                }
            }
        }
        for (int i = 0; i < line.Length; i++)
        {
            if (path[i].locked)
            {
                for (int j = 0; j < line.Length; j++)
                {
                    line[j].SetActive(false);
                }
                break;
            }
        }
    }
    IEnumerator WinGame()
    {
        winGame.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        mainController.gameObject.SetActive(false);
    }
    IEnumerator LoseGame()
    {
        yield return new WaitForSeconds(1.6f);
        loseGame.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        mainController.gameObject.SetActive(false);
    }
}
