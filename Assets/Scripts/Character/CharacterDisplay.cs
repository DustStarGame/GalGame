using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterDisplay : MonoBehaviour
{
    public Image image;

    //初始化
    public void Setup(Sprite sprite, Vector2 anchor, Vector2 scale)
    {
        image.sprite = sprite;
        RectTransform rt = (RectTransform)transform;
        rt.anchorMin = rt.anchorMax = anchor;
        rt.anchoredPosition = Vector2.zero;
        rt.localScale = scale;
        gameObject.SetActive(true);
    }

    public void SetEmotion(Sprite sprite)
    {
        if (sprite != null)
            image.sprite = sprite;
    }

    public void MoveToX(float targetX, float duration)
    {
        RectTransform rt = (RectTransform)transform;
        rt.DOKill();
        rt.DOAnchorPosX(targetX, duration)
        .SetEase(Ease.OutCubic);
    }
}
