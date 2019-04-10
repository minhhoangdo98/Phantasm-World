using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour {
//script dung de test chuc nang knockback
    public GameObject Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == Player.GetComponent<Collider2D>())
        {
            StartCoroutine(kBack());//bi knockback
            
        }
    }
  
    IEnumerator kBack()
    {
        if (Player.GetComponent<SpriteRenderer>().flipX == true)//kiem tra xem nguoi choi co doi huong hay khong, nguoi choi dang quay ve phia nao
        {
            Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(120f, Player.GetComponent<Rigidbody2D>().velocity.y));//bi luc tac dung day lui 120f
            Player.GetComponent<Player>().diChuyen = false;//khong the di chuyen trong luc bi day lui
            Player.GetComponent<SpriteRenderer>().color = Color.red;//thay doi mau de mo phong nhan sat thuong
            yield return new WaitForSeconds(0.4f);//doi 0.4s
            Player.GetComponent<Player>().diChuyen = true;//di chuyen lai binh thuong
            Player.GetComponent<SpriteRenderer>().color = Color.white;//tro lai mau nhu cu
        }
        else
        {
            Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-120f, Player.GetComponent<Rigidbody2D>().velocity.y));
            Player.GetComponent<Player>().diChuyen = false;
            Player.GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.4f);
            Player.GetComponent<Player>().diChuyen = true;
            Player.GetComponent<SpriteRenderer>().color = Color.white;
        }    
    }
}
