using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] private Transform gridParent;
    [SerializeField] private Transform gridElement;
    [SerializeField] private Vector2 gridSpacing = Vector2.zero;

    [Header("Camera")]
    [SerializeField] private Camera gameCamera;

    public int checkCount = 0;
    private int gridSize;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateGrid(int size)
    {
        gridSize = size;

        if(gridParent.childCount > 0)
        {
            for(int i = gridParent.childCount-1; i >= 0; i--) Destroy(gridParent.GetChild(i).gameObject);
        }
         

        for(int x=0; x < size; x++)
        {
            for(int y=0; y < size; y++)
            {
                GameObject cloneGridElement = Instantiate(gridElement.gameObject, gridParent);

                float xCoord = x + (x * gridSpacing.x);
                float yCoord = y + (y * gridSpacing.y);
                cloneGridElement.transform.localPosition = new Vector3(xCoord, yCoord);
                cloneGridElement.GetComponent<GridElement>().SetCoordinate(new Vector2(x,y));
                cloneGridElement.GetComponent<GridElement>().SetController(this);
            }
        }

        //place the grid to screen center
        float width = (size - 1) + ((size - 1) * gridSpacing.x);
        float height = (size - 1) + ((size - 1) * gridSpacing.y);
        gridParent.position = new Vector3(-width / 2f, -height / 2f,0);

        
        if(gameCamera.aspect > 1)
        {
            gameCamera.orthographicSize = (width + 1) * (gameCamera.aspect) * 0.5f; //will not fill screen on landscape orientation
        }
        else
        {
            gameCamera.orthographicSize = (width + 1) * (1 / gameCamera.aspect) * 0.5f;
        }

        
 
    }

    public GridElement GetGridElement(Vector2 coord)
    {
        int childNumber = (int) (coord.x * gridSize + coord.y);
        
        GridElement selectedElement = null;
        if(childNumber < gridParent.childCount && childNumber >= 0) selectedElement = gridParent.GetChild(childNumber).GetComponent<GridElement>();

        return selectedElement;
    }
}
