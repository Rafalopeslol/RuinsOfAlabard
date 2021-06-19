using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public GameObject[] Characters;
    public int selectedCharacter;

    public void NextCharacter()
    {
        Characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % Characters.Length;
        Characters[selectedCharacter].SetActive(true);
    }

    public void PreviousCharacter()
    {
        Characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter += Characters.Length;
        }
        Characters[selectedCharacter].SetActive(true);
    }

    public void Confirm()
    {
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
    }
}
