using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC : MonoBehaviour
{
    public enum state
    {
        star, notComplete, complete
    }

    public state _state;


    [Header("對話")]
    public string star = "PLEASE HELP ME GET 4 PUMPKINPIE";
    public string notcomplete = "YOU'R NOT GET ALL MATE";
    public string complete = "THANK YOU MATE";
    [Header("速度")]
    public float talkspeed = 0.1f;
    [Header("任務")]
    public bool mission_complete = false;
    public int count_player = 0;
    public int finish = 4;
    [Header("UI")]
    public GameObject objcan;
    public Text textSay;

    public static NPC food;

    public GameObject END;

    private void Start()
    {
        food = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            Say();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Sayout();
    }

    /// <summary>
    /// 對話
    /// </summary>
    void Say()
    {
        objcan.SetActive(true);
        StopAllCoroutines();

        if (count_player >= finish)
        {
            _state = state.complete;

            Invoke("Finish", 2f);
        } 

        switch (_state)
        {
            case state.star:
                StartCoroutine(ShowDialog(star));
                _state = state.notComplete;
                break;
            case state.notComplete:
                StartCoroutine(ShowDialog(notcomplete));
                break;
            case state.complete:
                StartCoroutine(ShowDialog(complete));
                break;
        }
    }

    private IEnumerator ShowDialog(string say)
    {
        textSay.text = "";

        for (int i = 0; i < say.Length; i++)
        {
            textSay.text += say[i].ToString();
            yield return new WaitForSeconds(talkspeed);
        }
    }

    /// <summary>
    /// 離開對話
    /// </summary>
    void Sayout()
    {
        StopAllCoroutines();
        objcan.SetActive(false);
    }

    void Finish()
    {
        END.SetActive(true);

        Destroy(player.play);
    }
}
