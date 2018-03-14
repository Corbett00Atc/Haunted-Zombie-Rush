using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{
    public static Coin instance = null;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject coinParent;
    [SerializeField] private float coinSpeed;
    [SerializeField] private float rotationSpeed;
    private float minSpawnY = 8.5f;
    private float maxSpawnY = 15f;
    private bool spawnTrue = false;
    private float x = -4.6f;
    private float z = -12.89f;
    [SerializeField] private float spawnRate;
    public Quaternion rotation = Quaternion.Euler(180, 180, 0);



    // Use this for initialization
    void Start ()
    {
        StartCoroutine(SpawnDelay());
	}

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // looks to see if there is a coin
        // if there is, it will move it
        if (GameObject.FindWithTag("coin"))
        {
            GameObject coin = GameObject.FindWithTag("coin");
            coin.GetComponent<Rigidbody>().transform.Translate(Vector3.right * (coinSpeed * Time.deltaTime), Space.World);
            coin.transform.Rotate(0, Time.deltaTime * rotationSpeed, 0, Space.World);
        }
    }



    void SpawnCoin()
    {
        Instantiate(coinPrefab, new Vector3(coinParent.transform.position.x, Random.Range(minSpawnY, maxSpawnY), coinParent.transform.position.z), Quaternion.identity);

    }

    public void Spawn()
    {
        spawnTrue = true;
    }

    public void Stop()
    {
        spawnTrue = false;
    }

    IEnumerator SpawnDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);

            if(spawnTrue == true)
            {
                SpawnCoin();
            }
        }
    }
}
