using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;

public class RandomImageDisplay : MonoBehaviour
{

   public int i = 0;
    public Texture2D[] textures;
    public GameController gc;
    public int numberOfImagesToDisplay;
    public bool gameInProgress = true;
    public int finalScore = 0;
    public float imageHeight = 1;
    public float imageWidth = 1;
    



    void Start()
    {
      
        gc = GameObject.FindObjectOfType<GameController>();
        numberOfImagesToDisplay = gc.textureList.Count;
                
            changeTexture();       
    }

   


   public void changeTexture()
    {             

       // Debug.Log(gc.validIndices.Count);

        // remove index when the image has been displayed the given maximum number of times
        for (int j = 0; j < gc.indexCount.Count; j++)
        {

            if (gc.indexCount[j].Equals(gc.maxNumberOfInputsForAnImage))
            {                              

                Texture2D imageToStop = gc.indexImageDict[j];
                int indexToRemove = j;

                
                gc.validIndices.Remove(indexToRemove);

                gc.indexCount[j] = -1;               
                
            }

           // NumberOfImagesLeftText.text = "Number of Images Left: " + " " + gc.textureList.Count;

        }

        // stop the display
        if (gc.validIndices.Count.Equals(0))
        {
            Debug.Log(gc.textureList.Count + "No elements left!!!!!!!!");
            gc.SaveScore();
            gameInProgress = false;
                       
            


        }

        //gc.validIndices.Sort();
        if (gameInProgress == true)
        {
            i = gc.validIndices[Random.Range(0, gc.validIndices.Count)];

            Rect rect = new Rect(0, 0, gc.textureList[i].width, gc.textureList[i].height);
            
            this.GetComponent<SpriteRenderer>().sprite = Sprite.Create(gc.textureList[i], rect, new Vector2(0.5f, 0.5f), 200);
            


            gc.indexCount[i]++;
            

            //index of the current image being displayed
            Debug.Log(i);
            
           // NumberOfImagesLeftText.text = "Images Left: " + " " + numberOfImagesToDisplay;


        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
    }
}
