using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{
    public static Coin instance = null;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject coinParent;
    [SerializeField] float coinSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float spawnRate;

    private float minSpawnY = 8.5f;
    private float maxSpawnY = 13.72f;
    private bool spawnTrue = false;
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
            coin.transform.Rotate(Time.deltaTime * -rotationSpeed, Time.deltaTime * rotationSpeed, 0, Space.World);
        }
    }


    void SpawnCoin()
    {
        Instantiate(coinPrefab, new Vector3(coinParent.transform.position.x, 
                                            Random.Range(minSpawnY, maxSpawnY), 
                                            coinParent.transform.position.z), 
                                            Quaternion.identity
                                            );
    }

    public void Spawn() { spawnTrue = true; }
    public void Stop() { spawnTrue = false; }

    IEnumerator SpawnDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);

            if(spawnTrue == true)
                SpawnCoin();
        }
    }
}
