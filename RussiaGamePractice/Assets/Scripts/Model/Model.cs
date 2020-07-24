using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏地图
/// </summary>
public class Model
{
    private int score = 0;
    private int highScore;
    private const int ROW = 23;
    private const int COLUMN = 10;
    private const int NORMAL_ROW = 20;

    public int Score
    {
        get
        {
            return score;
        }
    }

    public int HighScore
    {
        get
        {
            return highScore;
        }
        set
        {
            highScore = value;
        }
    }

    private Transform[,] map = new Transform[COLUMN, ROW]; 

    private GameFacade gameFacade;
    public void Init(GameFacade gameFacade)
    {
        this.gameFacade = gameFacade;
    }

    #region block位置判断
    //判断block是否在地图内;
    private bool IsInSideMap(Vector3 position)
    {
        if (position.x >= 0 && position.x < COLUMN && position.y >= 0)
        {
            return true;
        }
        return false;
    }

    public bool IsValidPosition(Transform t)
    {
        foreach (Transform child in t)
        {
            if (child.tag != "Block") continue;
            //检查Block是否在地图内
            Vector2 position = child.position.Round();
            if (IsInSideMap(position) == false) return false;

            //检查当前的位置是否有block
            if (!IsNullPosition(position)) return false;
        }
        return true;
    }

    //当前位置是否为空;
    private bool IsNullPosition(Vector3 position)
    {
        if (map[(int) position.x,(int)position.y]!=null)
        {
            return false;
        }
        return true;
    }
    #endregion

    //存储每一个block的位置信息
    public void SaveBlockPosition(Transform t)
    {
        foreach (Transform child in t)
        {
            if (child.tag != "Block") continue;
            Vector2 position = child.position.Round();
            map[(int)position.x, (int)position.y] = child;
        }
    }

    //检查地图是否有符合消除的条件;
    public void CheckMap()
    {
        int count = 0;
        //检查地图上的每一行
        for (int j = 0; j < ROW; j++)
        {
            //检查每一行是否满了
            if (IsFullRow(j))
            {
                //删除
                DeleteRow(j);
                //移动上面的Blocks 从J+1行开始
                MoveBlocks(j + 1);
                //当前行炸掉 还要从当前行再次检查;
                count++;
                j--;
            }
        }
        if (count>0)
        {
            score += (count * 100);
            if (score>=highScore)
            {
                highScore = score;
                //更新分数
                gameFacade.view.SetScore(score, highScore);
            }
        }
    }

    //移动Blocks
    private void MoveBlocks(int currentRow)
    {
        for (int i = 0; i < COLUMN; i++)
        {
            for (int j = currentRow; j < ROW; j++)
            {
                if (map[i,j]!=null)
                {
                    map[i, j - 1] = map[i, j];
                    map[i, j] = null;
                    map[i, j - 1].position += new Vector3(0, -1, 0);
                }
            }
        }
    }

    //删除行Blocks
    private void DeleteRow(int j)
    {
        for (int i = 0; i < COLUMN; i++)
        {
            //先删除当前位置的物体 然后再讲当前的物体置空;
            GameObject.Destroy(map[i, j].gameObject);
            map[i, j] = null;
        }
        gameFacade.AudioManager.PlayClearClip();
    }

    //检查每一行
    private bool IsFullRow(int j)
    {
        for (int i = 0; i < COLUMN; i++)
        {
            if (map[i,j]==null)
            {
                return false;
            }
        }
        return true;
    }

    //判断20-23行之间是否有为空
    public bool IsGameOver()
    {
        for (int i = 0; i < COLUMN; i++)
        {
            for (int j = NORMAL_ROW; j <ROW; j++)
            {
                if (map[i,j]!=null)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
