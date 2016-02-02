using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
                
    public AudioSource musicSource;                 
    public AudioSource ambianceSource;    
    public static SoundManager _instance = null;     //Allows other scripts to call functions from SoundManager.

    public GameObject[] roomSoundSpots;
    public GameObject[] outeroomSoundSpots;

    public AudioClip[] GameIntro;

    public AudioClip[] FlockersIntro;
    public AudioClip[] FlockersOutro;
    public AudioClip[] FlockersEavesdrop;

    public AudioClip[] OneEarthIntro;
    public AudioClip[] OneEarthOutro;
    public AudioClip[] OneEarthEavesdrop;

    public AudioClip[] ExemptionIntro;
    public AudioClip[] ExemptionOutro;
    public AudioClip[] ExemptionEavesdrop;

    public AudioClip[] SantaClausIntro;
    public AudioClip[] SantaClausOutro;
    public AudioClip[] SantaClausEavesdrop;

    public AudioClip[] IridescentIntro;
    public AudioClip[] IridescentOutro;
    public AudioClip[] IridescentEavesdrop;

    public AudioClip[] ForksIntro;
    public AudioClip[] ForksOutro;
    public AudioClip[] ForksEavesdrop;

    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (_instance == null)
            //if not, set it to this.
            _instance = this;
        //If instance already exists:
        else if (_instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }

    public void playIntro(int level)
    {
        switch (level) {
            case 0:
                playRandomSfx(FlockersIntro, roomSoundSpots);
                break;
            case 1:
                playRandomSfx(OneEarthIntro, roomSoundSpots);
                break;
            case 2:
                playRandomSfx(SantaClausIntro, roomSoundSpots);
                break;
            case 3:
                playRandomSfx(IridescentIntro, roomSoundSpots);
                break;
            case 4:
                playRandomSfx(ForksIntro, roomSoundSpots);
                break;
        }
    }

    public void playOutro(int level)
    { 
        switch (level) {
            case 0:
                playRandomSfx(FlockersOutro, roomSoundSpots);
                break;
            case 1:
                playRandomSfx(OneEarthOutro, roomSoundSpots);
                break;
            case 2:
                playRandomSfx(SantaClausOutro, roomSoundSpots);
                break;
            case 3:
                playRandomSfx(IridescentOutro, roomSoundSpots);
                break;
            case 4:
                playRandomSfx(ForksOutro, roomSoundSpots);
                break;
        }
    }

    public void playEavesdrop (int level)
    {
        switch (level)
        {
            case 0:
                playRandomSfx(FlockersEavesdrop, outeroomSoundSpots);
                break;
            case 1:
                playRandomSfx(OneEarthEavesdrop, outeroomSoundSpots);
                break;
            case 2:
                playRandomSfx(SantaClausEavesdrop, outeroomSoundSpots);
                break;
            case 3:
                playRandomSfx(IridescentEavesdrop, outeroomSoundSpots);
                break;
            case 4:
                playRandomSfx(ForksEavesdrop, outeroomSoundSpots);
                break;
        }
    }

    public float playGameIntro()
    {
        return playRandomSfx(GameIntro, roomSoundSpots);
    }


    float playRandomSfx (AudioClip[] clips, GameObject[] soundspots)
    {
        int randomIndex = Random.Range(0, clips.Length);

        for (int i=0; i< soundspots.Length; i++)
        {
            soundspots[i].GetComponent<AudioSource>().clip = clips[randomIndex];
            soundspots[i].GetComponent<AudioSource>().Play();
        }

        return clips[randomIndex].length;

    }

    public void playAmbience()
    {
        ambianceSource.Play();
    }

    public void stopAmbience()
    {
        ambianceSource.Stop();
    }

}
