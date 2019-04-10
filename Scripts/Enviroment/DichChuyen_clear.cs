using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DichChuyen_clear : MonoBehaviour
{
    GameObject gameController;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch(gameController.GetComponent<GameController>().story)
            {
                case 2:
                    gameController.GetComponent<GameController>().player.GetComponent<Player>().diChuyen = false;
                    StartCoroutine(FadeWhite());
                    break;

            }
        }
    }

    IEnumerator FadeWhite()
    {
        yield return new WaitForSeconds(0.5f);
        gameController.GetComponent<GameController>().fadeOutWhite.SetActive(true);
        yield return new WaitForSeconds(1f);
        gameController.GetComponent<GameController>().whiteScreen.SetActive(true);
        gameController.GetComponent<GameController>().fadeOutWhite.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        gameController.GetComponent<EventController>().SaveGame();
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

}
