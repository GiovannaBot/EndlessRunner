using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerTutorial : MonoBehaviour {

    //UI Variables
    private UIManager UIManager;
    private int count = 0;

	void Start () {
        //UI
        UIManager = FindObjectOfType<UIManager>();

	}

    // Function to allocate the map
    private void OnTriggerEnter(Collider other)
    {
        count++;
        UIManager.UpdateTutorial(count);   
    }
}