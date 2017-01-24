using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class ScorePanelUI : NetworkBehaviour
{
    private GameController gc;

    public GameObject redTeamUIContainer, blueTeamUIContainer;

    public GameObject[] redTeamNamesArray = new GameObject[8];
    public GameObject[] blueTeamNamesArray = new GameObject[8];
    public GameObject[] redTeamScoreArray = new GameObject[8];
    public GameObject[] blueTeamScoreArray = new GameObject[8];

    public Text[] redTeamNamesTextArray = new Text[8];
    public Text[] blueTeamNamesTextArray = new Text[8];
    public Text[] redTeamScoreTextArray = new Text[8];
    public Text[] blueTeamScoreTextArray = new Text[8];

    public Text redTeamTotalScore;
    public Text blueTeamTotalScore;

    void Awake()
    {
        gc = FindObjectOfType<GameController>();
    }
    public override void OnStartLocalPlayer()
    {
   

        for (int i = 0; i < 8; i++)
        {
            redTeamNamesArray[i].SetActive(false);
            redTeamScoreArray[i].SetActive(false);
        }

        for (int i = 0; i < 8; i++)
        {
            if (i > gc.m_BlueTeamMembers.Count)
            {
                blueTeamNamesArray[i].SetActive(false);
                blueTeamScoreArray[i].SetActive(false);
            }
        }
        transform.GetChild(0).gameObject.SetActive(false);
    }

    void Update()
    {
        //redTeamTotalScore.text = gc.m_TotalTeamScoreList[0].ToString();
        //blueTeamTotalScore.text = gc.m_TotalTeamScoreList[1].ToString();

        for (int i = 0; i < gc.m_BlueTeamMembers.Count; i++)
        {
            blueTeamNamesTextArray[i].text = gc.m_BlueTeamMembers[i].playerName;
            blueTeamScoreTextArray[i].text = gc.m_BlueTeamMembers[i].playerPersonalScore.ToString();
        }
        for (int i = 0; i < gc.m_RedTeamMembers.Count; i++)
        {
            redTeamNamesTextArray[i].text = gc.m_RedTeamMembers[i].playerName;
            redTeamScoreTextArray[i].text = gc.m_RedTeamMembers[i].playerPersonalScore.ToString();
        }

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
