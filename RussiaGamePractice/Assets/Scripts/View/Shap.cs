using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shap : MonoBehaviour
{
    //设置Block的颜色
    private GameFacade facade;
    private GameManager gameManager;
    private float time = 0f;
    private float timer = 0.8f;
    private bool isPause;
    private int speedUpNum=8;
    private bool isSpeedUp = false;
    private Transform pivot;

    private void Update()
    {
        //停止
        if (isPause)
        {
            return;
        }

        if (time<timer)
        {
            time += Time.deltaTime;
        }
        else
        {
            //InputControl放在FallDown里0.8秒执行一次;只有一帧的时间 不行;
            FallDown();
            time = 0f;
        }
        InPutControl();
    }

    private void Awake()
    {
        facade = GameFacade.Instance;
        gameManager = GameManager.Instance;
        isPause = false;
        pivot = transform.Find("Pivot");
    }
    public void SetColor(Color color)
    {
        foreach (Transform child in transform)
        {
            if (child.tag!="Block")
            {
                continue;
            }
            child.GetComponent<SpriteRenderer>().color = color;
        }
    }

    //block向下移动
    public void FallDown()
    {
        //先移动Block
        Vector3 position = transform.position;
        position.y -= 1;
        transform.position = position;

        //位置不合法要停下来
        if (!facade.Model.IsValidPosition(transform))
        {
            position.y += 1;
            transform.position = position;
            isPause = true;

            //将落下来的方块存储起来;
            facade.Model.SaveBlockPosition(transform);

            //检查地图是否有满的行
            facade.Model.CheckMap();

            //停下来的处理
            gameManager.FallDownHandle();
        }
        facade.AudioManager.PlayDropClip();
    }

    public void InPutControl()
    {
        //按下A键 向左移动
        int xDistance = 0;
        if (Input.GetKeyDown(KeyCode.A))
        {
            xDistance = -1;
        }
        //向右移动
        if (Input.GetKeyDown(KeyCode.D))
        {
            xDistance = 1;
        }
        if (xDistance!=0)
        {
            //左右移动
            Vector3 position = transform.position;
            position.x += xDistance;
            transform.position = position;
            //检查移动后是否合法
            if (!facade.Model.IsValidPosition(transform))
            {
                position.x -= xDistance;
                transform.position = position;
            }
            else
            {
                facade.AudioManager.PlayMoveClip();
            }
        }
        //翻转;
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.RotateAround(pivot.position, Vector3.forward, -90f);
            if (!facade.Model.IsValidPosition(transform))
            {
                transform.RotateAround(pivot.position, Vector3.forward, 90f);
            }
            else
            {
                facade.AudioManager.PlayMoveClip();
            }
        }
        //按下加速键
        if (Input.GetKeyDown(KeyCode.S)&&!isSpeedUp)
        {
            facade.AudioManager.PlayMoveClip();
            isSpeedUp = true;
            timer /= speedUpNum;
        }
       
    }
    
}
