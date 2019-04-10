using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBoss : MonoBehaviour
{
    //script duoc dung boi object Tutorial Boss
    public bool rage = false, raging, normalSkillAttacking = false;
    public GameObject nhacNen, rageEffect, diemDiChuyen1, diemDiChuyen2, diemDiChuyen3, darkBall, castSkill, gameController, daChanDuongSau;
    public AudioClip bossRageMusic;
    private EnemyController ec;

    private void Start()
    {
        nhacNen = GameObject.FindGameObjectWithTag("NhacNen");
        gameController = GameObject.FindGameObjectWithTag("GameController");
        ec = GetComponent<EnemyController>();
    }

    private void Update()
    {
        if (ec.hp > 30 && !rage && !normalSkillAttacking && !gameController.GetComponent<GameController>().isGameOver)//Neu hp > 30 va chua rage va dang khong dung skill thuong thi dung skill thuong
        {
            normalSkillAttacking = true;
            StartCoroutine(NormalSkill());
        }

        if (ec.hp<=30 && !rage)//Neu hp <= 30 va chua rage 
        {
            rage = true;
            ec.rightSight.SetActive(false);
            ec.leftSight.SetActive(false);
            StartCoroutine(RageChange());
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if(rage && !raging && !gameController.GetComponent<GameController>().isWin && !gameController.GetComponent<GameController>().isGameOver)//Neu dang trong trang thai rage va van chua thi trien skill ulti thi dung ulti
        {
            StartCoroutine(BossUlti());
        }

        if (ec.GetComponent<EnemyController>().hp <= 0)
        {
            nhacNen.GetComponent<AudioSource>().Stop();
            gameController.GetComponent<GameController>().isWin = true;
            gameController.GetComponent<GameController>().storyIsPlaying = true;
            gameController.GetComponent<GameController>().PlayStory();
            daChanDuongSau.SetActive(false);
        }
    }

    #region Skill
    IEnumerator NormalSkill()
    {
        GameObject ck = Instantiate(castSkill, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f, 0), Quaternion.identity, gameObject.transform) as GameObject;
        Destroy(ck, 0.8f);
        yield return new WaitForSeconds(0.8f);

        if (ec.faceRight)
        {
            GameObject db = Instantiate(darkBall, new Vector3(gameObject.transform.position.x + 1f, gameObject.transform.position.y, 0), Quaternion.identity) as GameObject;
            Destroy(db, 2f);
        }

        if (!ec.faceRight)
        {
            GameObject db = Instantiate(darkBall, new Vector3(gameObject.transform.position.x - 1f, gameObject.transform.position.y, 0), Quaternion.identity) as GameObject;
            Destroy(db, 2f);
        }

        yield return new WaitForSeconds(4f);
        normalSkillAttacking = false;
    }

    IEnumerator RageChange()//Thay doi tu trang thai thuong sang trang thai rage
    {
        nhacNen.GetComponent<AudioSource>().Stop();
        GameObject re = Instantiate(rageEffect, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, 0), Quaternion.identity, gameObject.transform) as GameObject;//tao rage cho boss
        Destroy(re, 1f);
        yield return new WaitForSeconds(1f);
        nhacNen.GetComponent<AudioSource>().clip = bossRageMusic;
        nhacNen.GetComponent<AudioSource>().Play();
        if (ec.faceRight)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
            ec.faceRight = !ec.faceRight;
        }
        raging = false;//kich hoat skill ulti
    }

    IEnumerator BossUlti()
    {
        raging = true;//skill ulti dang dung 
        GameObject db1, db2, db3;
        yield return new WaitForSeconds(0.5f);
        gameObject.transform.position = Vector2.MoveTowards(transform.position, diemDiChuyen1.transform.position, ec.speed * 100 * Time.deltaTime);
        db1 = Instantiate(darkBall, transform.position, Quaternion.identity) as GameObject;
        Destroy(db1, 1f);
        yield return new WaitForSeconds(0.5f);
        gameObject.transform.position = Vector2.MoveTowards(transform.position, diemDiChuyen2.transform.position, ec.speed * 100 * Time.deltaTime);
        db2 = Instantiate(darkBall, transform.position, Quaternion.identity) as GameObject;
        Destroy(db2, 1f);
        yield return new WaitForSeconds(0.5f);
        gameObject.transform.position = Vector2.MoveTowards(transform.position, diemDiChuyen3.transform.position, ec.speed * 100 * Time.deltaTime);
        yield return new WaitForSeconds(0.8f);
        gameObject.transform.position = Vector2.MoveTowards(transform.position, diemDiChuyen2.transform.position, ec.speed * 100 * Time.deltaTime);
        db3 = Instantiate(darkBall, transform.position, Quaternion.identity) as GameObject;
        Destroy(db3, 1f);
        raging = false;
    }
    #endregion
}
