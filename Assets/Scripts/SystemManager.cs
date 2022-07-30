using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    [SerializeField] private UIController uiController;
    [SerializeField] private GridController gridController;


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
        if (hitInfo)
        {
            hitInfo.transform.GetComponent<GridElement>().SetMarkStatus(true);
        }
    }


    private void ReloadGrid()
    {
        int sizeInput = uiController.GetGridSize();
        gridController.CreateGrid(sizeInput);
    }


}
