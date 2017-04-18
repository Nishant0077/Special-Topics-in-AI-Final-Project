using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using System.Linq;
using System.Text;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{

    static int id = 1;
    public InputField input;

    public Texture2D[] textures;
    // MyImageClass myImageObject;
    MouseManager mm;
    RandomImageDisplay randomImageObject;
    Dictionary<int, string> goldIndexAnswerDict = new Dictionary<int, string>();
    public Dictionary<int, int> indexCount = new Dictionary<int, int>();
    Dictionary<int, List<string>> indexAnswerDict = new Dictionary<int, List<string>>();
    public GameObject currentSelection;
    public Sprite currentImageFromSelection;
    public int maxNumberOfInputsForAnImage = 3;
    public List<Texture2D> textureList = new List<Texture2D>();
    public Dictionary<int, Texture2D> indexImageDict = new Dictionary<int, Texture2D>();
    public List<int> validIndices = new List<int>();
    public Text scoreText;
    public int reward;
    public int currentScore = 0;
    private int correctGoldAnswerReward = 20;
    private int wrongGoldAnswerReward = -10;
    private int normalReward = 10;
    private int uniformAnswerReward = 15;
    Dictionary<int, string> analyticsDict = new Dictionary<int, string>();
    public string idOfPlayer;
    private AudioClip explosionSound;
    AudioSource audio;
    public float volume = 1;
    ExplosionEffectScript explosionScriptObject;
    float durationOfExplosion = 2;
  


    void Start()
    {

        textures = Resources.LoadAll<Texture2D>("");
        randomImageObject = GameObject.FindObjectOfType<RandomImageDisplay>();
        explosionScriptObject = GameObject.FindObjectOfType<ExplosionEffectScript>();
        audio = GameObject.FindGameObjectsWithTag("ExplosionSound")[0].GetComponent<AudioSource>();
        Debug.Log(audio);

        explosionSound = audio.clip;


        for (int i = 0; i < textures.Length; i++)
        {
            textureList.Add(textures[i]);
        }

        for (int i = 0; i < textures.Length; i++)
        {
            validIndices.Add(i);
        }

        for (int i = 0; i < textures.Length; i++)
        {
            indexCount.Add(i, 0);
        }

        for (int i = 0; i < textures.Length; i++)
        {
            indexImageDict.Add(i, textures[i]);
        }

        

        // defining the gold images 

        AddAnswerToGoldDictionary(17, "ensure");
        AddAnswerToGoldDictionary(19, "that");
        AddAnswerToGoldDictionary(4, "just");
        AddAnswerToGoldDictionary(10, "pictures");


    }

    public bool isImageGold(int index)
    {

        if (goldIndexAnswerDict.ContainsKey(index))
        {
            return true;
        }


        else return false;
    }

    public void AddAnswerToGoldDictionary(int index, string answer)
    {
        goldIndexAnswerDict[index] = answer;
    }

    public void AddToIndexListDictionary(int myKey, string myVal)
    {

        if (indexAnswerDict.ContainsKey(myKey))
        {
            indexAnswerDict[myKey].Add(myVal);
        }

        else
        {
            indexAnswerDict[myKey] = new List<string>
                {
                    myVal
                };
        }
    }

    public bool isCorrectAnswer(int index, string answer)
    {
        if (goldIndexAnswerDict.ContainsKey(index) && goldIndexAnswerDict[index].Equals(answer))
        {
            return true;
        }


        return false;
    }

    public void SetCurrentSelectedObject(GameObject go)
    {
        currentSelection = go;
       
    }

    public int getIndexOfCurrentSelection()
    {
        for (int i = 0; i < textures.Length; i++)
        {
            if (currentSelection != null && currentImageFromSelection.texture == (textures[i]))
            {
                return i;
            }
        }

        return -1;
    }

    public bool AllAnswersDifferent(int index)
    {
        List<string> theList = indexAnswerDict[index];

        bool isUnique = theList.Distinct().Count() == theList.Count();

        if (isUnique)
            return true;

        else return false;
    }

    public string GetMostRepeatedAnswer(int index)
    {
        List<string> list = indexAnswerDict[index];

        string most = list.GroupBy(i => i).OrderByDescending(grp => grp.Count()).Select(grp => grp.Key).First();
        return most;
        
    }

    public bool AreAllAnswersSame(int index)
    {
        List<string> list = indexAnswerDict[index];

        bool allAreSame = list.All(x => x == list.First());
        return allAreSame;
        
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("FinalScore", currentScore);
    }

    void Awake()
    {
        input = GameObject.Find("InputField").GetComponent<InputField>();
        idOfPlayer = PlayerPrefs.GetString("PlayerID");

    }

   

    public void GetInput(string guess)
    {


        Debug.Log("You entered " + guess);
        input.text = "";   // reset the input text
        
        // checking if answer is correct or wrong when gold image is displayed
        if (isImageGold(getIndexOfCurrentSelection()))
        {

            if (isCorrectAnswer(getIndexOfCurrentSelection(), guess))
            {
                Debug.Log("CORRECT!!!!!!!!");
            }

            else if (!isCorrectAnswer(getIndexOfCurrentSelection(), guess))
                Debug.Log("WRONG!!!!!!!!!");

        }

        // award score when correct answer is given for gold image
        if (isImageGold(getIndexOfCurrentSelection()) && isCorrectAnswer(getIndexOfCurrentSelection(), guess))
        {
            reward = correctGoldAnswerReward;
        }

        // reward when answer is wrong for current gold image
        if (isImageGold(getIndexOfCurrentSelection()) && !isCorrectAnswer(getIndexOfCurrentSelection(), guess))
        {
            reward = wrongGoldAnswerReward;
        }

        // reward when image is not gold
        if (!isImageGold(getIndexOfCurrentSelection()))
        {
            reward = normalReward;
        }
        
        // reward when no image has been selected currently
        if (currentSelection.Equals(null))
        {
            reward = 0;
        }       

        //actions to take when user input is received
        AddToIndexListDictionary(getIndexOfCurrentSelection(), guess);  // add the answer to dictionary


        
        Debug.Log(explosionSound);
        currentSelection.gameObject.GetComponent<ExplosionEffectScript>().Explode();
        audio.PlayOneShot(explosionSound);
        currentSelection.GetComponent<Renderer>().enabled = false;
        GameObject.FindGameObjectWithTag("Spaceship").gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Indicator").gameObject.SetActive(false);
        
        Destroy(currentSelection);


        // rewards when the image has been displayed for given max number of times
        // and the user's answers are compared for that image
        if (indexAnswerDict[getIndexOfCurrentSelection()].Count.Equals(maxNumberOfInputsForAnImage))
        {
            if (AllAnswersDifferent(getIndexOfCurrentSelection()) && !isImageGold(getIndexOfCurrentSelection()))
            {
                Debug.Log("ALL ANSWERS ARE DIFFERENT");
                reward = -1 * (reward * (maxNumberOfInputsForAnImage - 1));
            }

            if (AreAllAnswersSame(getIndexOfCurrentSelection()) && indexAnswerDict.Count.Equals(maxNumberOfInputsForAnImage))
            {
                Debug.Log("ALL ANSWERS ARE SAME");

                reward = reward + uniformAnswerReward;
                Debug.Log("THIS IS THE FINAL ANSWER" + GetMostRepeatedAnswer(getIndexOfCurrentSelection()));
            }

           

        }

        currentScore = currentScore + reward;          // update the current score
        scoreText.text = "Score: " + currentScore;     // display the current score     



        // send data to analytics dashboard

        

        Analytics.CustomEvent(idOfPlayer, new Dictionary<string, object>
        {
            {getIndexOfCurrentSelection().ToString(), guess + " " + currentScore}


        });

      
        //Debug.Log(Analytics.CustomEvent("TestEvent", new Dictionary<string, object> { { "firstObject", 10 } }));

    }
}