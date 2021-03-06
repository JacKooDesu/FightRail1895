using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class ObjectTweener : MonoBehaviour  // 物件位移類別
{

    public Transform target;
    public int currentPoint = 0;

    [System.Serializable]
    public class TweenPoint
    {
        public Ease easeType = Ease.OutQuad;
        public float animationTime = .8f;
    }

    public Transform[] points;

    public float moveTime = .8f;    // 之後連同位移點寫入 Class或 Struct

    public void SetTarget(Transform t)  // 綁定位移物件
    {
        target = t;
    }

    public void MoveNextPoint()     // 尚未定義完整
    {

    }

    public void MoveToPoint(int p)  // 位移至定點
    {
        target.transform.DOMove(points[p].position, moveTime);
        target.transform.DORotate(points[p].eulerAngles, moveTime);
    }

    public void AddPoint()      // Editor mode function
    {

    }
}
