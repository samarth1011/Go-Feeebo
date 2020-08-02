using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class HighScoreText : MonoBehaviour
{
    Text highscore;
    // Start is called before the first frame update
    void OnEnable()
    {
        highscore = GetComponent<Text>();
        highscore.text = "HIGH SCORE:" + PlayerPrefs.GetInt("HighScore").ToString();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
