using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawPath : MonoBehaviour
{
    [HideInInspector]
    public LineRenderer line;
    [HideInInspector]
    public bool locked;
    [HideInInspector]
    public Vector3[] positions;

    [SerializeField]
    private CircleCollider2D endCollider;
    private Camera mainCamera;
    private CircleCollider2D startCollider;
    private Vector3 previousPos;
    private Vector3 firstPoint;
    [SerializeField]
    private float minDis;
    [SerializeField, Range(0, 2)]
    private float width;
    private bool isDrawing = false;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 1;
        mainCamera = Camera.main;
        line.startWidth = line.endWidth = width;
        previousPos = transform.position;
        startCollider = transform.GetChild(0).GetComponent<CircleCollider2D>();
        firstPoint = transform.GetChild(0).position;
        locked = false;
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
            }
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                if (isDrawing)
                {
                    Vector3 touchPos = mainCamera.ScreenToWorldPoint(touch.position);
                    touchPos.z = 0f;

                    if (Vector3.Distance(touchPos, previousPos) > minDis)
                    {
                        if (previousPos == transform.position)
                        {
                            line.SetPosition(0, touchPos);
                        }
                        else
                        {
                            line.positionCount++;
                            line.SetPosition(line.positionCount - 1, touchPos);
                        }
                        previousPos = touchPos;
                    }
                }
            }
            if (touch.phase == TouchPhase.Ended )
            {
                if (IsWithinEndCollider())
                {
                    positions = new Vector3[line.positionCount];
                    line.GetPositions(positions);
                    locked = true;
                }
                else if(!locked)
                {
                    ResetLine();
                }
                isDrawing = false;
            }
        }
    }
    private void ResetLine()
    {
        line.positionCount = 1;
        line.SetPosition(0, firstPoint);
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
}
