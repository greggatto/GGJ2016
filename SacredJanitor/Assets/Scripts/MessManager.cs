using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MessManager : MonoBehaviour {
    public List<GameObject> messObjects;
    public GameObject fridgeBox;
    int score = 1000;
	
	void Awake () {
        messObjects = new List<GameObject>();
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Mess");
        for (int i = 0; i < temp.Length; i++)
        {
            messObjects.Add(temp[i]);
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CalculateScore()
    {
        for (int i = 0; i < messObjects.Count; i++)
        {
            Messy thisMess = messObjects[i].GetComponent<Messy>();
            if ((messObjects[i].activeInHierarchy == true) && (thisMess.needsCleaning == true))
            {
                if (!isContained(messObjects[i], thisMess.messType))
                {
                    score -= thisMess.pointsDetriment;
                }
            }
        }
        Debug.Log(score);
    }

    private bool isContained(GameObject target, string messType)
    {
        if (messType == "fridge")
        {
            if (fridgeBox.GetComponent<Collider>().bounds.Contains(target.transform.position)){
                return true;
            }
        }
        
        return false;
        
    }
}
