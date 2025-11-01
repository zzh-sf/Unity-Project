using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float shootDuration = 2f;
    private float shootTimer;
    public GameObject bulletPrefab;
    public Transform bulletSPos;
    
    void Update()
    {
        EnableUpdate();
    }
    
    void EnableUpdate()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootDuration)
        {
            shootTimer = 0f;
            GameObject bullet = Instantiate(bulletPrefab, bulletSPos.position, Quaternion.identity);
            // 使子弹沿x轴正方向飞行
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(5f, 0f);
            // 禁用重力，防止子弹下落
            rb.gravityScale = 0;
            
            // 直接为子弹添加销毁功能
            Destroy(bullet, 5f);
            
            Debug.Log("shoot");
        }
    }
}