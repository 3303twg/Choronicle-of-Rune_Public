using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class CursorMover : MonoBehaviour
{
    [SerializeField] private RectTransform cursor;
    [SerializeField] private RectTransform pointAUI;
    [SerializeField] private RectTransform pointBUI;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private float delay = 0.2f;

    private void Start()
    {
        StartPingPong();
    }

    private void StartPingPong()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(cursor.DOAnchorPos(pointAUI.anchoredPosition, duration).SetEase(Ease.InOutSine));
        seq.AppendInterval(delay);
        seq.Append(cursor.DOAnchorPos(pointBUI.anchoredPosition, duration).SetEase(Ease.InOutSine));
        seq.AppendInterval(delay);
        seq.SetLoops(-1);
    }
}