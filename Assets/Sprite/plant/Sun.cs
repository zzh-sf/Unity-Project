using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sun : MonoBehaviour
{
    float moveDuration = 1.0f;
    int sunCount = 50;

    
    void Start()
    {
        // 确保对象有Collider2D组件用于鼠标检测
        if (GetComponent<Collider2D>() == null)
        {
            gameObject.AddComponent<BoxCollider2D>();
        }
    }
    public void LinearTo(Vector3 position) { 
    transform.DOMove(position, moveDuration).SetEase(Ease.Linear);
    }
    public void JumpTo(Vector3 position)
    {
        Vector3 startPosition = transform.position;

        // 计算跳跃高度，基于起点和终点之间的水平距离，而不是直线距离
        float horizontalDistance = Mathf.Abs(position.x - startPosition.x);
        float jumpHeight = horizontalDistance * 0.3f; // 降低跳跃高度系数
        jumpHeight = Mathf.Clamp(jumpHeight, 0.5f, 3f); // 限制跳跃高度在合理范围内

        // 设置目标位置的z轴为-1
        Vector3 targetPosition = new Vector3(position.x, position.y, -1);

        // 使用DOJump创建抛物线效果
        transform.DOJump(targetPosition, jumpHeight, 1, moveDuration)
                 .SetEase(Ease.OutQuad);
    }
    
    public void OnMouseDown()
    {
        // 确保SunManager实例存在
        if (SunManager.instance != null)
        {
            // 获取文本框的位置
            RectTransform sunTextRect = SunManager.instance._sun.GetComponent<RectTransform>();
            Vector3 textPosition = sunTextRect.position;
            
            // 将屏幕坐标转换为世界坐标
            Vector3 worldTextPosition = Camera.main.ScreenToWorldPoint(textPosition);
            worldTextPosition.z = -1; // 设置z轴坐标
            
            // 让阳光移动到文本位置后再销毁
            float horizontalDistance = Mathf.Abs(worldTextPosition.x - transform.position.x);
            float jumpHeight = horizontalDistance * 0.3f;
            jumpHeight = Mathf.Clamp(jumpHeight, 0.5f, 3f);
            
            transform.DOMove( worldTextPosition, moveDuration)
                     .OnComplete(() => {
                         // 移动完成后增加阳光数量并销毁自身
                         SunManager.instance.AddSun(sunCount);
                         Destroy(gameObject);
                     });
        }
        else
        {
            // 如果SunManager不存在，直接销毁并增加阳光
            if (SunManager.instance != null)
            {
                SunManager.instance.AddSun(sunCount);
            }
            Destroy(gameObject);
        }
    }
}