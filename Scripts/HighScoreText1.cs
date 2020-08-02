using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class HighScoreText1 : MonoBehaviour
{
    Text highscore1; 
    // Start is called before the first frame update
    void OnEnable()
    {
        highscore1 = GetComponent<Text>();
        highscore1.text = "HIGH SCORE:"+PlayerPrefs.GetInt("HighScore1").ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
