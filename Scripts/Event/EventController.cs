using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventController : MonoBehaviour
{

    //Script duoc dung boi script GameController
    public GameObject player, hoiThoai, playerStat;
    private GameController gc;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hoiThoai = GameObject.FindGameObjectWithTag("HoiThoai");
        playerStat = GameObject.FindGameObjectWithTag("Stat");
        gc = GetComponent<GameController>();

    }

    #region Save,Load,Delete
    public void SaveGame()//Luu game
    {
        PlayerPrefs.SetInt("str", playerStat.GetComponent<PlayerStat>().str);
        PlayerPrefs.SetInt("spd", playerStat.GetComponent<PlayerStat>().spd);
        PlayerPrefs.SetInt("vit", playerStat.GetComponent<PlayerStat>().vit);
        PlayerPrefs.SetInt("Level", playerStat.GetComponent<PlayerStat>().level);
        playerStat.GetComponent<PlayerStat>().UpdateHeal();//hoi day hp va stamina
        PlayerPrefs.SetInt("hp", playerStat.GetComponent<PlayerStat>().hp);
        PlayerPrefs.SetInt("stamina", playerStat.GetComponent<PlayerStat>().stamina);
        PlayerPrefs.SetInt("gold", playerStat.GetComponent<PlayerStat>().gold);
        PlayerPrefs.SetInt("stage1Complete", gc.stage1Complete);
        PlayerPrefs.SetInt("stage2Complete", gc.stage2Complete);
        PlayerPrefs.SetInt("stage3Complete", gc.stage3Complete);
        PlayerPrefs.SetInt("stage4Complete", gc.stage4Complete);
        if (gc.story < gc.storyTam)
            gc.story = gc.storyTam;
        PlayerPrefs.SetInt("story", gc.story);
        gc.saved = 1;//Da co file luu
        PlayerPrefs.SetInt("saved", gc.saved);
    }
    public void LoadGame()
    {
        gc.saved = PlayerPrefs.GetInt("saved");
        if (gc.saved == 1)//neu co da luu game truoc do, thi load lai file luu
        {
            playerStat.GetComponent<PlayerStat>().str = PlayerPrefs.GetInt("str");
            playerStat.GetComponent<PlayerStat>().spd = PlayerPrefs.GetInt("spd");
            playerStat.GetComponent<PlayerStat>().vit = PlayerPrefs.GetInt("vit");
            playerStat.GetComponent<PlayerStat>().level = PlayerPrefs.GetInt("Level");
            playerStat.GetComponent<PlayerStat>().gold = PlayerPrefs.GetInt("gold");
            gc.story = PlayerPrefs.GetInt("story");
            gc.storyTam = PlayerPrefs.GetInt("storyTam");
            playerStat.GetComponent<PlayerStat>().UpdateHeal();//hoi day hp
            gc.stage1Complete = PlayerPrefs.GetInt("stage1Complete");
            gc.stage2Complete = PlayerPrefs.GetInt("stage2Complete");
            gc.stage3Complete = PlayerPrefs.GetInt("stage3Complete");
            gc.stage4Complete = PlayerPrefs.GetInt("stage4Complete");
        }
        else//choi lai tu dau
        {
            playerStat.GetComponent<PlayerStat>().str = 1;
            playerStat.GetComponent<PlayerStat>().spd = 1;
            playerStat.GetComponent<PlayerStat>().vit = 1;
            playerStat.GetComponent<PlayerStat>().level = 1;
            playerStat.GetComponent<PlayerStat>().gold = 0;
            gc.saved = 0;
            gc.story = 0;
            gc.storyTam = 0;
            PlayerPrefs.SetInt("str", playerStat.GetComponent<PlayerStat>().str);
            PlayerPrefs.SetInt("spd", playerStat.GetComponent<PlayerStat>().spd);
            PlayerPrefs.SetInt("vit", playerStat.GetComponent<PlayerStat>().vit);
            PlayerPrefs.SetInt("Level", playerStat.GetComponent<PlayerStat>().level);
            PlayerPrefs.SetInt("gold", playerStat.GetComponent<PlayerStat>().gold);
            PlayerPrefs.SetInt("story", gc.story);
            if (gc.storyTam == 0)
                PlayerPrefs.SetInt("storyTam", gc.storyTam);
            PlayerPrefs.SetInt("saved", gc.saved);
            playerStat.GetComponent<PlayerStat>().UpdateHeal();
            gc.stage1Complete = 0;
            gc.stage2Complete = 0;
            gc.stage3Complete = 0;
            gc.stage4Complete = 0;
        }
    }
    public void DeleteSave()//Xoa save game, bat dau lai tu dau
    {
        gc.saved = 0;
        PlayerPrefs.SetInt("saved", gc.saved);
    }
    #endregion

    #region Event
    #region Event
    public void Intro()
    {
        StartCoroutine(EvIntro());

    }

    public void story1()
    {
        StartCoroutine(EvStory1());
    }

    public void story2()
    {
        StartCoroutine(EvStory2());
    }
    #endregion
    #region IEnumerator
    IEnumerator EvIntro()
    {
        switch (gc.textNum)
        {
            case 0:
                gc.blackScreen.SetActive(true);
                yield return new WaitForSeconds(2f);
                hoiThoai.GetComponent<NoiChuyen>().face.GetComponent<RawImage>().texture = hoiThoai.GetComponent<NoiChuyen>().None;
                hoiThoai.GetComponent<NoiChuyen>().ten.GetComponent<Text>().text = "???";
                hoiThoai.GetComponent<NoiChuyen>().talk.GetComponent<Text>().text = "Đây là thử thách cần phải vượt qua trước khi bắt đầu chính thức";
                hoiThoai.SetActive(true);
                gc.isPressNext = true;
                break;
            case 1:
                hoiThoai.GetComponent<NoiChuyen>().talk.GetComponent<Text>().text = "Bạn đã sẵn sàng chưa?";
                gc.isPressNext = true;
                break;
            case 2:
                hoiThoai.GetComponent<NoiChuyen>().talk.GetComponent<Text>().text = "Liệu bạn có đủ thực lực để gánh vác những trọng trách nặng nề hơn sau này";
                yield return new WaitForSeconds(1f);
                gc.blackScreen.SetActive(false);
                gc.fadeIn.SetActive(true);
                gc.isPressNext = true;
                break;
            case 3:
                hoiThoai.GetComponent<NoiChuyen>().talk.GetComponent<Text>().text = "Mọi thứ sẽ được quyết định ở vòng này, tuy chỉ là thử thách đơn giản nhưng hãy thật cẩn thận!";
                gc.isPressNext = true;
                break;
            case 4://Sau khi ket thuc mot story can nhung cau lenh nay
                hoiThoai.SetActive(false);
                gc.storyIsPlaying = false;
                gc.storyTam = 1;
                gc.textNum = 0;
                PlayerPrefs.SetInt("storyTam", gc.storyTam);
                gc.isPressNext = false;
                player.GetComponent<Player>().diChuyen = true;
                break;
        }
        yield return new WaitForSeconds(0.5f);
    }
    IEnumerator EvStory1()
    {
        //lam man hinh trang khi tao hieu ung no
        yield return new WaitForSeconds(0.5f);
        gc.fadeOutWhite.SetActive(true);
        yield return new WaitForSeconds(1f);
        player.GetComponent<Player>().diChuyen = false;
        gc.whiteScreen.SetActive(true);
        gc.fadeOutWhite.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        gc.whiteScreen.SetActive(false);
        gc.fadeInWhite.SetActive(true);
        yield return new WaitForSeconds(1f);
        gc.fadeInWhite.SetActive(false);

        gc.storyIsPlaying = false;
        gc.storyTam = 2;
        gc.textNum = 0;
        PlayerPrefs.SetInt("storyTam", gc.storyTam);
        player.GetComponent<Player>().diChuyen = true;
    }
    IEnumerator EvStory2()
    {
        switch (gc.textNum)
        {
            case 0:
                gc.whiteScreen.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                gc.whiteScreen.SetActive(false);
                gc.fadeInWhite.SetActive(true);
                yield return new WaitForSeconds(1f);
                gc.fadeInWhite.SetActive(false);
                yield return new WaitForSeconds(1f);
                hoiThoai.GetComponent<NoiChuyen>().face.GetComponent<RawImage>().texture = hoiThoai.GetComponent<NoiChuyen>().Heart;
                hoiThoai.GetComponent<NoiChuyen>().ten.GetComponent<Text>().text = "???";
                hoiThoai.GetComponent<NoiChuyen>().talk.GetComponent<Text>().text = "Chúc mừng anh đã vượt qua được thử thách, tôi sẽ đưa anh đến Phantasm World ngay bây giờ";
                hoiThoai.SetActive(true);
                gc.isPressNext = true;
                break;
            case 1:
                hoiThoai.GetComponent<NoiChuyen>().talk.GetComponent<Text>().text = "Hãy cố gắng nhiều hơn nữa nhé!";
                gc.isPressNext = true;
                break;
            case 2:
                hoiThoai.GetComponent<NoiChuyen>().talk.GetComponent<Text>().text = "Chúng ta sẽ còn gặp nhau lần nữa! Sớm thôi!";
                gc.isPressNext = true;
                break;
            case 3:
                hoiThoai.SetActive(false);
                gc.storyIsPlaying = false;
                gc.storyTam = 3;
                gc.textNum = 0;
                PlayerPrefs.SetInt("storyTam", gc.storyTam);
                gc.isPressNext = false;
                SaveGame();
                SceneManager.LoadScene(4, LoadSceneMode.Single);
                break;
        }
    }
    #endregion
    #endregion
}
