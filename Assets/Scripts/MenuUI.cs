using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] GameObject menuUI;
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject spawnerObject;
    [SerializeField] Dropdown dropdown;
    [SerializeField] Button start; 

    private void Start()
    {  
        start.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        menuUI.SetActive(false);
        gameUI.SetActive(true);
        CreateMap();
    }

    private void CreateMap()
    {
        int mapSize = dropdown.value;
        string optionText = dropdown.options[dropdown.value].text;

        switch (optionText)
        {
            case "3 X 3":
                mapSize = 3;
                break;
            case "4 X 4":
                mapSize = 4;
                break;
            case "5 X 5":
                mapSize = 5;
                break;
            case "6 X 6":
                mapSize = 6;
                break;
            default:
                mapSize = 3;
                break;
        }
        Spawner spawner = spawnerObject.GetComponent<Spawner>();
       // spawner.GenerateTilemap(mapSize);
    }
}

