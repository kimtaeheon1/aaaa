using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject characterPrefab;  // ���� ��� ĳ���� ������
    public int maxCharacters = 5;       // �ִ� ĳ���� ��
    private List<GameObject> characters = new List<GameObject>();  // ������ ĳ���� ���
    private float[] spawnTimes = { 0f, 15f, 30f, 60f, 120f };        // ���� �ð� �迭

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
    public GameObject bulletPrefab;     // �Ѿ� ������
    public Transform firePoint;         // �Ѿ��� �߻�Ǵ� ��ġ
    public float fireInterval = 1f;     // �Ѿ� �߻� ����

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