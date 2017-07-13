using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Canvas_controller : MonoBehaviour
{

    public Text[] texts;
    public Text Score_text;
    public static string per_String;

    public static int per = 100;
    public static int life_num = 5;
    public static int attempts = 0;
    public static int Score = 0;

    public GameObject panel;
    public static bool gameOver = false;

    // Use this for initialization
    void Start()
    {
        Debug.Log("STartt");
        texts = GameObject.FindObjectsOfType<Text>();
        panel.SetActive(false);
        Score = 0;
        attempts = 0;
        life_num = 5;
        Size.Amount = 0;
        gameOver = false;

    }

    // Update is called once per frame
    void Update()
    {
        texts[2].text = "Attempts:" + attempts;
        texts[0].text = "Area Cut: " + per_String;
        texts[1].text = "Life: " + life_num;
        Score_text.text = "Game Over \n Your Score is:" + Score;


        if (life_num <= 0 || Size.Amount >= 90)
        {
            Debug.Log("ShowPanel panel");
            panel.SetActive(true);
            ShowPanel();
        }
        
    }


    public void score_Calculator(int attempt)
    {
        if (life_num > 0)
        {
            if (attempt > 2)
            {
                Score = 9000 / attempt;
            }
            else
            {
                Score = 3000;
            }
        }
        else
        {
            Score = 0;
        }
    }


    public void ShowPanel()
    {
        
            score_Calculator(attempts);
        gameOver = true;
        
    }
    public void ButtonActions(string call)
    {
        if (call.ToLower() .Equals("retry"))
        {
            Application.LoadLevel(0);
        }
        else
        {
            Application.Quit();
        }
    }
}
