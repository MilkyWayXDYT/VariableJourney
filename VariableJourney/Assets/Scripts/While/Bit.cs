using UnityEngine;

public class Bit : MonoBehaviour
{
    private BitCounter counter;

    private void Start()
    {
        counter = GameObject.Find("BitCounter").GetComponent<BitCounter>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        counter.Assembling();
        Destroy(gameObject);
    }
}
