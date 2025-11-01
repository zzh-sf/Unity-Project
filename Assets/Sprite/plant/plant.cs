using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlantState
{
    Disable,
    Enable
}

public class plant : MonoBehaviour
{
    public PlantType plantType;
    public PlantState state=PlantState.Disable;
    Animator anim;
    private BoxCollider2D boxCollider;
    public int Hp = 100;
    void Start()
    {
        if (GetComponent<BoxCollider2D>() == null) { 
        gameObject.AddComponent<BoxCollider2D>();
        }
        anim=GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        // 设置为触发器以避免物理碰撞排斥
        if (boxCollider != null)
        {
            boxCollider.isTrigger = true;
        }
    }
    
    void Update()
    {
        switch (state)
        { 
            case PlantState.Disable:
             TransitionToDisable();
                break;
            case PlantState.Enable:
               TransitionToEnable();
                break;
        }
    }
    void TransitionToDisable()
    {
        GetComponent<Animator>().enabled = false;
        if (GetComponent<Shoot>() != null) { 
        GetComponent<Shoot>().enabled = false;
        }
        if (GetComponent<PlanCanMakeSun>() != null)
        {
            GetComponent<PlanCanMakeSun>().enabled = false;
        }
        if (boxCollider != null)
            boxCollider.enabled = false;
    }
    void TransitionToEnable()
    {
        GetComponent<Animator>().enabled = true;
        if (GetComponent<Shoot>() != null) { 
        GetComponent<Shoot>().enabled = true;
        }
        if (GetComponent < PlanCanMakeSun >() != null)
        {
            GetComponent<PlanCanMakeSun>().enabled = true;
        }
        if (boxCollider != null)
            boxCollider.enabled = true;
    }
    void Die()
    {
        Destroy(gameObject);
    }
   public void Attacked(int damage) { 
        Hp -= damage;
        if (Hp <= 0) {
            Die();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        ZombieControl zombie = collider.GetComponent<ZombieControl>();
        if (zombie != null)
        {
            Debug.Log("Plant collided with zombie");
        }
    }
}