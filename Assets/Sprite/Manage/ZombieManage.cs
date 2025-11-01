using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SpawState { 
NotStart,
Spawning,
End
}
public class ZombieManage : MonoBehaviour
{
 public static ZombieManage instance;
    private SpawState spawState;
    // 当前场上活着的僵尸数量
    public int aliveZombies = 0;
    public Transform[] spawnPoints;
    public GameObject zombiePrefab;
    private Coroutine spawnCoroutine;
 private void Awake()
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
    void Start()
    {
        //StartSpawning();

    }
    public void StartSpawning() { 
    spawState = SpawState.Spawning;
    spawnCoroutine = StartCoroutine(SpawZombie());
 }
   public void StopSpawning() { 
        spawState = SpawState.End;
        if (spawnCoroutine != null) {
            StopCoroutine(spawnCoroutine);
        }
    }
    IEnumerator SpawZombie() {
        for (int i = 0; i < 5; i++) { 
            SpawnARandonZombie();
            yield return new WaitForSeconds(2);
        }
        yield return new WaitForSeconds(2);
        for (int i = 0; i < 10; i++)
        {
            SpawnARandonZombie();
            yield return new WaitForSeconds(2);
        }
        yield return new WaitForSeconds(2);
        for (int i = 0; i < 20; i++)
        {
            SpawnARandonZombie();
            yield return new WaitForSeconds(2);
        }
        // 所有预定的僵尸已生成，标记为结束状态
        spawState = SpawState.End;
        // 如果此时场上已无僵尸，触发胜利
        if (aliveZombies == 0)
        {
            if (GameManage.instance != null)
            {
                GameManage.instance.GameOverSuccec();
            }
        }
        
    }
    private void SpawnARandonZombie() { 
        // 检查spawnPoints数组和其中的元素是否有效
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return;
        }
        
        int index = Random.Range(0, spawnPoints.Length);
        
        // 检查选中的spawnPoint是否有效
        if (spawnPoints[index] == null)
        {
            Debug.LogError("Spawn point at index " + index + " is missing!");
            return;
        }
        
        GameObject zombie = Instantiate(zombiePrefab, spawnPoints[index].position, Quaternion.identity);
    // 生成后增加存活计数，Zombie 在销毁时会通知减少计数
    aliveZombies++;
        SpriteRenderer zombieRenderer = zombie.GetComponent<SpriteRenderer>();
        
        // 设置僵尸的渲染顺序，使用固定值而不是从生成点获取
        if (zombieRenderer != null) {
            zombieRenderer.sortingOrder = spawnPoints[index].gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1; // 设置一个合适的渲染顺序
        } else {
            Debug.LogWarning("Zombie prefab does not have a SpriteRenderer component!");
        }
    }

    // 由僵尸在销毁时调用，减少活着的僵尸计数并在满足胜利条件时触发胜利流程
    public void NotifyZombieDeath()
    {
        aliveZombies = Mathf.Max(0, aliveZombies - 1);
        // 如果已经不再生成新的僵尸且场上已无僵尸，则判定胜利
        if (spawState == SpawState.End && aliveZombies == 0)
        {
            if (GameManage.instance != null)
            {
                GameManage.instance.GameOverSuccec();
            }
        }
    }
}