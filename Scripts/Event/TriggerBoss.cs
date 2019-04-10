using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoss : MonoBehaviour
{
    public GameObject chanDuong, boss;
    public GameObject nhacNen;
    public AudioClip bossMusic;

    private void Start()
    {
        nhacNen = GameObject.FindGameObjectWithTag("NhacNen");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            nhacNen.GetComponent<AudioSource>().Stop();
            chanDuong.SetActive(true);
            boss.SetActive(true);
            nhacNen.GetComponent<AudioSource>().clip = bossMusic;
            nhacNen.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }
}
