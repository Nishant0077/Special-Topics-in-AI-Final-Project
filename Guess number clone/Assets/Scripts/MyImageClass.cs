using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyImageClass : MonoBehaviour {

    private Texture2D theImage;
    private bool isGoldImage = false;
    private string answer = "";
    private int index;
    private int numberOfTimesDisplayed = 0;

    public void SetImage(Texture2D myTexture)
    {
        this.theImage = myTexture;
    }

    public void SetIsGoldImage()
    {
        this.isGoldImage = true;
    }

    public void SetAnswer(string myAnswer)
    {
        this.answer = myAnswer;
    }

    public void SetIndex(int myIndex)
    {
        this.index = myIndex;
    }

    public void IncrementNumberOfTimesDisplayed()
    {
        this.numberOfTimesDisplayed++;
    }

    public Texture2D GetImage()
    {
        return this.theImage;
    }

    public bool GetIsGoldImage()
    {
        return this.isGoldImage;
    }

    public string GetAnswer()
    {
        return this.answer;
    }

    public int GetIndex()
    {
       return this.index;
    }

    public int GetNumberOfTimesDisplayed()
    {
        return this.numberOfTimesDisplayed;
    }

    public MyImageClass(Texture2D anImage, bool isGold, string answer, int index, int numberOfTimesDisplayed)
    {
        this.theImage = anImage;
        this.isGoldImage = isGold;
        this.answer = answer;
        this.index = index;
        this.numberOfTimesDisplayed = numberOfTimesDisplayed;
    }

}
