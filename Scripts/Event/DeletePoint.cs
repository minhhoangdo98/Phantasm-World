using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeletePoint : MonoBehaviour
{
    //script duoc dung de xoa file save
    public GameObject gameController, hoiThoai;
    private bool deleting = false;
    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            deleting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            deleting = false;
        }
    }

    void Update()
    {
        if (deleting == true)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(DeleteSave());
            }
        }
    }

    IEnumerator DeleteSave()
    {
        hoiThoai.GetComponent<NoiChuyen>().face.GetComponent<RawImage>().texture = hoiThoai.GetComponent<NoiChuyen>().None;
        hoiThoai.GetComponent<NoiChuyen>().ten.GetComponent<Text>().text = "";
        hoiThoai.GetComponent<NoiChuyen>().talk.GetComponent<Text>().text = "Đã xóa!";
        hoiThoai.SetActive(true);
        gameController.GetComponent<EventController>().DeleteSave();
        yield return new WaitForSeconds(0.5f);
        deleting = false;
        hoiThoai.SetActive(false);
    }
}
