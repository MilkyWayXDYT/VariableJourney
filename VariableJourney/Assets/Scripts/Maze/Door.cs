using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Material gMat;
    [SerializeField] private Material rMat;

    private bool isOpen = false;

    public void DoorAction()
    {
        isOpen = ! isOpen;

        if (isOpen)
            Open();
        else
            Close();
    }

    private void Open()
    {
        transform.position += Vector3.up * 3;
        GetComponent<MeshRenderer>().material = gMat;
    }

    private void Close()
    {
        transform.position -= Vector3.up * 3;
        GetComponent<MeshRenderer>().material = rMat;
    }
}
