using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    //script duoc dung boi object [Weapons]
    public GameObject Weapons, player, playerStat, slash;
    public bool WeaponAttack = false, attackable = true;
    public Animator anim;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStat = GameObject.FindGameObjectWithTag("Stat");
        anim =gameObject.GetComponent<Animator>();
        Weapons = gameObject;
        gameObject.GetComponent<SpriteRenderer>().flipY = true;
    }

    void Update()
    {
        anim.SetBool("WeaponAttack", WeaponAttack);
        if (player.GetComponent<Player>().diChuyen)//Neu cho phep di chuyen
        {
            if (Input.GetKey(KeyCode.C) && attackable && playerStat.GetComponent<PlayerStat>().stamina >= 5)//khi bam C va cho phep danh thuong va du stamina se danh thuong
            {
                playerStat.GetComponent<PlayerStat>().stamina -= 5;
                StartCoroutine(Attack());
            }
        }

    }

    #region Tan Cong bang vu khi
    IEnumerator Attack()//danh thuong
    {
        Transform oldPos = gameObject.transform;//luu lai vi tri goc
        WeaponAttack = true;
        attackable = false;

        gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
        gameObject.GetComponent<SpriteRenderer>().flipY = !gameObject.GetComponent<SpriteRenderer>().flipY;

        if (Mathf.Abs(player.GetComponent<Player>().r2.velocity.x) <= 0.1f || !player.GetComponent<Player>().grounded)
            player.GetComponent<Player>().attacktrigger = true;
        if (player.GetComponent<Player>().faceright)//danh ben phai
        {
            Vector3 pos = new Vector3(transform.position.x + 0.5f, transform.position.y );//hieu ung chem vi tri la canh vu khi
            GameObject sla = Instantiate(slash, pos, Quaternion.identity, gameObject.transform) as GameObject;//tao hieu ung chem
            gameObject.transform.position = new Vector2(oldPos.transform.position.x + 0.37f, oldPos.transform.position.y + 0.12f);
            yield return new WaitForSeconds(0.4f);
            Destroy(sla);
            gameObject.transform.position = new Vector2(oldPos.transform.position.x - 0.37f, oldPos.transform.position.y - 0.12f);
        }
        else//danh ben trai
        {
            Vector3 pos = new Vector3(transform.position.x - 0.5f, transform.position.y);//hieu ung chem vi tri la canh vu khi
            GameObject sla = Instantiate(slash, pos, Quaternion.identity, gameObject.transform) as GameObject;//tao hieu ung chem
            sla.GetComponent<SpriteRenderer>().flipY = true;
            gameObject.transform.position = new Vector2(oldPos.transform.position.x - 0.33f, oldPos.transform.position.y + 0.12f);
            yield return new WaitForSeconds(0.4f);
            Destroy(sla);
            gameObject.transform.position = new Vector2(oldPos.transform.position.x + 0.33f, oldPos.transform.position.y - 0.12f);
        }

        gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
        gameObject.GetComponent<SpriteRenderer>().flipY = !gameObject.GetComponent<SpriteRenderer>().flipY;

        player.GetComponent<Player>().attacktrigger = false;
        WeaponAttack = false;
        yield return new WaitForSeconds(0.4f);
        attackable = true;
        
    }
    #endregion
}
