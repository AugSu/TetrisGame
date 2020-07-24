using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuState : FSMState
{
    private GameFacade gameFacade;
    private Button startButton;
    //当前状态的初始化
    //设置状态 转换关系;
    public void Init(GameFacade gameFacade)
    {
        stateID = StateID.MainMenuState;
        AddTransition(Transition.OnStartButtonClick, StateID.PlayState);
        this.gameFacade = gameFacade;
        gameFacade.Fsm.AddState(this);

        startButton = gameFacade.view.transform.Find("MainMenu/StartButton").
                        GetComponent<Button>();
        startButton.onClick.AddListener(OnStartButtonClick);
    }
    //进入主菜单界面
    public override void DoBeforeEntering()
    {
        gameFacade.cameraController.ZoomOut();
        gameFacade.view.ShowMainMenu();
    }

    //开始游戏按钮的监听事件;
    public void OnStartButtonClick()
    {
        gameFacade.AudioManager.PlayCursorClip();
        gameFacade.Fsm.PerformTransition(Transition.OnStartButtonClick);
    }

    //离开主菜单界面;
    public override void DoBeforeLeaving()
    {

        gameFacade.cameraController.ZoomIn();
        gameFacade.view.HideMainMenu();
    }
}
