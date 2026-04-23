using System;
using UnityEngine;

public class SetRoomsCount : MonoBehaviour
{
    public string bitString = "";
    public int roomsCount;

    public void NumberConversion()
    {
        roomsCount = Convert.ToInt32(bitString, 2);
        DontDestroyOnLoad(this.gameObject);
    }
}
