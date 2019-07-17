using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLane : MonoBehaviour {

    private int randomLane;
    private int countRotate = 0;

    public void PositionLane(){
        randomLane = Random.Range(-1, 2);
        transform.position = new Vector3(randomLane, transform.position.y, transform.position.z);
    }

    public void RotateLane() {
        transform.Rotate(transform.rotation.x ,Time.deltaTime,transform.rotation.z);
    }

}
