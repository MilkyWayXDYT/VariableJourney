using UnityEngine;

public class FloorFailure : MonoBehaviour
{
    [SerializeField]
    private GameObject floor;
    [SerializeField]
    private GameObject block;

    private float timer = 0;
    private bool timerStart = false;

    private void Update()
    {
        if (timerStart)
            timer -= Time.deltaTime;
        if (timer < 0)
        {
            floor.SetActive(!floor.activeSelf);
            if (floor.activeSelf)
            {
                block.SetActive(false);
                this.gameObject.SetActive(false);
            }
            timer = 10f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!timerStart)
        {
            timer = 15f;
            timerStart = true;
        }
    }
}
