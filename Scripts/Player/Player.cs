using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    //Script nay duoc su dung boi object Main
    public float speed = 150f, maxspeed = 3, jumpPow = 350f, defaultSpeed;
    public bool grounded = true, faceright = true, doubleJump = false, attacktrigger = false, takeDam = false, death = false;
    public Rigidbody2D r2;
    public Animator anim;
    public AudioClip Nhay, phong;
    private AudioSource audioSource;
    public bool diChuyen;
    public GameObject Dash, playerStat, Weapons, gameController;


    void Start()
    {
        r2 = gameObject.GetComponent<Rigidbody2D>();//Lay nhan vat
        playerStat = GameObject.FindGameObjectWithTag("Stat");
        anim = gameObject.GetComponent<Animator>();//Bien chua animation cho Player
        audioSource = gameObject.GetComponent<AudioSource>();//Chua am thanh de chay
        Weapons = GameObject.FindGameObjectWithTag("MainWeapon");
        gameController = GameObject.FindGameObjectWithTag("GameController");
        audioSource.clip = Nhay;//am thanh nhay
        diChuyen = true;//co the di chuyen
        defaultSpeed = speed;//speed ban dau
        speed = defaultSpeed + playerStat.GetComponent<PlayerStat>().spd * 2;//lay toc do di chuyen theo chi so spd
    }


    void Update()
    {
        anim.SetBool("Grounded", grounded);//animation khi dung yen tren mat dat (grounded = true)
        anim.SetFloat("Speed", Mathf.Abs(r2.velocity.x)); // Mathf.abs: tra ve gia tri duong ; r2.velocity.x: toc do hien tai, animation khi chay
        anim.SetBool("AttackTrigger", attacktrigger);
        anim.SetBool("TakeDamage", takeDam);
        anim.SetBool("Death", death);

        if (Input.GetKeyDown(KeyCode.X) && diChuyen) // neu nut an xuong cua nguoi choi la X va dang cho phep di chuyen (diChuyen = true)
        {
            gameObject.GetComponent<GroundCheck>();//Goi ham kiem tra xem Player co dang dung tren mat dat hay khong
            if (grounded)//neu dang dung tren mat dat
            {
                grounded = false;//cho grounded = false tuc la nguoi choi se nhay len khong
                doubleJump = true;//co the nhay tiep lan 2
                audioSource.clip = Nhay;
                audioSource.Play();//Play am thanh khi nhay
                r2.AddForce(Vector2.up * jumpPow);//thay doi vi tri nhan vat len tren dua vao jumpPow
            }
            else//Nguoc lai, Neu khong dung tren mat dat
            {
                if (doubleJump)//neu chua nhay lan 2
                {
                    doubleJump = false;//nhay tiep lan 2 va khong the nhay them nua
                    r2.velocity = new Vector2(r2.velocity.x, 0);
                    audioSource.clip = Nhay;
                    audioSource.Play();
                    r2.AddForce(Vector2.up * jumpPow * 0.8f);
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (diChuyen)//neu cho phep di chuyen (diChuyen = true)
        {
            float h = Input.GetAxis("Horizontal");//Lay thong tin nut bam la nut mui ten (Phai: 1, Trai: -1)
            r2.AddForce(Vector2.right * speed * h);//Thay doi vi tri nhan vat dua vao speed va h

            //Ham gioi han toc do di chuyen
            if (r2.velocity.x > maxspeed) //Gioi han toc do di ve ben phais
                r2.velocity = new Vector2(maxspeed, r2.velocity.y);
            if (r2.velocity.x < -maxspeed)// Gioi han toc do di ve ben trai
                r2.velocity = new Vector2(-maxspeed, r2.velocity.y);

            if (h > 0 && !faceright && !Weapons.GetComponent<WeaponsController>().WeaponAttack)//Neu h > 0 tuc la ben phai va player dang quay ve ben trai va chua trong trang thai tan cong
            {
                Flip();//Goi ham dao chieu 
            }
            if (h < 0 && faceright && !Weapons.GetComponent<WeaponsController>().WeaponAttack)//Neu h < 0 tuc la ben trai va player dang quay ve ben phai va chua trong trang thai tan cong
            {
                Flip();
            }

            if (grounded)
            {
                r2.velocity = new Vector2(r2.velocity.x * 0.7f, r2.velocity.y);
            }

            if (!grounded)
            {
                r2.velocity = new Vector2(r2.velocity.x * 0.7f, r2.velocity.y);
            }


            if (Input.GetKey(KeyCode.LeftShift) && h != 0 && playerStat.GetComponent<PlayerStat>().stamina >= 1)//khi nhan giu Shift se chay nhanh
            {
                playerStat.GetComponent<PlayerStat>().stamina -= 1;
                speed = defaultSpeed + playerStat.GetComponent<PlayerStat>().spd * 2 + 200f;
            }
            else
                speed = defaultSpeed + playerStat.GetComponent<PlayerStat>().spd * 2;
            if (!Input.GetKey(KeyCode.LeftShift))//khi khong nhan shift se tro ve toc do ban dau
            {
                speed = defaultSpeed + playerStat.GetComponent<PlayerStat>().spd * 2;
            }
        }
    }

    public void Flip() // Chuyen huong nhan vat
    {
        faceright = !faceright;
        gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
        Weapons.GetComponent<SpriteRenderer>().flipX = !Weapons.GetComponent<SpriteRenderer>().flipX;//chuyen huong vu khi dang su dung
    }

    #region Nhan Sat Thuong
    public void TakeDamage(int damageAmount)//Nhan sat thuong
    {
        takeDam = true;
        diChuyen = false;
        playerStat.GetComponent<PlayerStat>().hp -= damageAmount;
        StartCoroutine(TakeDam());
    }

    IEnumerator TakeDam()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;//thay doi mau de mo phong nhan sat thuong
        yield return new WaitForSeconds(0.4f);//doi 0.4s
        if (playerStat.GetComponent<PlayerStat>().hp <= 0 && !gameController.GetComponent<GameController>().isGameOver)
        {
            gameController.GetComponent<GameController>().isGameOver = true;
            diChuyen = false;
            death = true;
            gameController.GetComponent<GameController>().fadeOut.SetActive(true);
            yield return new WaitForSeconds(1.3f);
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }
        else
        {
            takeDam = false;
            diChuyen = true;//di chuyen lai binh thuong
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;//tro lai mau nhu cu
        }
    }
    #endregion

}
