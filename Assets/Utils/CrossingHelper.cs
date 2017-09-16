using UnityEngine;
using System.Collections.Generic;

public static class CrossingHelper
{
    /*
    public static Vector2 CircleCircle(Vector2 center1, float rad1, Vector2 center2, float rad2)
    {
        float diff = (center2 - center1).magnitude - rad1 - rad2;

        if (diff >= 0) return Vector2.zero;
        else diff = -diff;

        return (center2 - center1).normalized * diff;
    }

    public static Vector2 RectRect(Rect r1, Rect r2)
    {
        if (r2.xMax <= r1.xMin) return Vector2.zero;
        if (r2.xMin >= r1.xMax) return Vector2.zero;
        if (r2.yMax <= r1.yMin) return Vector2.zero;
        if (r2.yMin >= r1.yMax) return Vector2.zero;

        bool leftCross = false;
        bool rightCross = false;
        bool upCross = false;
        bool downCross = false;

        if (r2.xMin < r1.xMax) leftCross = true;
        if (r2.xMax > r1.xMin) rightCross = true;
        if (r2.yMax > r1.yMin) downCross = true;
        if (r2.yMin < r1.yMax) upCross = true;

        if(!leftCross && !rightCross && !upCross && !downCross) return Vector2.zero;

        float xDiff1 = 0;
        float yDiff1 = 0;

        //пересечения уголками
        if (leftCross && upCross && !rightCross && !downCross)
        {
            xDiff1 = r1.xMax - r2.xMin;
            yDiff1 = r1.yMax - r2.yMin;
            if (xDiff1 < yDiff1) return new Vector2(xDiff1, 0);
            else return new Vector2(0, yDiff1);
        }
        if (leftCross && !upCross && !rightCross && downCross)
        {
            xDiff1 = r1.xMax - r2.xMin;
            yDiff1 = r2.yMax - r1.yMin;
            if (xDiff1 < yDiff1) return new Vector2(xDiff1, 0);
            else return new Vector2(0, -yDiff1);
        }
        if (!leftCross && upCross && rightCross && !downCross)
        {
            xDiff1 = r2.xMax - r1.xMin;
            yDiff1 = r1.yMax - r2.yMin;
            if (xDiff1 < yDiff1) return new Vector2(-xDiff1, 0);
            else return new Vector2(0, yDiff1);
        }
        if (!leftCross && !upCross && rightCross && downCross)
        {
            xDiff1 = r2.xMax - r1.xMin;
            yDiff1 = r2.yMax - r1.yMin;
            if (xDiff1 < yDiff1) return new Vector2(-xDiff1, 0);
            else return new Vector2(0, -yDiff1);
        }

        //вставки
        if (leftCross && upCross && !rightCross && downCross)
        {
            xDiff1 = r1.xMax - r2.xMin;
            return new Vector2(xDiff1, 0);
        }
        if (!leftCross && upCross && rightCross && downCross)
        {
            xDiff1 = r2.xMax - r1.xMin;
            return new Vector2(-xDiff1, 0);
        }
        if (leftCross && upCross && rightCross && !downCross)
        {
            yDiff1 = r1.yMax - r2.yMin;
            return new Vector2(0, yDiff1);
        }
        if (leftCross && !upCross && rightCross && downCross)
        {
            yDiff1 = r2.yMax - r1.yMin;
            return new Vector2(0, -yDiff1);
        }


        float xDiff2 = 0;
        float yDiff2 = 0;

        //вхождение
        if (leftCross && upCross && rightCross && downCross)
        {
            xDiff1 = r1.xMax - r2.xMin;
            xDiff2 = r2.xMax - r1.xMin;
            if (xDiff2 < xDiff1) xDiff1 = -xDiff2;

            yDiff1 = r1.yMax - r2.yMin;
            yDiff2 = r2.yMax - r1.yMin;
            if (yDiff2 < yDiff1) yDiff1 = -yDiff2;

            if (Mathf.Abs(xDiff1) < Mathf.Abs(yDiff1)) return new Vector2(xDiff1, 0);
            else return new Vector2(0, yDiff1);
        }

        return Vector2.zero;
    }

    public static Vector2 CircleRect(Vector2 center1, float rad1, Rect r2)
    {
        return -RectCircle(r2, center1, rad1);
    }

    public static Vector2 RectCircle(Rect r1, Vector2 center2, float rad2)
    {
        Rect r2 = new Rect(new Vector2(center2.x - rad2, center2.y - rad2), new Vector2(rad2 * 2, rad2 * 2));

        if (r2.xMax <= r1.xMin) return Vector2.zero;
        if (r2.xMin >= r1.xMax) return Vector2.zero;
        if (r2.yMax <= r1.yMin) return Vector2.zero;
        if (r2.yMin >= r1.yMax) return Vector2.zero;

        bool leftCross = false;
        bool rightCross = false;
        bool upCross = false;
        bool downCross = false;

        if (r2.xMin < r1.xMax) leftCross = true;
        if (r2.xMax > r1.xMin) rightCross = true;
        if (r2.yMax > r1.yMin) downCross = true;
        if (r2.yMin < r1.yMax) upCross = true;

        if (!leftCross && !rightCross && !upCross && !downCross) return Vector2.zero;

        float xDiff1 = 0;
        float yDiff1 = 0;
        float diff = 0;
        Vector2 point;

        //пересечения уголками
        if (leftCross && upCross && !rightCross && !downCross)
        {
            if (r1.yMax < center2.y && r1.xMax < center2.x)
            {
                point = new Vector2(r1.xMax, r1.yMax);
                diff = (center2 - point).magnitude - rad2;
                if (diff >= 0)
                {
                    return Vector2.zero;
                }
                return (center2 - point).normalized * (-diff);
            }

            xDiff1 = r1.xMax - r2.xMin;
            yDiff1 = r1.yMax - r2.yMin;
            if (xDiff1 < yDiff1) return new Vector2(xDiff1, 0);
            else return new Vector2(0, yDiff1);
        }
        if (leftCross && !upCross && !rightCross && downCross)
        {
            if (r1.yMin > center2.y && r1.xMax < center2.x)
            {
                point = new Vector2(r1.xMax, r1.yMin);
                diff = (center2 - point).magnitude - rad2;
                if (diff >= 0)
                {
                    return Vector2.zero;
                }
                return (center2 - point).normalized * (-diff);
            }
            xDiff1 = r1.xMax - r2.xMin;
            yDiff1 = r2.yMax - r1.yMin;
            if (xDiff1 < yDiff1) return new Vector2(xDiff1, 0);
            else return new Vector2(0, -yDiff1);
        }
        if (!leftCross && upCross && rightCross && !downCross)
        {
            if (r1.yMax < center2.y && r1.xMin > center2.x)
            {
                point = new Vector2(r1.xMin, r1.yMax);
                diff = (center2 - point).magnitude - rad2;
                if (diff >= 0)
                {
                    return Vector2.zero;
                }
                return (center2 - point).normalized * (-diff);
            }
            xDiff1 = r2.xMax - r1.xMin;
            yDiff1 = r1.yMax - r2.yMin;
            if (xDiff1 < yDiff1) return new Vector2(-xDiff1, 0);
            else return new Vector2(0, yDiff1);
        }
        if (!leftCross && !upCross && rightCross && downCross)
        {
            if (r1.yMin > center2.y && r1.xMin > center2.x)
            {
                point = new Vector2(r1.xMin, r1.yMin);
                diff = (center2 - point).magnitude - rad2;
                if (diff >= 0)
                {
                    return Vector2.zero;
                }
                return (center2 - point).normalized * (-diff);
            }
            xDiff1 = r2.xMax - r1.xMin;
            yDiff1 = r2.yMax - r1.yMin;
            if (xDiff1 < yDiff1) return new Vector2(-xDiff1, 0);
            else return new Vector2(0, -yDiff1);
        }

        //вставки
        if (leftCross && upCross && !rightCross && downCross)
        {
            xDiff1 = r1.xMax - r2.xMin;
            return new Vector2(xDiff1, 0);
        }
        if (!leftCross && upCross && rightCross && downCross)
        {
            xDiff1 = r2.xMax - r1.xMin;
            return new Vector2(-xDiff1, 0);
        }
        if (leftCross && upCross && rightCross && !downCross)
        {
            yDiff1 = r1.yMax - r2.yMin;
            return new Vector2(0, yDiff1);
        }
        if (leftCross && !upCross && rightCross && downCross)
        {
            yDiff1 = r2.yMax - r1.yMin;
            return new Vector2(0, -yDiff1);
        }


        float xDiff2 = 0;
        float yDiff2 = 0;

        //вхождение
        if (leftCross && upCross && rightCross && downCross)
        {
            xDiff1 = r1.xMax - r2.xMin;
            xDiff2 = r2.xMax - r1.xMin;
            if (xDiff2 < xDiff1) xDiff1 = -xDiff2;

            yDiff1 = r1.yMax - r2.yMin;
            yDiff2 = r2.yMax - r1.yMin;
            if (yDiff2 < yDiff1) yDiff1 = -yDiff2;

            if (Mathf.Abs(xDiff1) < Mathf.Abs(yDiff1)) return new Vector2(xDiff1, 0);
            else return new Vector2(0, yDiff1);
        }

        return Vector2.zero;
    }
    */

