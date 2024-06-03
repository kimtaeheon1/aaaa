using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject characterPrefab;  // 총을 쏘는 캐릭터 프리팹
    public int maxCharacters = 5;       // 최대 캐릭터 수
    private List<GameObject> characters = new List<GameObject>();  // 스폰된 캐릭터 목록
    private float[] spawnTimes = { 0f, 15f, 30f, 60f, 120f };        // 스폰 시간 배열

    void Start()
    {
        StartCoroutine(SpawnCharacter());
    }

    IEnumerator SpawnCharacter()
    {
        for (int i = 0; i < spawnTimes.Length && characters.Count < maxCharacters; i++)
        {
            yield return new WaitForSeconds(spawnTimes[i] - (i > 0 ? spawnTimes[i - 1] : 0));

            if (characters.Count < maxCharacters)
            {
                Vector3 randomPosition = new Vector3(Random.Range(-20f, 20f), 1f, Random.Range(-20f, 20f));
                GameObject newCharacter = Instantiate(characterPrefab, randomPosition, Quaternion.identity);
                characters.Add(newCharacter);
            }
        }
    }
}

public class ShootingCharacter : MonoBehaviour
{
    public GameObject bulletPrefab;     // 총알 프리팹
    public Transform firePoint;         // 총알이 발사되는 위치
    public float fireInterval = 1f;     // 총알 발사 간격

    void Start()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireInterval);

            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
}