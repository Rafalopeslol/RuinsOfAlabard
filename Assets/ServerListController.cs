using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerListController : MonoBehaviour
{
    private string _roomNameThatPlayerWantGo = "";
    private RoomButton _actualRoomButtonSelected;

    public void SetRoomName(string roomName, RoomButton myRoomButton)
    {
        _roomNameThatPlayerWantGo = roomName;

        if (_actualRoomButtonSelected != null) {
            _actualRoomButtonSelected.Unselected();
        }

        _actualRoomButtonSelected = myRoomButton;
    }

    public void GoToRoomChoosed()
    {
        if(_roomNameThatPlayerWantGo == "")
        {
            Debug.Log("Nenhuma sala selecionada!");
            return;
        }

        PhotonNetwork.JoinRoom(_roomNameThatPlayerWantGo);
    }
}
