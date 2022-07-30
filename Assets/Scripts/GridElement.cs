using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GridElement : MonoBehaviour
{
    [SerializeField] private Transform markTransform;
    private Vector2 gridCoordinate = Vector2.zero;

    private bool currentMarkStatus = false;
    private float markAnimationSpeed = 0.3f;
    void Start()
    {
        markTransform.localScale = Vector3.zero;
    }


    public void SetMarkStatus(bool status)
    {
        currentMarkStatus = status;

        Vector3 targetScale = currentMarkStatus ? Vector3.one : Vector3.zero;

        markTransform.DOScale(targetScale, markAnimationSpeed);
    }

    public void SetCoordinate(Vector2 coord)
    {
        gridCoordinate = coord;
    }

    public Vector2 GetCoordiante()
    {
        return gridCoordinate;
    }
}
