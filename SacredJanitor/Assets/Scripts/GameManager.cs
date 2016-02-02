using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    private LevelManager levelManager;
    
    public int[] levelOrder;
    public int currentLevelIndex = 0;
    private int score;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }

    void Awake()
    {
        //if (GameObject.FindGameObjectsWithTag("GameManager").Length > 1)
        //{
        //    Destroy(gameObject);
        //}
        DontDestroyOnLoad(gameObject);
        Instance = this;
        score = 0;
        ShuffleLevelOrder();

        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
}

    void OnLevelWasLoaded(int level)
    {
        currentLevelIndex++;
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        Debug.Log("I'm a little teapot");
        StartCoroutine(GameIntro());

    }

	void Start () {
        StartCoroutine(GameIntro());
        

    }

    public void Reset()
    {
        score = 0;
        ShuffleLevelOrder();
        currentLevelIndex = 0;
        Application.LoadLevel(Application.loadedLevel);
    }
	void Update () {
        
        


    }

    private void ShuffleLevelOrder()
    {
        levelOrder = new int[6];
        for (int i = 0; i < 6; i++)
        {
            int tempInt = Random.Range(0, levelOrder.Length);
            bool alreadyUsed = false;
            for (int j = 0; j < i; j++)
            {
                if (tempInt == levelOrder[j])
                {
                    alreadyUsed = true;
                    break;
                }
            }
            if (!alreadyUsed)
            {
                levelOrder[i] = tempInt;
            }
            else
            {
                i--;
            }
        }
    }

    private void setCanMove(bool b)
    {
        GameObject fpsController = GameObject.Find("FPSController");
        fpsController.GetComponent<CharacterController>().enabled = b;
        fpsController.GetComponent<FirstPersonController>().enabled = b;
    }

    public void SubmitScore(int s)
    {
        Score += s;
    }

    public void NotifyGameOutOfTime()
    {
        LoseGame();
    }

    public void NotifyLevelCompleted()
    {
        levelManager.DisplayScreen("pass");

        if (currentLevelIndex < 5)
        {
            SoundManager._instance.playIntro(levelOrder[currentLevelIndex + 1]);
        }

        //PrepareLoadNextLevel();
    }

    public void PrepareLoadNextLevel()
    {
        if (currentLevelIndex < 5)
        {
            Application.LoadLevel(Application.loadedLevel);
            Debug.Log(currentLevelIndex);
        }
        else
        {
            WinGame();
        }

    }

    private void WinGame()
    {
        Time.timeScale = 0.001f;
        levelManager.DisplayScreen("win");
    }

    private void LoseGame()
    {
        Time.timeScale = 0.001f;
        levelManager.DisplayScreen("lose");
    }

    IEnumerator GameIntro()
    {
        setCanMove(false);
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        CountdownClock countdownClock = GameObject.Find("Countdown").GetComponent<CountdownClock>();
        float secs = SoundManager._instance.playGameIntro();
        yield return new WaitForSeconds(secs);
        levelManager.FadeFromBlack();
        yield return new WaitForSeconds(3f);
        levelManager.switchLights();
        yield return new WaitForSeconds(1);
        levelManager.gameIsPlaying = true;
        yield return new WaitForSeconds(1);
        countdownClock.setIsCounting(true);
        levelManager.CheckRemaining();
        setCanMove(true);
        SoundManager._instance.playOutro(levelOrder[currentLevelIndex]);
        Debug.Log("tomato");

    }
}
