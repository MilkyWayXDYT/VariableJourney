using UnityEngine;

public class Bit : MonoBehaviour
{
    [SerializeField]
    private int bitValue;

    private BitCounter counter;
    private SetRoomsCount roomsCount;

    private void Start()
    {
        counter = GameObject.Find("BitCounter").GetComponent<BitCounter>();
        roomsCount = GameObject.Find("DontDestroy").GetComponent<SetRoomsCount>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        roomsCount.bitString += bitValue;
        counter.Assembling();
        Destroy(gameObject);
    }
}
