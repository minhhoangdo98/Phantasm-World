using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSide : MonoBehaviour
{
    //Script duoc dung boi object Left trong object Enemy
    public GameObject enemy;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && enemy.GetComponent<EnemyController>().diChuyen)//Neu nguoi choi vao tam nhin va cho phep di chuyen
        {
            if (enemy.GetComponent<EnemyController>().faceRight)//Neu dang quay ben phai
            {
                enemy.GetComponent<SpriteRenderer>().flipX = !enemy.GetComponent<SpriteRenderer>().flipX;//Quay theo huong nguoc lai
                enemy.GetComponent<EnemyController>().faceRight = !enemy.GetComponent<EnemyController>().faceRight;
            }
            enemy.GetComponent<Transform>().position= Vector2.MoveTowards(enemy.GetComponent<Transform>().position, collision.GetComponent<Transform>().position, enemy.GetComponent<EnemyController>().speed * Time.deltaTime);//duoi theo nguoi choi
        }
    }
}
