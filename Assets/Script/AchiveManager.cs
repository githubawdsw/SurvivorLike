using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;
    public GameObject uiNotice;

    enum Achive    { unlockPotato , unlockBean }
    Achive[] achives;
    WaitForSecondsRealtime wait;
    private void Awake()
    {
        achives = (Achive[])Enum.GetValues(typeof( Achive));
        wait = new WaitForSecondsRealtime(5f);

        if (!PlayerPrefs.HasKey("MyData"))
            Init();
    }

    void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);

        foreach (var achive in achives)
        {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }
    }

    void Start()
    {
        UnLockCharacter();
    }

    void UnLockCharacter()
    {
        for (int idx = 0; idx < lockCharacter.Length; idx++)
        {
            string achiveName = achives[idx].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1;
            lockCharacter[idx].SetActive(!isUnlock);
            unlockCharacter[idx].SetActive(isUnlock);
        }
    }

    void LateUpdate()
    {
        foreach (var achive in achives)
        {
            CheckAchive(achive);
        }
    }

    void CheckAchive(Achive achive)
    {
        bool isAchive = false;
        switch (achive)
        {
            case Achive.unlockPotato:
                isAchive = GameManager.Instance.kill >= 10; 
                break;
            case Achive.unlockBean:
                isAchive = GameManager.Instance.gameTime == GameManager.Instance.maxGameTime;
                break;
            default:
                break;
        }

        if (isAchive && PlayerPrefs.GetInt(achive.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);

            for (int idx = 0; idx < uiNotice.transform.childCount; idx++)
            {
                bool isActive = idx == (int)achive;
                uiNotice.transform.GetChild(idx).gameObject.SetActive(isActive);
            }
            StartCoroutine(NoticeRoutine());
        }
    }

    IEnumerator NoticeRoutine()
    {
        uiNotice.SetActive(true);

        yield return wait;

        uiNotice.SetActive(false);
    }
}
