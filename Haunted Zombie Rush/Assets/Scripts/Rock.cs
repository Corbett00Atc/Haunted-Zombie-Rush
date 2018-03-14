using UnityEngine;
using System.Collections;

public class Rock : Object
{
    [SerializeField] Vector3 topPosition;
    [SerializeField] Vector3 botPosition;
    [SerializeField] float speed;
    [SerializeField] float rotation;


	// Use this for initialization
	void Start ()
    {
        StartCoroutine(Move(botPosition));
	}

    // rotates the rock 
    protected override void Update()
    {
        if (GameManager.instance.PlayerActive)
        {
            pushObject();
            rotateY();
        }
    }


    // moves rock up and down
    IEnumerator Move(Vector3 target)
    {
        // find the difference between target position and where we currently are
        // .2 chosen to give wiggle room for math errors
        while (Mathf.Abs((target - transform.position).y) > 0.30f) 
        {
            // ternary expression to move depending on the target
            // if target is the top it'll read true and move it upg
            Vector3 direction = target.y == topPosition.y ? Vector3.up : Vector3.down;
            transform.position += direction * Time.deltaTime * speed;

            // keeps running loop regardless of return
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        Vector3 newTarget = target.y == topPosition.y ? botPosition : topPosition;

        StartCoroutine(Move(newTarget));
    }

    private void rotateY()
    {
        transform.Rotate(0, Time.deltaTime * rotation, 0, Space.World);
    }
}
