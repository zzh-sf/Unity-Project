using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class SunManager : MonoBehaviour
{
    public static SunManager instance { get; private set; } //单例模式
    public GameObject sun;
    public TMP_Text _sun;
      [SerializeField]
    [Range(0,10000)]private int sunPoint; //阳光数量
    public float proudceTime;
    private float produceTimer;
    private Vector3 sunPos;
    public bool isStartProduce;
    
    public int _sunPoint 
    { 
        get { return sunPoint; } 
    } //公开获得阳光数量的方法

    
    void Start()
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
         if (_sun != null)
        {
            _sun.text = sunPoint.ToString();
        }
    }
    
    void Update()
    {
        // 在每帧更新UI文本显示，确保_sun不为null再访问其属性
        if (_sun != null)
        {
            _sun.text = sunPoint.ToString();
        }
        if (isStartProduce)
        {
            ProduceSun();
        }
    }
    public int SubSun(int SunSpend) 
    {
        sunPoint -= SunSpend;
        return sunPoint;
    }
    public int AddSun(int SunAdd) 
    {
        sunPoint += SunAdd;
        return sunPoint;
    }
    public void ProduceSun() { 
        isStartProduce = true;
    produceTimer+=Time.deltaTime;
        if (produceTimer >= proudceTime)
        {
            produceTimer = 0;
            Vector3 position = new Vector3(Random.Range(-5, 6.5f), 6.2f, -1);
            GameObject go= GameObject.Instantiate(sun, position, Quaternion.identity);
            position.y = Random.Range(-3, 4f);
            go.GetComponent<Sun>().LinearTo(position);
        }
    }
   public void StopProduce() 
    {
        isStartProduce = false;
    }
  
}