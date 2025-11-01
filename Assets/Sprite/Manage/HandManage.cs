using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using UnityEngine;

public class HandManage : MonoBehaviour
{
    public static HandManage instance;//单例模式
    public List<plant> plants;//植物预制体列表
    private plant currentPlant;//当前选中的植物
    // 可选的卡槽位置（在场景中指定，用于放回失败的植物）
    public Transform cardSlot;
    // 记录当前植物的初始（卡槽）位置，用作放置失败时的回退位置
    private Vector3 spawnPosition;
    // 记录上一次成功放置的格子（可选，用于扩展行为）
    private Cell lastPlacedCell;
    //TODO: 增加植物的购买功能
    //TODO:可以点击卡槽建造预制体，跟随鼠标移动
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    private void Update()
    {
        FollowMouse();
    }
    
    public bool BuyPlant(PlantType plantType)
    {
        if (currentPlant != null) 
        {
            return false;
        }
        plant plantPrefab = GetPlantPrefab(plantType);
        if (plantPrefab == null)
        {
            Debug.LogError($"Plant prefab for type {plantType} not found!");
            return false;
        }
        // 在卡槽位置或原位置生成该预制体，便于回退
        if (cardSlot != null)
        {
            currentPlant = GameObject.Instantiate(plantPrefab, cardSlot.position, Quaternion.identity);
            spawnPosition = cardSlot.position;
        }
        else
        {
            currentPlant = GameObject.Instantiate(plantPrefab);
            spawnPosition = currentPlant.transform.position;
        }
        // 设置植物类型
        currentPlant.plantType = plantType;
        return true;
    }

    private plant GetPlantPrefab(PlantType plantType)
    {
        foreach (var plant in plants)
        {
            if (plant.plantType == plantType)
            {
                return plant;
            }
        }
        return null;
    }
    private void FollowMouse()
    {
        if (currentPlant == null)
        {
            return;
        }
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPlant.transform.position=new Vector3(mouseWorldPosition.x,mouseWorldPosition.y,0);
    }
    public bool OnCellClick(Cell cell) {
        if (currentPlant == null) { 
         return false; }
        bool isSuccess = cell.AddPlant(currentPlant);
        if (isSuccess) {
            // 只有在成功放置到格子后才将植物激活并移除 currentPlant 引用
            currentPlant.state = PlantState.Enable;
            // 将植物放到格子下作为子对象，并固定位置
            currentPlant.transform.SetParent(cell.transform);
            currentPlant.transform.position = cell.transform.position;
            lastPlacedCell = cell;
            currentPlant = null;
        } else {
            // 放置失败（例如格子已被占用），把植物移回卡槽位置或初始位置并取消拖拽
            currentPlant.state = PlantState.Disable;
            currentPlant.transform.SetParent(null);
            currentPlant.transform.position = spawnPosition;
            // 取消跟随鼠标：清空 currentPlant 引用
            currentPlant = null;
        }
        return isSuccess;
     
    }
}