using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class RoomButton : MonoBehaviour
{
    private ServerListController _serverListController;
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text sizeText;

    private string roomName;
    private int roomSize = 4;
    private int playerCount;

    public void JoinRoom()
    {
        _serverListController.SetRoomName(roomName, this);
        Selected();
    }

    private void Selected()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Unselected()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void SetRoom(string nameInput, int sizeInput, int countInput, ServerListController serverListController)
    {
        roomName = nameInput;
        sizeInput = roomSize;
        playerCount = countInput;
        nameText.text = nameInput;
        sizeText.text = countInput + "/" + sizeInput;

        _serverListController = serverListController;
    }

}
