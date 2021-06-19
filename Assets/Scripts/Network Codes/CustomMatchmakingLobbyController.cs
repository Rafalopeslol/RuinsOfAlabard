using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomMatchmakingLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private ServerListController serverListController;

    [SerializeField]
    private GameObject lobbyConnectButton;
    [SerializeField]
    private GameObject lobbyPanel;
    [SerializeField]
    private GameObject mainPanel;
    [SerializeField]
    private GameObject loading;
    [SerializeField]
    private GameObject RoomCreator;
    [SerializeField]
    private GameObject QuitMenu;

    public AudioSource source;
    public AudioClip sound;
    public TMP_InputField playerName;
    private bool canQuit;
    private string roomName;
    private List<RoomInfo> roomListings = new List<RoomInfo>();
    [SerializeField]
    private Transform roomsContainer;
    [SerializeField]
    private GameObject roomListingPrefab;

    public void Start()
    {
        source.clip = sound;
        source.playOnAwake = false;
        Screen.SetResolution(1280, 720, true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        canQuit = true;
    }
    public void Update()
    {
        if (canQuit && Input.GetKey(KeyCode.Escape))
        {
            QuitMenu.SetActive(true);
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        loading.SetActive(false);

        if (PlayerPrefs.HasKey("Username"))
        {
            if(PlayerPrefs.GetString("Username") == "")
            {
                PhotonNetwork.NickName = "Guest" + Random.Range(0, 1000); 
            }
            else
            {
                PhotonNetwork.NickName = PlayerPrefs.GetString("Username");
            }
        }
        else
        {
            //PhotonNetwork.NickName = "Guest" + Random.Range(0, 1000);
        }
        playerName.text = PhotonNetwork.NickName;
    }

    public void PlayerNameUpdate(string name)
    {
        PhotonNetwork.NickName = name;
        PlayerPrefs.SetString("Username", name);
    }

    public void JoinLobby()
    {
        source.PlayOneShot(sound);
        mainPanel.SetActive(false);
        lobbyPanel.SetActive(true);
        PhotonNetwork.JoinLobby();
        canQuit = false;
    }

    public void CreateRoomMenu()
    {
        source.PlayOneShot(sound);
        mainPanel.SetActive(false);
        RoomCreator.SetActive(true);
        PhotonNetwork.JoinLobby();
        canQuit = false;
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int tempIndex;
        foreach (RoomInfo room in roomList)
        {
            Debug.Log(roomListings.Count);
            if(roomListings != null)
            {
                tempIndex = roomListings.FindIndex(ByName(room.Name));
            }
            else
            {
                tempIndex = -1;
            }
            if (tempIndex != -1)
            {
                roomListings.RemoveAt(tempIndex);
                Destroy(roomsContainer.GetChild(tempIndex).gameObject);
            }
            if (room.PlayerCount > 0)
            {
                roomListings.Add(room);
                ListRoom(room);
            }
        }
    }

    static System.Predicate<RoomInfo> ByName(string name)
    {
        return delegate (RoomInfo room)
        {
            return room.Name == name;
        };
    }

    void ListRoom(RoomInfo room)
    {
        if (room.IsOpen && room.IsVisible)
        {
            GameObject tempListing = Instantiate(roomListingPrefab, roomsContainer);
            RoomButton tempButton = tempListing.GetComponent<RoomButton>();
            tempButton.SetRoom(room.Name, room.MaxPlayers, room.PlayerCount, serverListController);
        }
    }

    public void OnRoomNameChanged(string nameIn)
    {
        roomName = nameIn;
    }

    public void CreateRoom()
    {
        RoomOptions roomOpts = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 4, PublishUserId = true };
        PhotonNetwork.CreateRoom(roomName, roomOpts);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Something went wrong");
    }

    public void MatchMakingCancel()
    {
        mainPanel.SetActive(true);
        lobbyPanel.SetActive(false);
        PhotonNetwork.LeaveLobby();
        canQuit = true;
    }

    public void Back()
    {
        lobbyPanel.SetActive(false);
        RoomCreator.SetActive(false);
        mainPanel.SetActive(true);
        PhotonNetwork.LeaveLobby();
        canQuit = true;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void DoNotQuit()
    {
        QuitMenu.SetActive(false);
    }
}
