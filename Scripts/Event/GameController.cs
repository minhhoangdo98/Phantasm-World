using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    //Script duoc dung boi object GameController
    public int story, storyTam, textNum, saved = 0, stage1Complete = 0, stage2Complete = 0, stage3Complete = 0, stage4Complete = 0;//story dung de xac dinh cot truyen dang den dau, textNum dung de xac dinh cuoc doi thoai, saved = 0 la chua co file save game
    public GameObject player, fadeIn, blackScreen, whiteScreen, fadeOut, hoiThoai, playerStat, fadeInWhite, fadeOutWhite;
    private EventController ev;
    public bool storyIsPlaying = false, isPressNext = true, isWin = false, isGameOver = false;//khi muon bat dau mot story thi goi storyIsPlaying = true ngoai tru Intro dau, saved = false la chua co file save game

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fadeIn = GameObject.FindGameObjectWithTag("FadeIn");
        fadeOut = GameObject.FindGameObjectWithTag("FadeOut");
        fadeInWhite = GameObject.FindGameObjectWithTag("FadeIn White");
        fadeOutWhite = GameObject.FindGameObjectWithTag("FadeOut White");
        blackScreen = GameObject.FindGameObjectWithTag("BlackScreen");
        whiteScreen = GameObject.FindGameObjectWithTag("WhiteScreen");
        hoiThoai = GameObject.FindGameObjectWithTag("HoiThoai");
        playerStat = GameObject.FindGameObjectWithTag("Stat");
        ev = GetComponent<EventController>();
        blackScreen.SetActive(false);
        whiteScreen.SetActive(false);
        fadeInWhite.SetActive(false);
        fadeOutWhite.SetActive(false);
        fadeIn.SetActive(false);
        fadeOut.SetActive(false);
        hoiThoai.GetComponent<NoiChuyen>().luaChon.SetActive(false);
        hoiThoai.SetActive(false);
        ev.LoadGame();//load game moi khi chuyen map, dung de load story
        textNum = 0;
        if (story == 0)//bat dau story intro
        {
            player.GetComponent<Player>().diChuyen = false;
            storyIsPlaying = true;//story dang chay
            ev.Intro();
        }
        if (storyTam < story)
            storyTam = story;
        else
            story = storyTam;

        if (story == 2)
        {
            player.GetComponent<Player>().diChuyen = false;
            storyIsPlaying = true;
            PlayStory();
        }
    }

    void Update()
    {
        if (storyIsPlaying && isPressNext)//story co dang chay hay khong va co cho phep bam next hay khong
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))//nut bam de chuyen text doi thoai
            {
                isPressNext = false;
                textNum++;
                PlayStory();
            }

        }
        if (story < storyTam)
            story = storyTam;

    }

    private void OnApplicationQuit()//khi quit game, story tam = 0 khi mo len se lay gia tri tu story
    {
        storyTam = 0;
        PlayerPrefs.SetInt("storyTam", storyTam);
    }

    #region Story
    public void PlayStory()//Ham chay story
    {
        if (storyIsPlaying)//Neu cho phep chay story
        {
            switch (story)//xac dinh story hien tai
            {
                case 0:
                    ev.Intro();
                    break;
                case 1://neu bat dau story 1 va cho phep chay story
                    ev.story1();

                    break;
                case 2:
                    ev.story2();
                    break;

            }
        }
    }
    #endregion
}
