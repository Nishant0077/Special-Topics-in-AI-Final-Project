using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetUserID : MonoBehaviour {

    public static string playerID;
    private InputField idInputField;

     void Awake()
    {
        idInputField = GameObject.Find("UserIDInputField").GetComponent<InputField>();
    }

    

	public void GetUserIDFunction(string id)
    {
        Debug.Log(id);
        PlayerPrefs.SetString("PlayerID", id);
        idInputField.text = "";
        SceneManager.LoadScene("scene0");
    }
}
