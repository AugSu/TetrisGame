using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : FSMState
{
    private GameFacade gameFacade;
    public void Init(GameFacade gameFacade)
    {
        AddTransition(Transition.GameOver, StateID.GameOverState);
        this.gameFacade = gameFacade;
        stateID = StateID.PlayState;
        gameFacade.Fsm.AddState(this);
    }

    //进入Game界面
    public override void DoBeforeEntering()
    {
        GameManager.Instance.IsGameStart();
        gameFacade.view.ShowGameUI();
    }


}
