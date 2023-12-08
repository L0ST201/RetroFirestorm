using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerNameManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInput;

    void Start()
    {
        if (usernameInput == null)
        {
            Debug.LogError("Username input field is not assigned!");
            return;
        }

        InitializeUsername();
    }

    private void InitializeUsername()
    {
        string defaultUsername = GenerateDefaultUsername();
        string savedUsername = PlayerPrefs.GetString("username", defaultUsername);
        SetUsername(savedUsername);
    }

    private string GenerateDefaultUsername()
    {
        return "Player" + Random.Range(0, 10000).ToString("0000");
    }

    private void SetUsername(string username)
    {
        PhotonNetwork.NickName = username;
        usernameInput.text = username;
        PlayerPrefs.SetString("username", username);
    }

    public void OnUsernameInputValueChanged()
    {
        SetUsername(usernameInput.text);
    }
}
