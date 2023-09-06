using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    [HideInInspector]
    public bool isMove;

    [SerializeField]
    private DrawPath[] path;
    
    // Start is called before the first frame update
    void Start()
    {
        isMove = false;
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
            }
        }
    }
}
