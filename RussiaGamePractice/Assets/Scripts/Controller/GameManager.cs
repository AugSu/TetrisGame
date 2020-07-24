using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameFacade facade;
    private FSMSystem fsm;
    private Transform blockHolder;
    private bool isPause;

    private Shap currentBlock = null;
    public Shap[]shaps;
    public Color[] colors;
    private int blocksNum;
    private int colorNum;


    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        facade = GameFacade.Instance;
        _instance = this;
        facade.Model.HighScore = PlayerPrefs.GetInt("highScore",0);
        facade.view.SetScore(0, facade.Model.HighScore);
    }

    private void Start()
    {
        MakeFSM();
        blocksNum = shaps.Length;
        colorNum = colors.Length;
        isPause = true;
        blockHolder = transform.Find("BlockHolder");

    }
    private void Update()
    {
      
        if (isPause)
        {
            return;
        }
        if (currentBlock==null)
        {
            SpawnBlock();
        }
    }

    private void MakeFSM()
    {
        fsm = facade.Fsm;
        fsm.SetCurrentState(facade.MainMenuState);
    }

    //产生方块;
    public void SpawnBlock()
    {
        currentBlock = Instantiate(shaps[Random.Range(0, blocksNum)]);
        currentBlock.SetColor(colors[Random.Range(0, colorNum)]);
        currentBlock.transform.SetParent(blockHolder);
        Debug.Log("进入");
    }

    //游戏暂停
    public void IsGamePause()
    {
        isPause = true;
    }

    //游戏开始
    public void IsGameStart()
    {
        isPause = false;
    }

    //物体落下后的处理
    public void FallDownHandle()
    {
        currentBlock = null;
        if (IsGameOver())
        {
            fsm.PerformTransition(Transition.GameOver);
            PlayerPrefs.SetInt("highScore", facade.Model.HighScore);
            isPause = true;
        }
    }
    
    private bool IsGameOver()
    {
        if (facade.Model.IsGameOver())
        {
            return true;        
        }
        return false;
    }


}
