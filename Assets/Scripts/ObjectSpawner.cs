using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private Coin coinPrefab;
    [SerializeField] private GameObject[] obstaclePrefab;
    [SerializeField] private SpikeBall spikeBallPrefab;

    private float coinInterval = 0.25f;

    private float[] coinPosY = {-3f, -1.8f, -0.5f, -1.8f}; // coin이 생성될 위치
    private float[] obstaclePosY = {-4.2f, -2.5f}; // spike plate와 arrow가 생성될 위치
    private int index = 0;
    private int coinCount = 0; // 생성된 코인 개수
    
    private Coroutine objectSpawnCoroutine;

    void Start() {
        StartObjectSpawn();
    }

    private void StartObjectSpawn() {
        objectSpawnCoroutine = StartCoroutine(SpawnObjectRoutine());
    }
    
    public void StopObjectSpawn() {
        StopCoroutine(objectSpawnCoroutine);
    }

    IEnumerator SpawnObjectRoutine() {
        yield return new WaitForSeconds(1f);
        while (true) {
            SpawnObject();
            yield return new WaitForSeconds(coinInterval);
        }
    }

    

    private void SpawnObject() {
        // Spawn Coin
        SetCoinPositionY();
        CreateCoin();

        // Spawn SpikePlate
        if (index == coinPosY.Length / 2) {
            CreateObstacle();
        }

        // Spawn SpikeBall
        if (coinCount % 10 == 8) { // 임의의 타이밍 마다 1개씩
            CreateSpikeBall();
        }
    }

    private void SetCoinPositionY() {
        if (coinCount != 0 && coinCount % 10 == 0) { // 10번 간격으로 coin이 Wave 형태로 생성
            index++;
            if (index == coinPosY.Length) { // Wave가 끝났다면
                index = 0;
                coinCount++;
            }
        } else {
            coinCount ++;
        }
    }

    private void CreateCoin() {
        float posY = coinPosY[index];
        Vector3 initPos = new Vector3(transform.position.x, posY, transform.position.z);
        Instantiate<Coin>(coinPrefab, initPos, Quaternion.identity);
    }

    private void CreateObstacle() {
        int obstacleIndex = Random.Range(0, 2);
        Vector3 initPos = new Vector3(transform.position.x, obstaclePosY[obstacleIndex], transform.position.z);
        Instantiate<GameObject>(obstaclePrefab[obstacleIndex], initPos, Quaternion.identity);
    }

    private void CreateSpikeBall() {
        Vector3 initPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        SpikeBall spikeBall = Instantiate<SpikeBall>(spikeBallPrefab, initPos, Quaternion.identity);
        Vector2 throwDir = new Vector2(Random.Range(-0.9f, -1f), Random.Range(0.5f, 0.6f));
        float throwForce = 13f;
        spikeBall.GetComponent<Rigidbody2D>().AddForce(throwDir * throwForce, ForceMode2D.Impulse);
    }

    public void DecreaseInterval() {
        coinInterval -= 0.01f;
    }
}