    public static bool IsRectIntersect(Rect r1, Rect r2)
    {
        if (r2.xMax <= r1.xMin) return false;
        if (r2.xMin >= r1.xMax) return false;
        if (r2.yMax <= r1.yMin) return false;
        if (r2.yMin >= r1.yMax) return false;
        return true;
    }

    private static Vector2 _GetRectIntersectShift(Rect r1, Rect r2)
    {
        float xDiff1 = float.MaxValue;
        float yDiff1 = float.MaxValue;
        float xDiff2 = float.MinValue;
        float yDiff2 = float.MinValue;

        if (r2.xMin < r1.xMax) xDiff1 = r1.xMax - r2.xMin;
        if (r2.xMax > r1.xMin) xDiff2 = r1.xMin - r2.xMax;
        if (r2.yMax > r1.yMin) yDiff1 = r1.yMin - r2.yMax;
        if (r2.yMin < r1.yMax) yDiff1 = r1.yMax - r2.yMin;

        return new Vector2(Mathf.Abs(xDiff1) < Mathf.Abs(xDiff2) ? xDiff1 : xDiff2, Mathf.Abs(yDiff1) < Mathf.Abs(yDiff2) ? yDiff1 : yDiff2);
    }

    public static void GetRectIntersectShift(Rect r1, Rect r2, out Vector2 v1, out Vector2 v2, out Vector2 v3, out Vector2 v4)
    {
        Vector2 v = _GetRectIntersectShift(r1, r2);
        v1 = new Vector2(v.x > 0 ? v.x + 0.01f : v.x - 0.01f, 0);
        v2 = new Vector2(v.x > 0 ? v.x - r1.width - r2.width - 0.01f : v.x + r1.width + r2.width + 0.01f, 0);
        v3 = new Vector2(0, v.y > 0 ? v.y + 0.01f : v.y - 0.01f);
        v4 = new Vector2(0, v.y > 0 ? v.y - r1.height - r2.height - 0.01f : v.y + r1.height + r2.height + 0.01f);
    }



}
