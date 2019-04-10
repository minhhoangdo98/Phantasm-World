using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TittleController : MonoBehaviour
{
    //Script duoc dung boi object TittleController
    public GameObject logoFadeIn, logoFadeOut, blackScreen;
    bool skip = true;
    public int saved;
    void Start()
    {
        saved = PlayerPrefs.GetInt("saved");
        StartCoroutine(ShowLogo());
    }

    IEnumerator ShowLogo()
    {
        yield return new WaitForSeconds(6f);
        logoFadeIn.SetActive(true);
        yield return new WaitForSeconds(1.8f);
        logoFadeOut.SetActive(true);
        logoFadeIn.SetActive(false);
        blackScreen.SetActive(false);
        yield return new WaitForSeconds(5.3f);
        logoFadeOut.SetActive(false);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
        PlayerPrefs.SetInt("saved", 0);
    }

    public void Continue()
    {
        if (saved == 1)
        {
            SceneManager.LoadScene(4, LoadSceneMode.Single);
        }
    }

    public void Thoat()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Space))
        {
            if (skip)
            {
                StopAllCoroutines();
                blackScreen.SetActive(false);
                logoFadeIn.SetActive(false);
                logoFadeOut.SetActive(false);
            }
        }
    }
}
