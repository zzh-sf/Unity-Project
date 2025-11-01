using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlanCanMakeSun : MonoBehaviour
{
    public int AddSunCount;//可以增加的阳光数量
    [Range(0,100)]public float canMakeSunTime;//可以制造阳光的时间
    [Range(0,100)]public float canMakeSunTimer;//制作阳光计数器
    public bool canMakeSun=false;//是否可以制造阳光
    Animator animator;
    public GameObject sunPrefab;//阳光预制体
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (canMakeSun)
        {
            animator.SetTrigger("isCanMake");
            ProduceSun();
            canMakeSunTimer = 0;
            canMakeSun = false;
        }
        else 
        {
            canMakeSunTimer += Time.deltaTime;
            if (canMakeSunTimer >= canMakeSunTime)
            {
                canMakeSun = true;
            }
        }
    }
    public void ProduceSun() {
        // 保存新创建的阳光实例的引用
        GameObject newSun = GameObject.Instantiate(sunPrefab, transform.position, Quaternion.identity);
        float distance=Random.Range(0, 2);
        distance=Random.Range(0, 2)<1?-distance:distance;
        Vector3 Position = transform.position;
        Position.x+=distance;
        // 调用新创建的阳光实例上的JumpTo方法，而不是预制体上的
        newSun.GetComponent<Sun>().JumpTo(Position);
    }
}