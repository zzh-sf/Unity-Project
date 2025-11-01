using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaBullet : MonoBehaviour
{
    
    private int damage = 10;
    public GameObject PeaBoomPrefab;
    public void SetDamage(int hurt)
    {
        damage = hurt;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ZombieControl>() != null)
       {
            collision.gameObject.GetComponent<ZombieControl>().TakeDamage(damage);
            GameObject  go = GameObject.Instantiate(PeaBoomPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(go, 1f);
            
        }
    }
}
