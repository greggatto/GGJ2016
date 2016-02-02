using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountdownClock : MonoBehaviour {
    public MessManager messManager;
    public int clockTimer = 3600;
    private int defaultTime = 3600;
    private Rect clockPosition;
    public Text Timer;
    private bool isCounting = false;


	void Start () {
        clockTimer = defaultTime;
        clockPosition = new Rect(0, 0, 100, 100);
	
	}

    private string convertToDigits(int time)
    {
        string temp = "";
        int seconds = (int)(time * Time.fixedDeltaTime) % 60;
        int minutes = (int)((time * Time.fixedDeltaTime) - seconds) / 60;
        if (seconds < 10)
        {
            if (minutes < 10)
            {
                temp = "0"+minutes + ":" + "0" + seconds;
            }else{
                temp = minutes + ":" + "0" + seconds;
            }
        }
        else
        {
            if (minutes < 10)
            {
                temp = "0" + minutes + ":" + seconds;
            }
            else {
                temp = minutes + ":" + seconds;
            }

        }
        return temp;
    }

    public void FixedUpdate()
    {
        if (isCounting)
        {
            if (clockTimer > 0)
            {
                clockTimer--;
                
                Timer.text = convertToDigits(clockTimer);
                if ((clockTimer <= (defaultTime/2)) && ((clockTimer + 1) > (defaultTime/2)))
                {
                    NotifyHalfTime();
                }
                if ((clockTimer <= (60/Time.fixedDeltaTime)) && ((clockTimer + 1) > (60 / Time.fixedDeltaTime)))
                {
                    NotifyMinuteLeft();
                }
            }
            else
            {
                EndLevel();
                isCounting = false;
                NotifyOutOfTime();
            }
        }
    }

    public void ResetClock(int value)
    {
        clockTimer = value;
    }

    public void EndLevel()
    {
        //messManager.CalculateScore();
        ResetClock(defaultTime);
    }

    private void NotifyHalfTime()
    {
        Debug.Log("half time");
    }

    private void NotifyMinuteLeft()
    {
        Debug.Log("one minute left");
    }

    private void NotifyOutOfTime()
    {
        Debug.Log("out of time");
        GameManager.Instance.NotifyGameOutOfTime();
    }

    public void setIsCounting(bool b)
    {
        isCounting = b;
    }
}
