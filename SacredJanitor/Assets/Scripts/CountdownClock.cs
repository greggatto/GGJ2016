using UnityEngine;
using System.Collections;

public class CountdownClock : MonoBehaviour {
    public MessManager messManager;
    public int clockTimer = 3600;
    private Rect clockPosition;
	void Start () {
        clockPosition = new Rect(0, 0, 100, 100);
	
	}
	
	
	void OnGUI () {
        GUI.Label(clockPosition, convertToDigits(clockTimer));
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
        if (clockTimer > 0)
        {
            clockTimer--;
        }
        else
        {
            EndLevel();
        }
    }

    public void ResetClock(int value)
    {
        clockTimer = value;
    }

    public void EndLevel()
    {
        messManager.CalculateScore();
        ResetClock(3600);
    }
}
