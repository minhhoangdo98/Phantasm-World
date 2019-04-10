using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    //script duoc dung boi prefab Slash
    public GameObject playerStat, attackEffect;
    public int damageAmount;
    void Start()
    {
        playerStat = GameObject.FindGameObjectWithTag("Stat");
        damageAmount = 5;//Sat thuong mac dinh
    }

    #region Gay sat thuong cho ke dich
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))//neu la ke dich
        {
            Vector3 pos = new Vector3(collision.transform.position.x, collision.transform.position.y);
            GameObject ae = Instantiate(attackEffect, pos, Quaternion.identity, collision.transform) as GameObject;//tao hieu ung danh trung ke dich
            Destroy(ae, 0.5f);
            collision.SendMessage("TakeDamage", damageAmount + playerStat.GetComponent<PlayerStat>().str);//gay sat thuong cho ke dich
        }
    }

    #endregion
}
