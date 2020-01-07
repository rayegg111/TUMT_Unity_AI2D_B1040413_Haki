using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    [Header("數值")]
    public int speed = 150;
    public float jump = 2500;
    public bool pass = false;
    public bool isGround = false;

    private Rigidbody2D r2d;
    private Transform tra;
    public Animator ani;
    [Header("血量")]
    public float hp = 100;
    public Image hpBar;
    private float maxHP;

    public GameObject END;

    public static player play;

    private void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        tra = GetComponent<Transform>();

        maxHP = hp;
        play = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) Turn(0);
        if (Input.GetKeyDown(KeyCode.A)) Turn(180);
    }

    private void FixedUpdate()
    {
        Walk();
        Jump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGround = true;
        Debug.Log("碰到" + collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "food")
        {
            Destroy(collision.gameObject);

            NPC.food.count_player += 1;
        }
    }

    /// <summary>
    /// 走路
    /// </summary>
    void Walk()
    {
        ani.SetBool("walk", Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0);
        r2d.AddForce(new Vector2(speed * (Input.GetAxis("Horizontal")), 0));
    }
    /// <summary>
    /// 跳躍
    /// </summary>
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            isGround = false;
            r2d.AddForce(new Vector2(0, jump));

            ani.SetTrigger("jump");
        }
    }
    /// <summary>
    /// 轉向
    /// </summary>
    /// <param name="direction">控制方向，左：180，右：0 </param>
    void Turn(int direction)
    {
        tra.eulerAngles = new Vector3(0, direction, 0);
    }
    /// <summary>
    /// 傷害
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(float damage)
    {
        hp -= damage;
        hpBar.fillAmount = hp / maxHP;

        //死亡
        if (hp <= 0)
        {
            END.SetActive(true);

            Destroy(this);          //摧毀腳本
        }

    }
}
