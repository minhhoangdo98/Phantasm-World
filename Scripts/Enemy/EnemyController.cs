using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //script duoc dung boi object Enemy
    public GameObject player, playerStat, attackEffect, rightSight, leftSight, explor, gameController;
    public int hp = 10, gold = 10, atk = 5;
    public float stopMoveTime = 1f, speed = 1.5f;
    public bool faceRight = true, diChuyen = true, damagable = true;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        player = GameObject.FindGameObjectWithTag("Player");
        playerStat = GameObject.FindGameObjectWithTag("Stat");
    }

    #region Gay sat thuong cho nguoi choi
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == player.GetComponent<Collider2D>() && !gameController.GetComponent<GameController>().isWin && !gameController.GetComponent<GameController>().isGameOver && damagable)//Neu doi tuong cham vao la nguoi choi va chua win game, chua gameover va cho phep gay sat thuong
        {
            damagable = false;
            Vector3 pos = new Vector3(collision.transform.position.x, collision.transform.position.y);
            GameObject ae = Instantiate(attackEffect, pos, Quaternion.identity, collision.transform) as GameObject;//tao hieu ung danh trung nguoi choi
            Destroy(ae, 0.5f);
            StartCoroutine(StopMove());//Khi tan cong ke dich phai dung lai trong khoang thoi gian ngan
            KBack();//bi knockback
            collision.collider.SendMessage("TakeDamage", atk);//nguoi choi nhan sat thuong
        }
    }
    #endregion

    #region Nhan sat thuong 
    void TakeDamage(int damageAmount)
    {
        hp -= damageAmount;
        if (gameObject.CompareTag("Enemy"))
            EnemyKBack();//Day lui ke dich, ngoai tru boss
        if (hp <= 0)
        {
            if (gameObject.CompareTag("Boss"))
            {
                GameObject ep = Instantiate(explor, gameObject.transform.position, Quaternion.identity) as GameObject;
                Destroy(ep, 3f);
                playerStat.GetComponent<PlayerStat>().gold += gold;
                Destroy(gameObject, 0.5f);
                playerStat.GetComponent<PlayerStat>().UpdateHeal();
            }
            else
            {
                playerStat.GetComponent<PlayerStat>().gold += gold;
                Destroy(gameObject, 0.5f);
            }
           
        }
        StartCoroutine(StopMove());
            
    }

    IEnumerator StopMove()//ngung di chuyen
    {
        diChuyen = false;
        yield return new WaitForSeconds(stopMoveTime);
        diChuyen = true;
        damagable = true;
    }
    #endregion

    #region Day lui
    public void KBack()//Nguoi choi bi day lui
    {
        if (player.GetComponent<SpriteRenderer>().flipX == true)//kiem tra xem nguoi choi co doi huong hay khong, nguoi choi dang quay ve phia nao
        {
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(150f, player.GetComponent<Rigidbody2D>().velocity.y + 100f));//bi luc tac dung day lui 120f
            player.GetComponent<Player>().diChuyen = false;//khong the di chuyen trong luc bi day lui
        }
        else
        {
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-150f, player.GetComponent<Rigidbody2D>().velocity.y + 100f));
            player.GetComponent<Player>().diChuyen = false; 
        }
    }

    public void EnemyKBack()//ke dich bi day lui
    {
        if (faceRight == true)//kiem tra xem ke dich co doi huong hay khong, ke dich dang quay ve phia nao
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(150f, gameObject.GetComponent<Rigidbody2D>().velocity.y + 100f));//bi luc tac dung day lui 150f
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-150f, gameObject.GetComponent<Rigidbody2D>().velocity.y + 100f));
        }
    }
    #endregion
}
