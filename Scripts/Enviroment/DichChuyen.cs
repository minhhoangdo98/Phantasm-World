using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DichChuyen : MonoBehaviour
{
    public GameObject DiemDen, tranferEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = DiemDen.transform.position;
            GameObject tr = Instantiate(tranferEffect, collision.transform.position, Quaternion.identity);
            Destroy(tr, 0.5f);
        }
    }
}
