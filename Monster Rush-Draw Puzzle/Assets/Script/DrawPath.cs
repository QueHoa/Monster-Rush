using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class DrawPath : MonoBehaviour
{
    [HideInInspector]
    public LineRenderer line;
    [HideInInspector]
    public bool locked;
    [HideInInspector]
    public Vector3[] positions;
    [HideInInspector]
    public float pathLength;
    public Transform hero;

    [SerializeField]
    private CircleCollider2D endCollider;
    private MainController mainController;
    private Camera mainCamera;
    private CircleCollider2D startCollider;
    private Vector3 previousPos;
    [SerializeField]
    private float minDis;
    [SerializeField, Range(0, 2)]
    private float width;
    private int isTouch;
    private bool isDrawing = false;
    // Start is called before the first frame update
    void Start()
    {
        mainController = GameManager.Instance.mainController;
        line = GetComponent<LineRenderer>();
        line.positionCount = 1;
        mainCamera = Camera.main;
        line.startWidth = line.endWidth = width;
        previousPos = transform.position;
        startCollider = transform.GetChild(0).GetComponent<CircleCollider2D>();
        line.startColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        line.endColor = line.startColor;
        locked = false;
        isTouch = 0;
    }
    // Update is called once per frame
    public void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                return;
            }
            if (touch.phase == TouchPhase.Began && IsWithinStartCollider() && !locked)
            {
                isDrawing = true;
                mainController.backHome.interactable = false;
                mainController.skipLevel.interactable = false;
                mainController.iconAds.interactable = false;
                mainController.reload.interactable = false;
                isTouch = 1;
            }
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                Vector3 touchPos = mainCamera.ScreenToWorldPoint(touch.position);
                touchPos.z = 0;
                if (isDrawing)
                {
                    if (Vector3.Distance(touchPos, previousPos) > minDis)
                    {
                        if (previousPos == transform.position)
                        {
                            line.SetPosition(0, hero.position);
                            line.positionCount++;
                            line.SetPosition(1, transform.position);
                        }
                        else
                        {
                            line.positionCount++;
                            line.SetPosition(line.positionCount - 1, touchPos);
                            if (LastSegmentOnObstacle(line))
                            {
                                DeleteLastPoint();
                            }
                        }
                        previousPos = touchPos;
                    }
                }
            }
            if (touch.phase == TouchPhase.Ended && isTouch == 1)
            {
                if (IsWithinEndCollider())
                {
                    pathLength = CalculatePathLength();
                    positions = new Vector3[line.positionCount];
                    line.GetPositions(positions);
                    locked = true;
                }
                else if(!locked)
                {
                    ResetLine();
                }
                mainController.backHome.interactable = true;
                mainController.skipLevel.interactable = true;
                mainController.iconAds.interactable = true;
                mainController.reload.interactable = true;
                isDrawing = false;
                isTouch = 0;
            }
        }
    }
    public bool LastSegmentOnObstacle(LineRenderer line)
    {
        if (line.positionCount < 2)
            return false;

        return Physics2D.Linecast(line.GetPosition(line.positionCount - 1),
                                  line.GetPosition(line.positionCount - 2),
                                  LayerMask.GetMask("Obstacle"));
    }
    private void DeleteLastPoint()
    {
        if (line.positionCount > 1)
        {
            line.positionCount -= 1; 
        }
    }
    public void ResetLine()
    {
        line.positionCount = 1;
        line.SetPosition(0, transform.position);
    }
    bool IsWithinStartCollider()
    {
        Vector3 touchPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 touchPosition2D = new Vector2(touchPosition.x, touchPosition.y);
        return startCollider.OverlapPoint(touchPosition2D);
    }
    bool IsWithinEndCollider()
    {
        Vector3 touchPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 touchPosition2D = new Vector2(touchPosition.x, touchPosition.y);
        return endCollider.OverlapPoint(touchPosition2D);
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
}
