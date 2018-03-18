using UnityEngine;
using System.Collections;

public class PlayerOutOfBounds : MonoBehaviour
{
	void Start()
    {
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "player")
        {
            GameManager.instance.GameIsOver();
        }
    }
}
