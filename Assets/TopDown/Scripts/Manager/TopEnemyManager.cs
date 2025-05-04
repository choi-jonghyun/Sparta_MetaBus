using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopEnemyManager : MonoBehaviour
{
    private Coroutine waveRoutine; // ���� ���� ���� ���̺� �ڷ�ƾ

    [SerializeField]
    private List<GameObject> enemyPrefabs; // ������ �� ������ ����Ʈ

    [SerializeField]
    private List<Rect> spawnAreas; // ���� ������ ���� ����Ʈ

    [SerializeField]
    private Color gizmoColor = new Color(1, 0, 0, 0.3f); // ����� ����

    private List<TopEnemyController> activeEnemies = new List<TopEnemyController>(); // ���� Ȱ��ȭ�� ����

    private bool enemySpawnComplite; // ���� ���̺� ������ �Ϸ�Ǿ����� ����

    [SerializeField] private float timeBetweenSpawns = 0.2f; // ���� �� ���� �� ����
    [SerializeField] private float timeBetweenWaves = 1f; // ���̺� �� ��� �ð�

    TopGameManager gameManager;

    public void Init(TopGameManager gameManager)
    {
        this.gameManager = gameManager;
    }


    // ���̺� ���� (waveCount: ������ �� ��)
    public void StartWave(int waveCount)
    {

        if (waveCount <= 0)
        {
            gameManager.EndOfWave(); // GameManager�� ���̺� ���� �˸�
            return;
        }
        // ���� ���̺갡 ���� ���̸� �ߴ�
        if (waveRoutine != null)
            StopCoroutine(waveRoutine);

        // �� ���̺� ����
        waveRoutine = StartCoroutine(SpawnWave(waveCount));
    }

    // ���� ���� ���� ��� ���̺�/������ ����
    public void StopWave()
    {
        StopAllCoroutines();
    }

    // ������ �� ��ŭ ���� �����ϴ� �ڷ�ƾ
    private IEnumerator SpawnWave(int waveCount)
    {
        enemySpawnComplite = false;
        yield return new WaitForSeconds(timeBetweenWaves);
        for (int i = 0; i < waveCount; i++)
        {
            // ���̺� �� ��� �ð�
            yield return new WaitForSeconds(timeBetweenSpawns);
            SpawnRandomEnemy();
        }

        enemySpawnComplite = true;
    }

    // �� �ϳ��� ���� ��ġ�� ����
    private void SpawnRandomEnemy()
    {
        if (enemyPrefabs.Count == 0 || spawnAreas.Count == 0)
        {
            Debug.LogWarning("Enemy Prefabs �Ǵ� Spawn Areas�� �������� �ʾҽ��ϴ�.");
            return;
        }

        // ������ �� ������ ����
        GameObject randomPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

        // ������ ���� ����
        Rect randomArea = spawnAreas[Random.Range(0, spawnAreas.Count)];

        // Rect ���� ������ ���� ��ġ ���
        Vector2 randomPosition = new Vector2(
            Random.Range(randomArea.xMin, randomArea.xMax),
            Random.Range(randomArea.yMin, randomArea.yMax)
        );

        // �� ���� �� ����Ʈ�� �߰�
        GameObject spawnedEnemy = Instantiate(randomPrefab, new Vector3(randomPosition.x, randomPosition.y), Quaternion.identity);
        TopEnemyController enemyController = spawnedEnemy.GetComponent<TopEnemyController>();
        enemyController.Init(this, gameManager.player.transform);

        activeEnemies.Add(enemyController);
    }


    // ����� �׷� ������ �ð�ȭ (���õ� ��쿡�� ǥ��)
    private void OnDrawGizmosSelected()
    {
        if (spawnAreas == null) return;

        Gizmos.color = gizmoColor;
        foreach (var area in spawnAreas)
        {
            Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
            Vector3 size = new Vector3(area.width, area.height);
            Gizmos.DrawCube(center, size);
        }
    }

    public void RemoveEnemyOnDeath(TopEnemyController enemy)
    {
        activeEnemies.Remove(enemy);
        if (enemySpawnComplite && activeEnemies.Count == 0)
            gameManager.EndOfWave();
    }
}
