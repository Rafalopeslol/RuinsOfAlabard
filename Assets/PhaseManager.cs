using Photon.Realtime;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhaseManager : MonoBehaviourPunCallbacks
{
    public static PhaseManager Instance;
    public bool _hasGameEnded = false;
    public GameObject WinScreen;
    public GameObject LoseScreen;
    

    private bool _hasSended = false;

    private PhotonView _photonView;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        _photonView = GetComponent<PhotonView>();
    }

    public void PlayerWins(string userId)
    {
        if (_hasSended) return;

        _photonView.RPC("RPC_PlayerWins", RpcTarget.AllViaServer,userId);
    }

    [PunRPC]
    void RPC_PlayerWins(string userId)
    {
        Debug.Log(PhotonNetwork.LocalPlayer.UserId);
        Debug.Log(userId);
        if(userId == PhotonNetwork.LocalPlayer.UserId && !_hasGameEnded)
        {
            Debug.Log("You win!");
            WinScreen.SetActive(true);
        }
        else
        {
            Debug.Log("You lose!");
            LoseScreen.SetActive(true);
           
        }
        StartCoroutine(Timer());
        _hasGameEnded = true;
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(5f);
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Lobby");
    }
}
