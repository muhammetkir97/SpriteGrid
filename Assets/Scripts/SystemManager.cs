using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    [SerializeField] private UIController uiController;
    [SerializeField] private GridController gridController;
    private List<Vector2> markedTiles = new List<Vector2>();

    private int matchCount = 0;
    void Start()
    {
        uiController.reloadButtonCallbacks += ReloadGrid;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) ClickToGrid();
    }

    private void ClickToGrid()
    {

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hitInfo = Physics2D.Raycast(mouseWorldPosition, Vector2.zero);
        if (!hitInfo) return;

        GridElement selectedElement = hitInfo.transform.GetComponent<GridElement>();

        if(selectedElement.currentMarkStatus) return;
        
        selectedElement.SetMarkStatus(true);
        CheckMatches(selectedElement);
        
    }

    private void CheckMatches(GridElement firstElement)
    {
        List<GridElement> matchList = new List<GridElement>();
        List<GridElement> checkList = new List<GridElement>();
        List<GridElement> allCheckedElements = new List<GridElement>();
        checkList.Add(firstElement);
        allCheckedElements.Add(firstElement);

        while(checkList.Count > 0)
        {
            checkList[0].isChecked = true;

            List<GridElement> alignedGridElements = new List<GridElement>();

            for(int x=-1; x<2; x++)
            {
                for(int y=-1; y<2; y++)
                {
                    Vector2 checkPos = new Vector2(checkList[0].GetCoordiante().x + x, checkList[0].GetCoordiante().y + y);
                    if((x != 0 && y != 0) || (x == 0 && y == 0)) continue; //cross check and self-check disabled

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
                checkList[0].isMatched = true;
                matchList.Add(checkList[0]);
            }
            

            foreach(GridElement element in alignedGridElements)
            {
                element.isMatched = matchStatus;
                if(matchStatus) matchList.Add(element);
                if(!element.isChecked) 
                {
                    allCheckedElements.Add(element);
                    checkList.Add(element);
                }
                
            }

            checkList.Remove(checkList[0]);
        }

        foreach(GridElement element in matchList)
        {
            element.SetMarkStatus(false);
            element.isChecked = false;
            element.isMatched = false;
        }

        foreach(GridElement element in allCheckedElements)
        {
            element.isChecked = false;
        }

        if(matchList.Count > 0) matchCount++;
        uiController.SetMatchCount(matchCount);

    }


    private void ReloadGrid()
    {
        int sizeInput = uiController.GetGridSize();
        gridController.CreateGrid(sizeInput);
        matchCount = 0;
        uiController.SetMatchCount(matchCount);
    }


}
