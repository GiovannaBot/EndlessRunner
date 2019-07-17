using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private Transform player;
    private Vector3 offset;

    //public float speed;

    // Use this for initialization
    void Start()
    {
        // temporary speed
        //GetComponent<Rigidbody>().velocity = new Vector3(0, 0, speed);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, (player.position.z + offset.z));
        transform.position = newPosition; 
    }
}
