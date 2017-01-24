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

    public GameObject redTeamUIContainer, blueTeamUIContainer;

    private GameObject[] redTeamNamesArray = new GameObject[8];
    private GameObject[] blueTeamNamesArray = new GameObject[8];
    private GameObject[] redTeamScoreArray = new GameObject[8];
    private GameObject[] blueTeamScoreArray = new GameObject[8];

    private Text[] redTeamNamesTextArray = new Text[8];
    private Text[] blueTeamNamesTextArray = new Text[8];
    private Text[] redTeamScoreTextArray = new Text[8];
    private Text[] blueTeamScoreTextArray = new Text[8];

    public Text redTeamTotalScore;
    public Text blueTeamTotalScore;

    void Awake()
    {
        gc = FindObjectOfType<GameController>();

        for (int i = 0; i < 8; i++)
        {
            redTeamNamesArray[i] = redTeamUIContainer.transform.FindChild("Player" + (i + 1)).gameObject;
            blueTeamNamesArray[i] = blueTeamUIContainer.transform.FindChild("Player" + (i + 1)).gameObject;
            redTeamScoreArray[i] = redTeamUIContainer.transform.FindChild("Score" + (i + 1)).gameObject;
            blueTeamScoreArray[i] = blueTeamUIContainer.transform.FindChild("Score" + (i + 1)).gameObject;
        }

        for (int i = 0; i < 8; i++)
        {
            redTeamNamesTextArray[i] = redTeamNamesArray[i].GetComponent<Text>();
            blueTeamNamesTextArray[i] = blueTeamNamesArray[i].GetComponent<Text>();
            redTeamScoreTextArray[i] = redTeamScoreArray[i].GetComponent<Text>();
            blueTeamScoreTextArray[i] = blueTeamScoreArray[i].GetComponent<Text>();
        }

        for (int i = 0; i < gc.m_RedTeamMembers.Count-1; i++)
        {
            redTeamNamesArray[i].SetActive(true);
            redTeamScoreArray[i].SetActive(true);
        }

        for (int i = 0; i < gc.m_BlueTeamMembers.Count-1; i++)
        {
            blueTeamNamesArray[i].SetActive(true);
            blueTeamScoreArray[i].SetActive(true);
        }
        transform.GetChild(0).gameObject.SetActive(false);
    }

    void Update()
    {
        redTeamTotalScore.text = gc.m_TotalTeamScoreList[0].ToString();
        blueTeamTotalScore.text = gc.m_TotalTeamScoreList[1].ToString();

        for (int i = 0; i < gc.m_BlueTeamMembers.Count-1; i++)
        {
            blueTeamNamesTextArray[i].text = gc.m_BlueTeamMembers[i].playerName;
            blueTeamScoreTextArray[i].text = gc.m_BlueTeamMembers[i].playerPersonalScore.ToString();
        }
        for (int i = 0; i < gc.m_RedTeamMembers.Count-1; i++)
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
