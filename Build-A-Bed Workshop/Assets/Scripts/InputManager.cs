using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class InputManager : MonoBehaviour
{
    public enum Button
    {
        None,
        FaceButtonBottom,
        FaceButtonLeft,
        FaceButtonTop,
        FaceButtonRight,
        DPadDown,
        DPadLeft,
        DPadUp,
        DPadRight,
        LeftBumper,
        RightBumper,
        LeftStickButton,
        RightStickButton,
        LeftMenuButton,
        RightMenuButton
    }
    public enum Axis
    {
        None,
        LeftStickHorizontal,
        LeftStickVertical,
        RightStickHorizontal,
        RightStickVertical,
        LeftTrigger,
        RightTrigger
    }

    [System.Serializable]
    public class ButtonInput
    {
        public string inputName;
        public Button button;
    }

    [System.Serializable]
    public class AxisInput
    {
        public string inputName;
        public Axis Axis;
    }

    public List<ButtonInput> buttonInputList = new List<ButtonInput>();
    public List<AxisInput> axisInputList = new List<AxisInput>();


    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
	}

    public bool GetButton(string button, int player)
    {
        if (findButton(button, player) == ButtonState.Pressed)
        {
            return true;
        }
       
        else
        {
            return false;
        }
    }

    public bool GetButtonDown(string button, int player)
    {

        return false;
    }

    public bool GetButtonUp(string button, int player)
    {

        return false;
    }

    public float GetAxis(string axis, int player)
    {
        return findAxis(axis, player);
    }

    private ButtonState findButton(string button, int player)
    {
        PlayerIndex playerIndex = PlayerIndex.One;
        switch(player)
        {
            case 0:
                playerIndex = PlayerIndex.One;
                break;

            case 1:
                playerIndex = PlayerIndex.Two;
                break;

            case 2:
                playerIndex = PlayerIndex.Three;
                break;

            case 3:
                playerIndex = PlayerIndex.Four;
                break;
        }

        GamePadState gamePadState;
        gamePadState = GamePad.GetState(playerIndex);

        ButtonInput checkedInput = new ButtonInput();
        foreach(ButtonInput buttonInput in buttonInputList)
        {
            if(buttonInput.inputName == button)
            {
                checkedInput = buttonInput;
                break;
            }
        }

        switch(checkedInput.button)
        {
            case Button.DPadDown:
                return gamePadState.DPad.Down;

            case Button.DPadLeft:
                return gamePadState.DPad.Left;

            case Button.DPadRight:
                return gamePadState.DPad.Right;

            case Button.DPadUp:
                return gamePadState.DPad.Up;

            case Button.FaceButtonBottom:
                return gamePadState.Buttons.A;

            case Button.FaceButtonLeft:
                return gamePadState.Buttons.X;

            case Button.FaceButtonRight:
                return gamePadState.Buttons.B;

            case Button.FaceButtonTop:
                return gamePadState.Buttons.Y;

            case Button.LeftBumper:
                return gamePadState.Buttons.LeftShoulder;

            case Button.LeftMenuButton:
                return gamePadState.Buttons.Back;

            case Button.LeftStickButton:
                return gamePadState.Buttons.LeftStick;

            case Button.RightBumper:
                return gamePadState.Buttons.RightShoulder;

            case Button.RightMenuButton:
                return gamePadState.Buttons.Start;

            case Button.RightStickButton:
                return gamePadState.Buttons.RightStick;

            case Button.None:
                break;
        }
        
        
        return ButtonState.Released;
    }

    private float findAxis(string axis, int player)
    {
        PlayerIndex playerIndex = PlayerIndex.One;
        switch (player)
        {
            case 0:
                playerIndex = PlayerIndex.One;
                break;

            case 1:
                playerIndex = PlayerIndex.Two;
                break;

            case 2:
                playerIndex = PlayerIndex.Three;
                break;

            case 3:
                playerIndex = PlayerIndex.Four;
                break;
        }

        GamePadState gamePadState;
        gamePadState = GamePad.GetState(playerIndex);

        AxisInput checkedInput = new AxisInput();
        foreach (AxisInput axisInput in axisInputList)
        {
            if (axisInput.inputName == axis)
            {
                checkedInput = axisInput;
                break;
            }
        }

        switch (checkedInput.Axis)
        {
            case Axis.LeftStickHorizontal:
                return gamePadState.ThumbSticks.Left.X;

            case Axis.LeftStickVertical:
                return gamePadState.ThumbSticks.Left.Y;

            case Axis.LeftTrigger:
                return gamePadState.Triggers.Left;

            case Axis.RightStickHorizontal:
                return gamePadState.ThumbSticks.Right.X;

            case Axis.RightStickVertical:
                return gamePadState.ThumbSticks.Right.Y;

            case Axis.RightTrigger:
                return gamePadState.Triggers.Right;

            case Axis.None:
                break;

        }

        return 0.0f;
    }
}

