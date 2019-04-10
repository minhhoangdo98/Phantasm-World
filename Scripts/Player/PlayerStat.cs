using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    //script duoc dung boi object PlayerStat
    public int str, spd, vit, level, hp, gold, stamina;
    public GameObject healEffect;
    public Text healthText, staminaText, goldText;
    bool hoiSta = true;
    private void Start()
    {
        hp = 100 + vit * 25;//dat gia tri cho hp
        stamina = 100 + vit * 10;
    }

    private void Update()
    {
        int maxStamina = 100 + vit * 10;
        Mathf.Clamp(stamina, 0, maxStamina);
        healthText.text = hp.ToString();//hien text bieu thi mau cua nguoi choi len man hinh
        staminaText.text = stamina.ToString();//hien text bieu thi suc cua nguoi choi len man hinh
        goldText.text = gold.ToString();//hien text bieu thi vang len man hinh
        if (stamina < maxStamina && hoiSta)//Tu dong hoi phuc Stamina
        {
            StartCoroutine(hoiStamina());
        }

    }

    public void UpdateHeal()//hoi mau va stamina
    {
        GameObject he = Instantiate(healEffect, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        Destroy(he, 1f);
        hp = 100 + vit * 25;
        stamina = 100 + vit * 10;
    }

    IEnumerator hoiStamina()
    {
        hoiSta = false;
        yield return new WaitForSeconds(0.05f);//Thoi gian delay hoi 
        if (spd <= 20)
            yield return new WaitForSeconds(0.2f - (float)spd / 100);//Thoi gian delay anh huong boi speed
        stamina++;
        hoiSta = true;
    }
}
