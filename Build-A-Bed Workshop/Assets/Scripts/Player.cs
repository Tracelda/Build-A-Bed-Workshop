using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerNumber;
    private int controllerNumber = -1;
    private CraneControlScript crane;
    private InputManager input;
    private bool grabDown;
    private bool ready;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        input = GameObject.FindObjectOfType<InputManager>();
        grabDown = false;
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.EndGame())
        {

            if (ready)
            {
                if (crane)
                {
                    if (!crane.GetGrab())
                    {
                        crane.MoveHorizontal(input.GetAxis("MoveHorizontal", controllerNumber));
                        crane.MoveVertical(input.GetAxis("MoveVertical", controllerNumber));
                    }
                    if (input.GetButton("Grab", controllerNumber) && !grabDown)
                    {
                        grabDown = true;
                        crane.Grab();
                    }
                    if (!input.GetButton("Grab", controllerNumber))
                    {
                        grabDown = false;
                    }
                }
            }
        }
    }

    public void SetPlayer(int player, int controller)
    {
        playerNumber = player;
        controllerNumber = controller;
        foreach(CraneControlScript gotCrane in GameObject.FindObjectsOfType<CraneControlScript>())
        {
            if(gotCrane.CraneNumber == playerNumber)
            {
                crane = gotCrane;
                break;
            }
        }
    }

    public bool Configured()
    {
        if (controllerNumber != -1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetReady(bool value)
    {
        ready = value;
    }
}
