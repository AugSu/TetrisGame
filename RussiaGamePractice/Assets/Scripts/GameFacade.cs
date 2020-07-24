using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Canvas下面
/// </summary>
public class GameFacade : MonoBehaviour
{
    public MainMenuState MainMenuState { get; private set; }
    public PlayState PlayState { get; private set; }
    public Model Model { get; private set; }
    public FSMSystem Fsm { get; private set; }
    public AudioManager AudioManager { get; private set; }
    public CameraController cameraController { get; private set; }
    public GameOverState gameOverState { get; private set; }
    
    [HideInInspector]
    public View view;
    [HideInInspector]
    public AudioSource audioSource;

    #region 单例
    private static GameFacade _instance;
    public static GameFacade Instance
    {
        get
        {
            return _instance;
        }
    }
    #endregion

    private void Awake()
    {
        Init();
        cameraController.Awake();
        AudioManager.Awake();
    }


    private void Init()
    {
        _instance = this;
        Fsm = new FSMSystem();
        MainMenuState = new MainMenuState();
        PlayState = new PlayState();
        Model = new Model();
        AudioManager = new AudioManager();
        view = GetComponent<View>();
        cameraController = new CameraController();
        audioSource = GetComponent<AudioSource>();
        gameOverState = new GameOverState();

        gameOverState.Init(this);
        MainMenuState.Init(this);
        PlayState.Init(this);
        Model.Init(this);
    }


}
