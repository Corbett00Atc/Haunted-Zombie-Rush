using UnityEngine;
using System.Collections;

public class Object : MonoBehaviour
{
    [SerializeField] private float objectSpeed = 1;
    [SerializeField] private float resetPosition = 28f;
    [SerializeField] private float startPosition = -53.59f;

    // Use this for initialization
    void Start()
    {
	}
	
	// Update is called once per frame
	protected virtual void Update()
    {
        pushBridge();
	}

    private void pushBridge(int negativeForBridge = 1)
    {
        if (!GameManager.instance.GameOver)
        {

            transform.Translate(Vector3.left * (objectSpeed * Time.deltaTime));

            if (transform.localPosition.x >= resetPosition)
            {
                Vector3 newPos = new Vector3(startPosition, transform.position.y, transform.position.z);
                transform.position = newPos;
            }
        }
    }

    // moves object left regardless of tile
    protected virtual void pushObject()
    {
        if (!GameManager.instance.GameOver)
        {

            // Space.World will move it regardless of the orientation of the tilt
            transform.Translate(Vector3.right * (objectSpeed * Time.deltaTime), Space.World);

            if (transform.localPosition.x >= resetPosition)
            {
                Vector3 newPos = new Vector3(startPosition, transform.position.y, transform.position.z);
                transform.position = newPos;
            }
        }
    }
}
