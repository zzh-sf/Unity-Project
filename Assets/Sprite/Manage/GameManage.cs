using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class GameManage : MonoBehaviour
{
    public static GameManage instance;
    public PrepareUI prepareUI;
    public CardList cardList;
    public FailUI failUI;
    // 可选的成功 UI（如果在场景中挂载，则会在胜利时激活）
    public GameObject successUI;
    private bool isGameOver=false;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        GameStart();
        successUI?.SetActive(false);
    }
    void GameStart()
    {
    Vector3 currentPosition=Camera.main.transform.position;
        Camera.main.transform.DOPath(new Vector3[] { currentPosition, new Vector3(5, 1, -10), currentPosition },
            5f, PathType.Linear).OnComplete(ShowReadyUI);
    }
    void ShowReadyUI() { 
    prepareUI.Show(OnPrepreUIComplete);
    }
    void OnPrepreUIComplete() {
        SunManager.instance.ProduceSun();
        ZombieManage.instance.StartSpawning();
        cardList.ShowCardList();
    }
    public bool GameOverFail()
    {
        if (isGameOver) {
            return false;
        }
        isGameOver = true;
        failUI.Show();
        
        // 通知所有僵尸暂停
        ZombieControl[] zombies = FindObjectsOfType<ZombieControl>();
        foreach (ZombieControl zombie in zombies)
        {
            zombie.Pause();
        }
        // 通知所有僵尸停止生成
        ZombieManage.instance.StopSpawning();
        // 禁止卡槽点击
        cardList.DisableCardList();
        // 停止阳光生产
        SunManager.instance.StopProduce();
        return true;
    }

    public bool GameOverSuccec()
    {
        if (isGameOver)
        {
            return false;
        }
        isGameOver = true;

        // 如果挂载了成功 UI，则激活它
        if (successUI != null)
        {
            successUI.SetActive(true);
        }

        // 通知所有僵尸暂停
        ZombieControl[] zombies = FindObjectsOfType<ZombieControl>();
        foreach (ZombieControl zombie in zombies)
        {
            zombie.Pause();
        }
        // 通知所有僵尸停止生成
        ZombieManage.instance.StopSpawning();
        // 禁止卡槽点击
        cardList.DisableCardList();
        // 停止阳光生产
        SunManager.instance.StopProduce();
        Debug.Log("GameOverSuccec: 游戏胜利流程已执行");
        return true;
    }
    public void GameOverOnClick() { 
    SceneManager.LoadScene("Menu");
    }
}