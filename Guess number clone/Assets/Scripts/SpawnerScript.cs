using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using UnityEngine.UI;

public class SpawnerScript : MonoBehaviour {

    public Camera cam;
    private float maxWidth;
    public GameObject ball;
    public float zvalue = 95;
    List<GameObject> aliens = new List<GameObject>();
    RandomImageDisplay randomImageObject;
    GameController gc;
    public Text NumberOfImagesLeftText;
    public int numberOfImagesLeft;

    // Use this for initialization
    void Start () {

        gc = FindObjectOfType<GameController>();

        randomImageObject = FindObjectOfType<RandomImageDisplay>();

        numberOfImagesLeft = gc.textureList.Count * gc.maxNumberOfInputsForAnImage;

		if (cam == null)
        {
            cam = Camera.main;
        }

        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
        float ballWidth = ball.GetComponent<Renderer>().bounds.extents.x;
        maxWidth = targetWidth.x - ballWidth - 2;

        
          StartCoroutine(Spawn());

        
        
        

	}


    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2.0f);

            while (gc.validIndices.Count != 0)
            {

                Vector3 spawnPosition = new Vector3(

                    Random.Range(-maxWidth, maxWidth),
                    transform.position.y,
                    zvalue);

                Quaternion spawnRotation = ball.transform.rotation;

                Instantiate(ball, spawnPosition, spawnRotation);

           

                 yield return new WaitForSeconds(Random.Range (3.0f, 6.0f));

            numberOfImagesLeft--;

            if (numberOfImagesLeft <= 0)
                numberOfImagesLeft = 0;

            NumberOfImagesLeftText.text = "Images Left: " + " " + numberOfImagesLeft;

            }

            if (gc.validIndices.Count == 0)
               SceneManager.LoadScene("GameOverScene");
    }

    /*
    private void GetListOfAliens()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allObjects)
        {
            if (go.activeInHierarchy && go.tag == "Alien")
            {
                aliens.Add(go);
            }

            else aliens.Remove(go);

        }

        Debug.Log(aliens.Count);
    }
    */


}
