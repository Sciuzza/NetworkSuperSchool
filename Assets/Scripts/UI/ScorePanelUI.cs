using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScorePanelUI : MonoBehaviour
{
    private Text redTeamScoreText, blueTeamScoreText;
    private List<GameObject> redTeamGameObjects;
    private List<GameObject> blueTeamGameObjects;
    private GameController gc;

    void Awake()
    {
        gc = FindObjectOfType<GameController>();
    }

    void Start()
    {
      
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            ScoreUpdate();
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void ScoreUpdate()
    {

    }
}
