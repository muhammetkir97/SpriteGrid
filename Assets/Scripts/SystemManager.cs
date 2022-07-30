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
        
    }


    private void ReloadGrid()
    {
        int sizeInput = uiController.GetGridSize();
        gridController.CreateGrid(sizeInput);
    }


}
