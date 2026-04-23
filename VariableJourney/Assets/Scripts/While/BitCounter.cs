using UnityEngine;

public class BitCounter : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer[] lamps;
    [SerializeField]
    private Material bitAssembledMat;
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private SetRoomsCount roomsCount;

    private int bitAssembled = 0;

    public void Assembling()
    {
        bitAssembled++;
        lamps[bitAssembled - 1].material = bitAssembledMat;
        if (bitAssembled == 5)
        {
            DoorOpen();
            roomsCount.NumberConversion();
        }
    }

    private void DoorOpen()
    {
        door.SetActive(false);
    }
}
