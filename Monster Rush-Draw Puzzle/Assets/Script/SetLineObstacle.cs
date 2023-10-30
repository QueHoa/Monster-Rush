using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLineObstacle : MonoBehaviour
{
    public Transform[] point;

    [HideInInspector]
    public Vector3[] positions;

    private LineRenderer line;
    private Color brown = new Color(0.4f, 0.23f, 0.16f);

    void Start()
    {
        // Tạo một thành phần LineRenderer mới
        line = GetComponent<LineRenderer>();

        // Thiết lập các thuộc tính của LineRenderer
        line.positionCount = point.Length; // Số điểm trong đường thẳng
        line.startWidth = 0.1f; // Độ rộng ở đầu đường thẳng
        line.endWidth = 0.1f; // Độ rộng ở cuối đường thẳng
        line.material = new Material(Shader.Find("Sprites/Default")); // Đặt vật liệu cho đường thẳng
        line.startColor = brown; // Màu ở đầu
        line.endColor = brown; // Màu ở cuối
        for (int i = 0; i < point.Length; i++)
        {
            line.SetPosition(i, point[i].position);
        }
        
        positions = new Vector3[line.positionCount];
        line.GetPositions(positions);
    }
    void Update()
    {
        
    }
    public void ResetLine()
    {
        line.positionCount = 0;
    }
}
