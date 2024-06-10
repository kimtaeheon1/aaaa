using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject characterPrefab;  // ���� ��� ĳ���� ������
    public int maxCharacters = 5;       // �ִ� ĳ���� ��
    private List<GameObject> characters = new List<GameObject>();  // ������ ĳ���� ���
    private float[] spawnTimes = { 0f, 15f, 30f, 45f, 60f};        // ���� �ð� �迭

    private Vector3[] spawnPositions = new Vector3[]
    {
        new Vector3(20f, 0.55f, 1f),
        new Vector3(-20f, 0.55f, 1f),
        new Vector3(0f, 0.55f, -20f),
        new Vector3(0f, 0.55f, 20f)
    };

    void Start()
    {
        StartCoroutine(SpawnCharacter());
    }

    IEnumerator SpawnCharacter()
    {
        for (int i = 0; i < spawnTimes.Length && characters.Count < maxCharacters; i++)
        {
            yield return new WaitForSeconds(spawnTimes[i] - (i > 0 ? spawnTimes[i - 1] : 0));

            if (characters.Count < maxCharacters && i < spawnPositions.Length)
            {
                GameObject newCharacter = Instantiate(characterPrefab, spawnPositions[i], Quaternion.identity);
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