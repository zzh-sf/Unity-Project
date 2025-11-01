using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Camera mainCamera;
    private float cameraRightEdge;
    
    void Start()
    {
        mainCamera = Camera.main;
        // 销毁时间限制，防止子弹永远存在
        Destroy(gameObject, 10f);
    }
    
    void Update()
    {
        // 检查子弹是否超出摄像机视野范围
        if (mainCamera != null)
        {
            Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);
            // 如果子弹的x坐标小于0或大于1，说明已经超出摄像机视野
            if (screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1)
            {
                Destroy(gameObject);
            }
        }
    }
}