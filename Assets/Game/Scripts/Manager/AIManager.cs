using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RedAIState { FirstState, SecondState, ThirdState, Win }
public enum GreenAIState { FirstState, SecondState, ThirdState, Win }
public enum YellowAIState { FirstState, SecondState, ThirdState, Win }
public class AIManager : Singleton<AIManager>
{
    private RedAIState redAIState;
    private GreenAIState greenAIState;
    private YellowAIState yellowAIState;

    public void ChangeRedAIState(RedAIState redAIState)
    {
        this.redAIState = redAIState;       
    }

    public void ChangeGreenAIState(GreenAIState greenAIState)
    {
        this.greenAIState = greenAIState;
    }

    public void ChangeYellowAIState(YellowAIState yellowAIState)
    {
        this.yellowAIState = yellowAIState;
    }

    public bool IsRedState(RedAIState redAIState)
    {
        if (this.redAIState == redAIState)
            return true;
        else
            return false;
    }

    public bool IsGreenState(GreenAIState greenAIState)
    {
        if (this.greenAIState == greenAIState)
            return true;
        else
            return false;
    }

    public bool IsYellowState(YellowAIState yellowAIState)
    {
        if (this.yellowAIState == yellowAIState)
            return true;
        else
            return false;
    }
}
