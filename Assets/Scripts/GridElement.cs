using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GridElement : MonoBehaviour
{
    [SerializeField] private Transform markTransform;
    public bool currentMarkStatus = false;
    public bool isMatched = false;
    public bool isChecked = false;
    private Vector2 gridCoordinate = Vector2.zero;
    private GridController gridController;

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

    public void SetController(GridController controller)
    {
        gridController = controller;
    }

    public void CheckMatches()
    {
        
        isChecked = true;
        List<GridElement> alignedGridElements = new List<GridElement>();

        for(int x=-1; x<2; x++)
        {
            for(int y=-1; y<2; y++)
            {
                Vector2 checkPos = new Vector2(gridCoordinate.x + x, gridCoordinate.y + y);
                if(x != 0 && y != 0 || x == 0 && y == 0) continue; //cross check and self-check disabled

                GridElement selectedElement = gridController.GetGridElement(checkPos);
                if(selectedElement == null) continue;
                if(!selectedElement.currentMarkStatus) continue;
                
                alignedGridElements.Add(selectedElement);

            }
        }

        bool matchStatus = false;
        if(alignedGridElements.Count > 1) 
        {
            matchStatus = true;
            isMatched = true;
        }
        

        foreach(GridElement element in alignedGridElements)
        {
            element.isMatched = matchStatus;
            if(!element.isChecked) element.CheckMatches();
        }
    }
}
