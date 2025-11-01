using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ZombieState 
{   Move,
    Eat,
    Die,
    Pause

}
public class ZombieControl : MonoBehaviour
{
    ZombieState zombieState=ZombieState.Move;
   private Rigidbody2D rb;
    public float speed = 0.5f;
    public int health = 100;
    public int attackRange = 50;
    public float attackRate = 1f;
    public float timeSinceLastAttack = 0f;
    GameObject plant;
    bool isPlantInFront = false;
    public int currentHealth;
    public GameObject Hard;
    Animator anim;
    bool isGameOver;
    void Start()
    {
        currentHealth = health;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        switch (zombieState) { 
            case ZombieState.Move:
                Move();
                CheckForPlant();
                break;
            case ZombieState.Eat:
                AttackPlant();
                break;
         
        }
        
    }
    
    private void CheckForPlant()
    {
        // 检查前方是否有植物
        Vector2 direction = Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f);
        
        // 只有在没有正在攻击的植物时才检查新植物
        if (plant == null && hit.collider != null && hit.collider.GetComponent<plant>() != null)
        {
            anim.SetBool("HasPlant", true);
            zombieState = ZombieState.Eat;
            plant = hit.collider.gameObject;
            isPlantInFront = true;
            Debug.Log("Plant detected in front");
        }
    }
    
    private void AttackPlant()
    {
        if (plant != null)
        {
            timeSinceLastAttack += Time.deltaTime;
            if (timeSinceLastAttack >= attackRate)
            {
                Debug.Log("Attacking plant");
                plant.GetComponent<plant>().Attacked(attackRange);
                timeSinceLastAttack = 0f;
            }
        }
        else
        {
            // 植物已被摧毁，恢复移动状态
            anim.SetBool("HasPlant", false);
            zombieState = ZombieState.Move;
            isPlantInFront = false;
        }
    }
    
    private void Move()
    {
        rb.MovePosition(rb.position + Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // 只有在移动状态且没有攻击目标时才设置攻击目标
        if (zombieState == ZombieState.Move && plant == null && collider.GetComponent<plant>() != null)
        {
            anim.SetBool("HasPlant", true);
            zombieState = ZombieState.Eat;
            plant = collider.gameObject;
            isPlantInFront = true;
            Debug.Log("Collision with plant");
        }
        else if (collider.gameObject.tag == "House")
        {
            GameManage.instance.GameOverFail();
            Pause();
            anim.SetBool("HasPlant", true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collider)
    {
        // 只有当前攻击的植物离开时才改变状态
        if (collider.GetComponent<plant>() != null && collider.gameObject == plant)
        {
            anim.SetBool("HasPlant", false);
            zombieState = ZombieState.Move;
            isPlantInFront = false;
            plant = null;
        }
    }
    
    public void Pause() { 
    zombieState = ZombieState.Pause;
    gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
    
    public void TakeDamage(int damage) {
        currentHealth -= damage;
        anim.SetFloat("HP", currentHealth*1f / health);
        if (currentHealth  <= 1)
        {
          GameObject go=  GameObject.Instantiate(Hard, transform.position, Quaternion.identity);
            zombieState = ZombieState.Die;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(gameObject, 5f);
            Destroy(go, 2f);
        }
    }

    private void OnDestroy()
    {
        // 当僵尸真正被销毁时，通知 ZombieManage 减少计数
        if (ZombieManage.instance != null)
        {
            ZombieManage.instance.NotifyZombieDeath();
        }
    }
}