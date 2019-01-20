using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private List<Player> playerList = new List<Player>();
    ControllerSetUp controllerSetUp = new ControllerSetUp();
    private bool ready = false;
    public float gameCountdown;
    private float currentGameCountdown;
    private Text titleText;
    private Text roundStartsIn;
    private Text variableText;
    public float roundTime;
    private float currentRoundTime;
    private bool endGame;
    public float endWaitTime;
    private float currentEndWaitTime;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Player player in GameObject.FindObjectsOfType<Player>())
        {
            playerList.Add(player);
        }
        titleText = GameObject.Find("TitleText").GetComponent<Text>();
        variableText = GameObject.Find("VariableText").GetComponent<Text>();
        roundStartsIn = GameObject.Find("RoundStartsIn").GetComponent<Text>();
        controllerSetUp.Start();
        currentGameCountdown = gameCountdown;
        currentRoundTime = roundTime;
        roundStartsIn.text = "";
        ready = false;
        endGame = false;
        currentEndWaitTime = endWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(!endGame)
        {
            controllerSetUp.Update(playerList, GetComponent<InputManager>());
            if (!ready && controllerSetUp.GetReady())
            {
                currentGameCountdown -= Time.deltaTime;
                roundStartsIn.text = "Game Starts In";
                titleText.text = "";
                variableText.text = Mathf.CeilToInt(currentGameCountdown).ToString();
                if (currentGameCountdown <= 0)
                {
                    foreach (Player player in playerList)
                    {
                        player.SetReady(true);
                    }
                    ready = true;
                    roundStartsIn.text = "";
                    titleText.text = "";
                    variableText.text = "";
                }
            }

            if (currentRoundTime <= 0)
            {

                bool allReturned = false;
                foreach (CraneControlScript crane in GameObject.FindObjectsOfType<CraneControlScript>())
                {
                    crane.Return();
                }
                foreach (CraneControlScript crane in GameObject.FindObjectsOfType<CraneControlScript>())
                {
                    if((crane.GetHome() - crane.transform.position).magnitude <= 0.1)
                    {
                        allReturned = true;
                    }
                    else
                    {
                        allReturned = false;
                        break;
                    }
                }
                if (allReturned)
                {
                    currentEndWaitTime -= Time.deltaTime;
                    if(currentEndWaitTime <=0)
                    {
                        GameObject.FindObjectOfType<EndGame>().StartEndGame();
                        endGame = true;
                    }
                }
            }

            else if (ready)
            {
                currentRoundTime -= Time.deltaTime;
                if (currentRoundTime < 0)
                {
                    currentRoundTime = 0;
                }
                titleText.text = "Time Remaining: " + Mathf.FloorToInt(currentRoundTime / 60).ToString("00") + ":" + Mathf.CeilToInt(currentRoundTime % 60).ToString("00");
            }
        }    
    }

    public bool EndGame()
    {
        if(currentRoundTime <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
