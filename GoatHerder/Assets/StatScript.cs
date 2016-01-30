using UnityEngine;
using System.Collections;
using System;

public class StatScript : MonoBehaviour {
    public struct GoatInfo
    {
        public string GoatName;
        public float timeAlive;
    }

    public ArrayList myList;

	// Use this for initialization
	void Start () {
        myList = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void storeInfo(string name, float time)
    {
        GoatInfo tempGoat = new GoatInfo();
        tempGoat.GoatName = name;
        tempGoat.timeAlive = time;
        myList.Add(tempGoat);
    }
}
