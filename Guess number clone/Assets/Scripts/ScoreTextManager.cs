using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using System.Linq;
using System.Text;
using UnityEngine.EventSystems;

public class ScoreTextManager : MonoBehaviour {

    RandomImageDisplay randomImageScriptObject;
    public int score;
    public Text scoreText;

	// Use this for initialization
	void Start () {

        randomImageScriptObject = GetComponent<RandomImageDisplay>();
        score = PlayerPrefs.GetInt("FinalScore");
        scoreText.text = "This is your score: " + score;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
