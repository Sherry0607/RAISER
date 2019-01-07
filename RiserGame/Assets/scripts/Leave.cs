using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leave : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<CharacterControl>().LifeChange(true);
            Destroy(gameObject);

        }
    }
}
