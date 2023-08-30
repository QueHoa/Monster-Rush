using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace MonsterRush.Model
{
    public class Path
    {
        public LineRenderer path;
        public float length;
        public List<float> segmentLength;

        public Path()
        {
            path = null;
            segmentLength = new List<float>();
        }

        public int GetPositionCount()
        {
            return path.positionCount;
        }

        public Vector3 GetPosition(int index)
        {
            if (index >= GetPositionCount())
                index = GetPositionCount() - 1;
            if (index < 0) index = 0;
            return path.GetPosition(index);
        }

        public void SetPath(LineRenderer lineRenderer)
        {
            this.path = lineRenderer;
            CalculatePathLength();
        }

        public void CalculatePathLength()
        {
            if (path == null)
            {
                length = 0.0001f;
            }
            else
            {
                length = 0;
                // ReSharper disable once TooWideLocalVariableScope
                float deltaL;
                for (var i = 1; i < GetPositionCount(); i++)
                {
                    deltaL = Utility.GetDistance(path.GetPosition(i), path.GetPosition(i - 1));
                    segmentLength.Add(deltaL);
                    length += deltaL;
                }
            }
        }

        #region Move On Path method

        public Sequence MoveObjectToEndOfPath(Transform obj, float runningTime, Action action)
        {
            var sequence = DOTween.Sequence();
            for (int i = 1; i < GetPositionCount(); i++)
            {
                int index = i;
                Tween t = obj.transform.DOMove(GetPosition(i), Utility.GetRunningTime(this, i - 1, runningTime))
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        if (GetPosition(index - 1).x < GetPosition(index).x)
                            obj.localScale = Vector3.left + Vector3.up + Vector3.forward;
                        else if (GetPosition(index - 1).x > GetPosition(index).x)
                            obj.localScale = Vector3.one;
                    });

                sequence.Append(t);
            }

            sequence.OnComplete(() =>
            {
                obj.localScale = Vector3.one;
                action();
            });

            return sequence;
        }


        public Sequence MoveObjectToEndOfPath(Transform obj, float runningTime)
        {
            Sequence sequence = DOTween.Sequence();
            for (int i = 1; i < GetPositionCount(); i++)
            {
                sequence.Append(obj.transform.DOMove(GetPosition(i), Utility.GetRunningTime(this, i - 1, runningTime))
                    .SetEase(Ease.Linear));
            }

            return sequence;
        }

        public Sequence MoveObjectToStartOfPath(Transform obj, float runningTime, Action action)
        {
            Tween t = null;
            var sequence = DOTween.Sequence();
            for (int i = GetPositionCount() - 2; i >= 0; i--)
            {
                int index = i;
                t = obj.DOMove(path.GetPosition(i), Utility.GetRunningTime(this, i, runningTime))
                    .SetEase(Ease.Linear).OnComplete(() =>
                {
                    if (GetPosition(index).x < GetPosition(index + 1).x)
                        obj.localScale = Vector3.one;
                    else if (GetPosition(index).x > GetPosition(index + 1).x)
                        obj.localScale = Vector3.left + Vector3.up + Vector3.forward;

                });
                sequence.Append(t);
            }

            sequence.OnComplete(() =>
            {
                obj.localScale = Vector3.one;
                action();
            });
            return sequence;
        }

        public Sequence MoveObjectToStartOfPath(Transform obj, float runningTime)
        {
            var sequence = DOTween.Sequence();
            for (int i = GetPositionCount() - 2; i >= 0; i--)
            {
                sequence.Append(obj.DOMove(path.GetPosition(i), Utility.GetRunningTime(this, i, runningTime))
                    .SetEase(Ease.Linear));
            }

            return sequence;
        }

        #endregion
    }
}