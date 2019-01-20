using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerSetUp
{
    private bool ready = false;
    private int playerNumber = 1;
    private List<int> usedNumbers = new List<int>();
    private Text titleText;
    private Text variableText;

    public void Start()
    {
        titleText = GameObject.Find("TitleText").GetComponent<Text>();
        variableText = GameObject.Find("VariableText").GetComponent<Text>();
        variableText.text = "";
    }

    public void Update(List<Player> playerList, InputManager inputManager)
    {
        if (!ready)
        {
            if (playerNumber - 1 < playerList.Count)
            {
                if (!playerList[playerNumber - 1].Configured())
                {
                    titleText.text = "Player " + playerNumber + " Press A";
                    for (int i = 0; i < 4; i++)
                    {

                        if (inputManager.GetButton("Grab", i))
                        {
                            if(!usedNumbers.Contains(i))
                            {
                                playerList[playerNumber - 1].SetPlayer(playerNumber, i);
                                playerNumber++;
                                usedNumbers.Add(i);
                                break;
                            }
                        }
                    }
                }
            }
            bool allComplete = true;
            for (int i = 0; i < playerList.Count; i++)
            {
                if (!playerList[i].Configured())
                {
                    allComplete = false;
                    break;
                }
            }
            if (allComplete)
            {
                ready = true;
            }
        }

    }

    public bool GetReady()
    {
        return ready;
    }
}
