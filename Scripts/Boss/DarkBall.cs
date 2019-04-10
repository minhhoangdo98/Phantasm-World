using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBall : MonoBehaviour
{
    //Script duoc dung boi object DarkBall
    public GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position, 2 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DealDamageTrigger"))
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == player.GetComponent<Collider2D>())
        {
            Destroy(gameObject);
        }
    }
}
