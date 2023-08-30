using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    #region define component
    Home home;
    [SerializeField] LineRenderer line;
    [SerializeField] SpriteRenderer sprite;
    Vector3 firstPoint;
    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        home = GetComponent<Home>();
        firstPoint = transform.GetChild(0).position;
    }
    #endregion

    bool isDrawing;
    private void Start()
    {
        isDrawing = false;
        ResetLine();
    }
    #region mouse behaviour
    private void OnMouseDown()
    {
        if (!GameController.canDraw) return;
        if (line.positionCount == 1 && !Utility.IsMouseOverUI()) // do not exist line
        {
            isDrawing = true;
            ResetLine();
            this.PostEvent(EventID.OnDrawEnter);
        }
    }

    private void OnMouseDrag()
    {
        if (!GameController.canDraw) return;
        if (isDrawing)
        {
            DrawSound.Play(); 
            AddPointToLine(Utility.GetMousePosition());
            if (LastSegmentOnObstacle(line))
            {
                DeleteLastPoint();
            }
        }
    }
    private void OnMouseUp()
    {
        if (!GameController.canDraw) return;
        isDrawing = false;
        this.PostEvent(EventID.OnDrawExit);
        if (home.SetLine(line))
            this.PostEvent(EventID.OnDrawCompleted);
        else
            ResetLine();
    }
    public static bool LastSegmentOnObstacle(LineRenderer line)
    {
        if (line.positionCount == 1) return false;

        return Physics2D.Linecast(Utility.GetLastPoint(line),
            line.GetPosition(line.positionCount - 2), LayerMask.GetMask("Obstacle"));
    }
    #endregion


    private void AddPointToLine(Vector3 point)
    {
        if (Utility.GetDistance(Utility.GetLastPoint(line), point) > Config.minSegmentLength)
        {
            line.positionCount += 1;
            line.SetPosition(line.positionCount - 1, point);
        }
    }
    private void DeleteLastPoint()
    {
        Logger.Warning("OnObstacle");
        if (line.positionCount > 1) { line.positionCount -= 1; }
    }
    private void ResetLine()
    {
        try
        {
            line.positionCount = 1;
            line.SetPosition(0, firstPoint);
        }
        catch
        {
            // ignored
        }
    }
    public void Reset()
    {
        ResetLine();
        isDrawing = false;
    }

}
