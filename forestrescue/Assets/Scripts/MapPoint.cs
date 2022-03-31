using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{
    public MapPoint up, right, down, left;
    public bool isLevel, isLocked;
    public string levelToLoad, levelToCheck, levelName;

    public int gemsCollected, totalGems;
    public float bestTime, targetTime;

    public GameObject gemBadge, timeBadge;

    private bool updateCurrentLevel;

    // Start is called before the first frame update
    void Start()
    {
        if(isLevel && levelToLoad != null)
        {
            isLocked = true;

            if(levelToCheck != null)
            {
                if(PlayerPrefs.HasKey(levelToCheck + "_unlocked"))
                {
                    if(PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1)
                    {
                        isLocked = false;
                    }
                } 
            }

            if(levelToLoad == levelToCheck)
            {
                isLocked = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("here");
            updateCurrentLevel = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("not here");
            updateCurrentLevel = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(updateCurrentLevel)
        {
            if (isLevel && levelToLoad != null)
            {
                isLocked = true;

                if (levelToCheck != null)
                {
                    if (PlayerPrefs.HasKey(levelToCheck + "_unlocked"))
                    {
                        if (PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1)
                        {
                            isLocked = false;
                        }
                    }
                }

                if (levelToLoad == levelToCheck)
                {
                    isLocked = false;
                }

                if (!isLocked)
                {
                    LSPlayer.singleton.currentPoint = this;
                }
            }
        }
    }
}
