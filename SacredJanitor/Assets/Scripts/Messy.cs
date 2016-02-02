using UnityEngine;
using System.Collections;

public class Messy : MonoBehaviour {
	public bool needsCleaning = true;
	public int pointsDetriment;
	public string messType;
    private bool isPuttingAway = false;
    //private float lerpTimer = 0f;
    //private Vector3 targetPos = Vector3.zero;

    
	void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == messType)
        {
            //targetPos = c.gameObject.transform.position;
            //PutAway();
            GameObject.Find("LevelManager").GetComponent<LevelManager>().CheckRemaining();
            gameObject.SetActive(false);
            if (GameManager.Instance.currentLevelIndex < 5)
            {
                SoundManager._instance.playEavesdrop(GameManager.Instance.levelOrder[GameManager.Instance.currentLevelIndex + 1]);
            }
        }
        else
        {
            if ((messType=="garbage"&&c.gameObject.tag=="sacred")|| (messType == "sacred" && c.gameObject.tag == "garbage")){

            }
        }
    }

    void FixedUpdate()
    {
        //if (isPuttingAway)
        //{
        //    lerpTimer += Time.deltaTime/Time.timeScale;
        //    float tempX = Mathf.Lerp(gameObject.transform.position.x, targetPos.x, lerpTimer*20);
        //    float tempY = Mathf.Lerp(gameObject.transform.position.y, targetPos.y, lerpTimer*20);
        //    float tempZ = Mathf.Lerp(gameObject.transform.position.z, targetPos.z, lerpTimer*20);
        //    gameObject.transform.position = new Vector3(tempX, tempY, tempZ);
        //}

    }

    //void PutAway()
    //{
    //    gameObject.GetComponent<Collider>().enabled = false;
    //    gameObject.GetComponent<Rigidbody>().detectCollisions = false;
    //    lerpTimer = 0f;
    //    isPuttingAway = true;
    //}
}
