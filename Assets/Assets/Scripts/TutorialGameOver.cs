using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGameOver : MonoBehaviour
{

    void Start()
    {

    }

    // Function to allocate the map
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Player>().GameOver();
    }
}