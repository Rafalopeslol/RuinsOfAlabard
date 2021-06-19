using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private PhotonView View;
    private MeshRenderer render;
    private MeshCollider col;
    public float firstTime,secondTime, thirdTime;

    private Animator _animator;


    void Start()
    {
        View = GetComponent<PhotonView>();
        render = GetComponent<MeshRenderer>();
        col = GetComponent<MeshCollider>();
        _animator = GetComponent<Animator>();

        if(!PhotonNetwork.IsMasterClient) return;

        View.RPC("Deactivate", RpcTarget.AllBuffered);
    }

    private IEnumerator Countdown()
    {
        render.enabled = true;
        col.enabled = true;

        yield return new WaitForSeconds(firstTime);

        _animator.SetBool("Shake", true);

        yield return new WaitForSeconds(secondTime);

        render.enabled = false;
        col.enabled = false;

        _animator.SetBool("Shake", false);
        yield return new WaitForSeconds(thirdTime);

        if(!PhotonNetwork.IsMasterClient) yield break;

        View.RPC("Deactivate", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void Deactivate()
    {
        StartCoroutine(Countdown());
    }
}
