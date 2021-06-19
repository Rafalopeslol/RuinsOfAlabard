using Photon.Pun;
using System.IO;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform spawn;
    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
        if (selectedCharacter == 0)
        {
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PlayerTest"), spawn.position, Quaternion.identity);
        }
        else if (selectedCharacter == 1)
        {
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Gladiator"), spawn.position, Quaternion.identity);
        }
        else if (selectedCharacter == 2)
        {
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Viking"), spawn.position, Quaternion.identity);
        }
    }

 
}
