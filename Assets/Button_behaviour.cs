using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_behaviour : MonoBehaviour {

    public Button button;
    public Text score_text;
    public int value;

    private int score;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(AddPoint);
        button.gameObject.SetActive(false);
        value = 1;
        score = 0;
        score_text.text = "Score: " + score.ToString();
        InvokeRepeating("PopUp", 1.0f, 3.0f);
    }

    void PopUp()
    {
        button.gameObject.SetActive(true);
    }

    void AddPoint()
    {
        score += value;
        score_text.text = "Score: " + score.ToString();
        button.gameObject.SetActive(false);
    }
}
