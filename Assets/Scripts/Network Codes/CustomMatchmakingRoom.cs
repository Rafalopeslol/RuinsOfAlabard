using JetBrains.Annotations;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomMatchmakingRoom : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int multiPlayerSceneIndex;
    [SerializeField]
    private GameObject lobbyPanel;
    [SerializeField]
    private GameObject roomPanel;
    [SerializeField]
    private GameObject roomCreator;
    [SerializeField]
    private GameObject mainPanel;
    public GameObject[] playerImage;
    public GameObject[] playerNicknameObject;
    public TMP_Text[] playerNicknameText;

    [SerializeField]
    private GameObject options;
    [SerializeField]
    private GameObject status;
    [SerializeField]
    private GameObject startButton;
    [SerializeField]
    private Transform playersContainer;
    [SerializeField]
    private GameObject playerListingPrefab;
    [SerializeField]
    private TMP_Text roomNameDisplay;

    private void Update()
    {
        
    }

    public override void OnJoinedRoom()
    {
        roomPanel.SetActive(true);
        lobbyPanel.SetActive(false);
        roomCreator.SetActive(false);
        roomNameDisplay.text = PhotonNetwork.CurrentRoom.Name;

        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }

        UpdatePlayersInUI();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayersInUI();
    }

    private void UpdatePlayersInUI()
    {
        var players = PhotonNetwork.PlayerListOthers;

        for (int i = 0; i < 4; i++)
        {
           playerImage[i].SetActive(false);
           playerNicknameObject[i].SetActive(false);
        }

        playerImage[0].SetActive(true);
        playerNicknameObject[0].SetActive(true);
        playerNicknameText[0].text = PhotonNetwork.LocalPlayer.NickName;

        for (int i = 0; i < players.Length; i++)
        {
            playerImage[i + 1].SetActive(true);
            playerNicknameObject[i + 1].SetActive(true);
            playerNicknameText[i + 1].text = players[i].NickName;
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
        }

        UpdatePlayersInUI();
    }

    public void StarGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.LoadLevel(multiPlayerSceneIndex);
        }
    }

    IEnumerator rejoinLobby()
    {
        yield return new WaitForSeconds(1);
        PhotonNetwork.JoinLobby();
    }

    public void Back()
    {
        lobbyPanel.SetActive(true);
        roomPanel.SetActive(false);
        PhotonNetwork.LeaveRoom();
        //StartCoroutine(rejoinLobby());
    }
    public void Options()
    {
        options.SetActive(true);
    }
    public void OptionsBack()
    {
        options.SetActive(false);
    }

    public void Status()
    {
        status.SetActive(true);
    }
    
    public void StatusBack()
    {
        status.SetActive(false);
    }
}
