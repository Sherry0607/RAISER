using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public float speed; //发射速度


    void Start()
    {

        DeatroySlef3Second();

    }

    // Update is called once per frame

    void Update()
    {

        transform.Translate(Vector3.right * Time.deltaTime * speed);

    }

    private void DeatroySlef3Second()
    {

        Destroy(transform.gameObject, 3);

    }
}
