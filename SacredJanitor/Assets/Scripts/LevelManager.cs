/* Script by Chelsea Schellenberg. 
* To be attached to a Level Manager object in the level with one child for each religion and one miscellaneous and all objects of that type the children of them
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
    private int levelScore;
    private int levelMaxScore = 2000;
    private int religionInt;
    private int itemRemainingCount;

    public Text itemCountDisplay;
    public Text messageDisplay;
    public Text subtextDisplay;
    public Image blackFilterScreen;

    public GameObject sigilPlane;
    public Texture[] sigilTextures;

    public List<GameObject> messObjects;
    private List<GameObject> unusedMessObjects;
    private List<GameObject> lightsList;
    private List<GameObject> torchesList;
    public bool gameIsPlaying = false;
	
	void Start () {
        religionInt = Mathf.FloorToInt(Random.Range(0, 5.99f));

        PopulateLevel(religionInt);

        lightsList = new List<GameObject>();
        GameObject[] tempObjArray = GameObject.FindGameObjectsWithTag("lighting");
        for (int i = 0; i < tempObjArray.Length; i++)
        {
            lightsList.Add(tempObjArray[i]);
        }

        torchesList = new List<GameObject>();
        tempObjArray = GameObject.FindGameObjectsWithTag("torch");
        for (int i = 0; i < tempObjArray.Length; i++)
        {
            torchesList.Add(tempObjArray[i]);
        }

        
        //switchLights();

        //int[] tempArray = returnRandomsFromRange(2, 3, 15);
        //Debug.Log(tempArray[0] + "," + tempArray[1]);

    }

    void PopulateLevel(int religion){
        religionInt = religion;
        GameObject[] tempObjArray = getAllImmediateChildren(gameObject);

        messObjects = new List<GameObject>();
        List<GameObject> tempMessObjects = new List<GameObject>();
        unusedMessObjects = new List<GameObject>();

        for (int i = 0; i < 7; i++)
        {
            if ((i != religionInt) && (i != 6))
            {

                int jMax = gameObject.transform.GetChild(i).childCount;
                for (int j = 0; j < jMax; j++)
                {
                    unusedMessObjects.Add(gameObject.transform.GetChild(i).GetChild(j).gameObject);
                }
            }
            else
            {

                int jMax = gameObject.transform.GetChild(i).childCount;
                for (int j = 0; j < jMax; j++)
                {
                    tempMessObjects.Add(gameObject.transform.GetChild(i).GetChild(j).gameObject);
                }
            }
        }

        int[] itemsToChoose = returnRandomsFromRange(2, 0, tempMessObjects.Count - 1);
        for (int i = 0; i < tempMessObjects.Count; i++)
        {
            bool isChosen = false;
            for (int j = 0; j < itemsToChoose.Length; j++)
            {
                if (itemsToChoose[j] == i)
                {
                    isChosen = true;
                }
            }
            if (isChosen)
            {
                messObjects.Add(tempMessObjects[i]);
            }
            else
            {
                unusedMessObjects.Add(tempMessObjects[i]);
            }
        }

        for (int i = 0; i < unusedMessObjects.Count; i++)
        {
            unusedMessObjects[i].SetActive(false);
        }
        for (int i = 0; i < messObjects.Count; i++)
        {
            messObjects[i].SetActive(true);
        }
        itemRemainingCount = messObjects.Count;
        sigilPlane.GetComponent<Renderer>().material.mainTexture = sigilTextures[religionInt];
    }

	
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }else if (Input.GetKeyDown(KeyCode.Return))
        {
            GameManager.Instance.Reset();
        }
	}

    public void switchLights()
    {
        setTorchesOn(gameIsPlaying);
        setLightsOn(!gameIsPlaying);
    }

    void setTorchesOn(bool b)
    {
        for (int i = 0; i < torchesList.Count; i++)
        {
            torchesList[i].GetComponent<Light>().enabled = b;
        }
    }
    void setLightsOn(bool b)
    {
        for (int i = 0; i < lightsList.Count; i++)
        {
            lightsList[i].GetComponent<Light>().enabled = b;
        }

    }

    GameObject[] getAllImmediateChildren(GameObject go)
    {
        GameObject[] rtn = new GameObject[go.transform.childCount];
        for (int i = 0; i < go.transform.childCount; i++)
        {
            rtn[i] = go.transform.GetChild(i).gameObject;
        }
        return rtn;
    }

    int[] returnRandomsFromRange(int numberOfRands, int min, int max)
    {
        int[] temp = new int[numberOfRands];
        for (int i = 0; i < numberOfRands; i++)
        {
            temp[i] = -1; //not valid b/c will be used for checking lists
        }
        int index = 0;
        while (index < numberOfRands)
        {
            int tempInt = 0;
            bool found = false;
            tempInt = Random.Range(min, max+1);
            for (int i = 0; i < index; i++)
            {
                if (temp[i] == tempInt)
                {
                    found = true;
                }
            }
            if (!found)
            {
                temp[index] = tempInt;
                index++;
            }
        }
        return temp;
    }
    public void FadeFromBlack()
    {
        StartCoroutine(FadeFromBlackCoroutine());
    }
    public void FadeToBlack()
    {
        StartCoroutine(FadeToBlackCoroutine());
    }

    public void CheckRemaining()
    {
        StartCoroutine(WaitAndCheckRemaining());
    }

    public 

    IEnumerator WaitAndCheckRemaining()
    {
        yield return new WaitForFixedUpdate();
        itemCountDisplay.text = "" + GameObject.FindGameObjectsWithTag("pickup").Length;
        if (GameObject.FindGameObjectsWithTag("pickup").Length == 0)
        {
            GameManager.Instance.NotifyLevelCompleted();
        }
    }

    public void DisplayScreen(string screenName)
    {
        if (screenName == "win")
        {
            StartCoroutine(DisplayGameWin());
        }
        else if(screenName == "lose"){
            StartCoroutine(DisplayLevelLose());

        }
        else //screenName == pass
        {
            StartCoroutine(DisplayLevelWin());
        }
    }

    IEnumerator DisplayLevelWin()
    {
        StartCoroutine(FadeToBlackCoroutine());
        yield return new WaitForSeconds(1f * Time.timeScale);
        itemCountDisplay.text = "";
        messageDisplay.text = "Pass!";
        subtextDisplay.text = "The next ritual will be starting shortly.";
        yield return new WaitForSeconds(2f * Time.timeScale);
        GameManager.Instance.PrepareLoadNextLevel();
    }

    IEnumerator DisplayLevelLose()
    {
        StartCoroutine(FadeToBlackCoroutine());
        yield return new WaitForSeconds(1f * Time.timeScale);
        messageDisplay.text = "You Fail!";
        subtextDisplay.text = "Press Esc. to Exit or Enter to start a new game.";
    }

    IEnumerator DisplayGameWin()
    {
        StartCoroutine(FadeToBlackCoroutine());
        yield return new WaitForSeconds(1f * Time.timeScale);
        itemCountDisplay.text = "";
        messageDisplay.text = "You Win!";
        subtextDisplay.text = "Press Esc. to Exit or Enter to start a new game.";
        
    }

    IEnumerator FadeFromBlackCoroutine()
    {
        bool done = false;
        float coroutineTimer = 0f;
        while (!done)
        {
            coroutineTimer += Time.deltaTime / Time.timeScale;
            float tempA = Mathf.Lerp(blackFilterScreen.color.a, 0f, coroutineTimer);
            if (Mathf.Abs(tempA - 0f) < 0.05f)
            {
                tempA = 0;
                done = true;
            }
            blackFilterScreen.color = new Color(blackFilterScreen.color.r, blackFilterScreen.color.g, blackFilterScreen.color.b, tempA);
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator FadeToBlackCoroutine()
    {
        bool done = false;
        float coroutineTimer = 0f;
        while (!done)
        {
            coroutineTimer += Time.deltaTime / Time.timeScale;
            float tempA = Mathf.Lerp(blackFilterScreen.color.a, 1f, coroutineTimer);
            if (Mathf.Abs(tempA - 1f) < 0.05f)
            {
                tempA = 1;
                done = true;
            }
            blackFilterScreen.color = new Color(blackFilterScreen.color.r, blackFilterScreen.color.g, blackFilterScreen.color.b, tempA);
            yield return new WaitForEndOfFrame();
        }
    }
}
