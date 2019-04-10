using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePoint : MonoBehaviour
{
    //Script duoc dung boi object SavePoint
    public GameObject gameController, hoiThoai;
    private bool saving = false;
    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            saving = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            saving = false;
        }
    }

    void Update()
    {
        if (saving == true)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(SaveGame());
            }
        }
    }

    IEnumerator SaveGame()
    {
        hoiThoai.GetComponent<NoiChuyen>().face.GetComponent<RawImage>().texture = hoiThoai.GetComponent<NoiChuyen>().None;
        hoiThoai.GetComponent<NoiChuyen>().ten.GetComponent<Text>().text = "";
        hoiThoai.GetComponent<NoiChuyen>().talk.GetComponent<Text>().text = "Đã lưu!";
        hoiThoai.SetActive(true);
        gameController.GetComponent<EventController>().SaveGame();
        yield return new WaitForSeconds(0.5f);
        saving = false;
        hoiThoai.SetActive(false);
    }
}
