using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private SpriteRenderer img;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<SpriteRenderer>();
        Debug.Log(img.bounds.size.x);
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = img.bounds.size.x / img.bounds.size.y;

        if (screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = img.bounds.size.y / 2;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = img.bounds.size.y / 2 * differenceInSize;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
