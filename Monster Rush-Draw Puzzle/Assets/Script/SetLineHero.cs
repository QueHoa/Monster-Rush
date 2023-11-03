using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLineHero : MonoBehaviour
{
    public GamePlay gamePlay;
    public Transform[] point;
    public DrawPath path;
    public GameColor color;

    private LineRenderer line;
    private Color blue = new Color(0, 0.8f, 1);
    private Color red = new Color(1, 0, 0);
    private Color yellow = new Color(1, 0.9f, 0);

    void Start()
    {
        // Tạo một thành phần LineRenderer mới
        line = GetComponent<LineRenderer>();

        // Thiết lập các thuộc tính của LineRenderer
        line.positionCount = point.Length; // Số điểm trong đường thẳng
        line.startWidth = 0.1f; // Độ rộng ở đầu đường thẳng
        line.endWidth = 0.1f; // Độ rộng ở cuối đường thẳng
        line.material = new Material(Shader.Find("Sprites/Default")); // Đặt vật liệu cho đường thẳng
        if(color == GameColor.Blue)
        {
            line.startColor = blue; // Màu ở đầu
            line.endColor = blue; // Màu ở cuối
        }else if (color == GameColor.Red)
        {
            line.startColor = red; // Màu ở đầu
            line.endColor = red; // Màu ở cuối
        }
        else if (color == GameColor.Yellow)
        {
            line.startColor = yellow; // Màu ở đầu
            line.endColor = yellow; // Màu ở cuối
        }

        for (int i = 0; i < point.Length; i++)
        {
            line.SetPosition(i, point[i].position);
        }
        path.positions = new Vector3[line.positionCount];
        line.GetPositions(path.positions);
        path.pathLength = CalculatePathLength();
    }
    void Update()
    {
        path.locked = true;
        if (gamePlay.isEnd)
        {
            ResetLine();
        }
    }
    public float CalculatePathLength()
    {
        float totalLength = 0f;

        for (int i = 1; i < line.positionCount; i++)
        {
            Vector3 startPoint = line.GetPosition(i - 1);
            Vector3 endPoint = line.GetPosition(i);

            totalLength += Vector3.Distance(startPoint, endPoint);
        }

        return totalLength;
    }
    public void ResetLine()
    {
        line.positionCount = 0;
    }
}
