using UnityEngine;
using System.Collections;

public class SpawnerOfGameManager : MonoBehaviour {
    public GameObject GM;
	
	void Awake()
    {
        if (GameObject.FindGameObjectWithTag("GameManager") == null)
        {
            Instantiate(GM, Vector3.zero, Quaternion.Euler(new Vector3(0, 0, 0)));
        }

    }
}
