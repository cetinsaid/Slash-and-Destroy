using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diffuculty : MonoBehaviour
{
    private Button button;

    private GameManager gameManager;

    public int difficultyLevel;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(setDifficulty);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setDifficulty()
    {
        gameManager.StartGame(difficultyLevel);
        Debug.Log(button.gameObject.name + " was clicked.");
    }
}
