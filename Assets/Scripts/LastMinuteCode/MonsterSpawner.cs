using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] Transform player;

    List<MonsterItem> monsterItems = new List<MonsterItem>();

    [SerializeField] GameObject monsterItemPrefab;

    [SerializeField] Vector2 monsterSpawnMinMax = new Vector2(7, 15);

    [SerializeField] int maxMonsters = 30;
    [SerializeField] int maxSpawnAtOnce = 6;

    [SerializeField] float timeDivision = 30;

    [SerializeField] float spawnInterval = 10;
    float currentInterval;

    private void Start()
    {
        currentInterval = spawnInterval;
    }

    private void Update()
    {
        if (GameController.instance.isGameActive)
        {
            if (currentInterval < 0)
            {
                currentInterval = spawnInterval;

                for (int i = 0; i < Random.Range(1, maxSpawnAtOnce); i++) SpawnMonster();
            }
            else currentInterval -= Time.deltaTime;
        }
    }

    void SpawnMonster()
    {
        //find open monster
        foreach (var item in monsterItems)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                item.SetMonster(GameController.instance.gameTimer / timeDivision, Random.Range(0, 4) == 1, player, GetRandomStartPosition());
                return;
            }
        }

        if(monsterItems.Count < maxMonsters) 
        { 
            MonsterItem monsterItem = Instantiate(monsterItemPrefab, transform).GetComponent<MonsterItem>();
            monsterItem.SetMonster(GameController.instance.gameTimer / timeDivision, Random.Range(0, 4) == 1, player, GetRandomStartPosition());
            monsterItems.Add(monsterItem);
        }
    }

    Vector3 GetRandomStartPosition()
    {
        return new Vector3(
            Random.Range(monsterSpawnMinMax.x, monsterSpawnMinMax.y) * (Random.Range(0,2)==1?1:-1),
            Random.Range(monsterSpawnMinMax.x, monsterSpawnMinMax.y) * (Random.Range(0, 2) == 1 ? 1 : -1)
        ) + player.position;
    }
}
