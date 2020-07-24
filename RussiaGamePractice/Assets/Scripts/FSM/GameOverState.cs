using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState :FSMState
{
    private GameFacade gameFacade;
    public void Init(GameFacade gameFacade)
    {
        stateID = StateID.GameOverState;
        gameFacade.Fsm.AddState(this);
        this.gameFacade = gameFacade;
    }

    public override void DoBeforeEntering()
    {
        gameFacade.view.ShowGameOverUI(gameFacade.Model.Score);
    }
}
