using UnityEngine;

public class Die_re : MonoBehaviour
{
    public int damage = 100;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            collision.gameObject.GetComponent<player>().Damage(damage);
        }
    }
}
