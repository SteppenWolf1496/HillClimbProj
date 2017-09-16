using UnityEngine;
using System.Collections;


public static class MathUtility
{
    public const float DegToRadCoef = Mathf.PI / 180.0f;
    public const float RadToDegCoef = 180.0f / Mathf.PI;

    public static Vector3 GetProjectionOnPlane(Vector3 a, Vector3 b, Vector3 c, Vector3 s)//проекция из точки s на плоскость, заданную тремя точками, в общем виде //http://ateist.spb.ru/mw/distpoint.htm
    {
        float a11 = (b.y - a.y) * (c.z - a.z) - (b.z - a.z) * (c.y - a.y);
        float a12 = (b.z - a.z) * (c.x - a.x) - (b.x - a.x) * (c.z - a.z);
        float a13 = (b.x - a.x) * (c.y - a.y) - (b.y - a.y) * (c.x - a.x);

        float a21 = b.x - a.x;
        float a22 = b.y - a.y;
        float a23 = b.z - a.z;
        float a31 = c.x - a.x;
        float a32 = c.y - a.y;
        float a33 = c.z - a.z;


        float b1 = a.x * a11 + a.y * a12 + a.z * a13;
        float b2 = s.x * (b.x - a.x) + s.y * (b.y - a.y) + s.z * (b.z - a.z);
        float b3 = s.x * (c.x - a.x) + s.y * (c.y - a.y) + s.z * (c.z - a.z);


        float del = Matrix33Determinant(a11, a12, a13, a21, a22, a23, a31, a32, a33);

        float xm = Matrix33Determinant(
            b1, a12, a13,
            b2, a22, a23,
            b3, a32, a33
            );

        float ym = Matrix33Determinant(
            a11, b1, a13,
            a21, b2, a23,
            a31, b3, a33);

        float zm = Matrix33Determinant(
            a11, a12, b1,
            a21, a22, b2,
            a31, a32, b3);

        return new Vector3(xm / del, ym / del, zm / del);
    }

    //угол между векторами, от -180 до 180 градусов, с учётом знака поворота
    public static float Angle(Vector2 from, Vector2 to)
    {
        from = from.normalized;
        to = to.normalized;

        float a1 = Mathf.Acos(from.x) * RadToDegCoef;
        float a2 = Mathf.Acos(to.x) * RadToDegCoef;

        while(a1 > 180) a1 -= 360;
        while(a1 < -180) a1 += 360;

        while(a2 > 180) a2 -= 360;
        while(a2 < -180) a2 += 360;

        if(from.y < 0) {
            a1 = -a1;
        }
        if(to.y < 0) {
            a2 = -a2;
        }

        float angle = a2 - a1;

        while(angle > 180) angle -= 360;
        while(angle < -180) angle += 360;

        return angle;
    }

    public static Vector3 VectorRotate(Vector2 v, float angle)
    {
        float a = Angle(Vector2.right, v) + angle;
        a *= DegToRadCoef;
        Vector2 res = new Vector2(Mathf.Cos(a), Mathf.Sin(a));
        res *= v.magnitude;
        return res;
    }

    public static Vector2 Perpendicular(Vector2 v)
    {
        if(v.Equals(Vector2.zero)) return Vector2.zero;
        Vector2 res;
        if(v.y != 0) {
            float y2 = -(v.x / v.y);
            res.x = 1;
            res.y = y2;
        } else {
            float x2 = -(v.y / v.x);
            res.x = x2;
            res.y = 1;
        }

        return res.normalized;
    }

    public static Vector2 PerpendicularLeft(Vector2 v)
    {
        if(v.Equals(Vector2.zero)) return Vector2.zero;
        Vector2 res = Perpendicular(v);
        float angle = Angle(v, res);
        if(angle < 0) {
            res = -res;
        }
        return res;
    }

    public static Vector2 PerpendicularRight(Vector2 v)
    {
        if(v.Equals(Vector2.zero)) return Vector2.zero;
        Vector2 res = Perpendicular(v);
        float angle = Angle(v, res);
        if(angle > 0) {
            res = -res;
        }
        return res;
    }

    private static float Matrix33Determinant(
        float a11, float a12, float a13,
        float a21, float a22, float a23,
        float a31, float a32, float a33)
    {
        return a11 * a22 * a33 - a11 * a23 * a32 - a12 * a21 * a33 + a12 * a23 * a31 + a13 * a21 * a32 - a13 * a22 * a31;
    }
}




