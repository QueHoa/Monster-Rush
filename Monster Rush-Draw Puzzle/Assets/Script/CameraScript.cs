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
        Camera.main.orthographicSize = img.bounds.size.x * Screen.height / Screen.width * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
