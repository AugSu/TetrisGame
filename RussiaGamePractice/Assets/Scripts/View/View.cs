using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class View:MonoBehaviour
{
    //开始界面
    private RectTransform logoName;
    private RectTransform mainMenu;
    private Text scoreLabel;
    private Text highScoreLabel;
    private Transform gameOverUI;
    private Text overHighScore;

    //Game界面
    private RectTransform gameUI;



    private void Awake()
    {
        logoName = transform.Find("LogoName") as RectTransform;
        mainMenu = transform.Find("MainMenu") as RectTransform;
        gameUI = transform.Find("GameUI") as RectTransform;
        scoreLabel = gameUI.transform.Find("ScoreLabel/Text").GetComponent<Text>();
        highScoreLabel = gameUI.transform.Find("HighScoreLabel/Text").GetComponent<Text>();
        gameOverUI = transform.Find("GameOverUI");
        overHighScore = gameOverUI.Find("HighLabel").GetComponent<Text>();
    }

    //显示主菜单的界面
    public void ShowMainMenu()
    {
        logoName.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(true);

        logoName.DOAnchorPosY(-186.9f, 0.5f);
        mainMenu.DOAnchorPosY(94f, 0.5f);
    }

    //隐藏主菜单
    public void HideMainMenu()
    {
        logoName.DOAnchorPosY(186.9f, 0.5f)
            .OnComplete(delegate { logoName.gameObject.SetActive(false); });
        mainMenu.DOAnchorPosY(-94f, 0.5f)
            .OnComplete(delegate { mainMenu.gameObject.SetActive(false); });
    }

    //显示游戏菜单
    public void ShowGameUI()
    {
        gameUI.DOAnchorPosY(-103.3f, 1f);
    }

    //隐藏游戏菜单;
    public void HideGameUI()
    {
        gameUI.DOAnchorPosY(270.5f, 0.8f);
    }

    public void SetScore(int score = 0, int highScore = 0)
    {
        Debug.Log("进入");
        scoreLabel.text = score.ToString();
        highScoreLabel.text = highScore.ToString();
    }

    public void ShowGameOverUI(int score)
    {
        overHighScore.text = score.ToString();
        gameOverUI.gameObject.SetActive(true);
    }

}
