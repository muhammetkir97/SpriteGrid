using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI matchCountText;
    [SerializeField] private Button reloadButton;
    [SerializeField] private TMP_InputField gridSizeInput;

    private int defaultGridSize = 3;
    public UnityAction reloadButtonCallbacks;

    void Start()
    {
        reloadButton.onClick.AddListener(ClickReloadButton);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ClickReloadButton()
    {
        if(reloadButtonCallbacks != null) reloadButtonCallbacks();
    }

    public void SetMatchCount(int count)
    {
        matchCountText.text = $"Match Count: {count}";
    }

    public int GetGridSize()
    {
        int size = defaultGridSize;

        int parsedValue;
        if(int.TryParse(gridSizeInput.text, out parsedValue)) size = parsedValue;

        return size;


    }
}
