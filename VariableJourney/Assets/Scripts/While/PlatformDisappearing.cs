using UnityEngine;

public class PlatformDisappearing : MonoBehaviour
{
    public float defTimer = 1;

    private BoxCollider platformCollider;
    private MeshRenderer platformMesh;

    private float timer;
    private bool timerIsRunning = false;
    private bool platformEnable = true;

    private void Update()
    {
        if (timerIsRunning)
        {
            timer -= Time.deltaTime;
            if (timer <= 0 && platformEnable)
                Disappearance();
            else if (timer <= 0 && !platformEnable)
                Appearance();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            timerIsRunning = true;
            timer = 1f;
        }
    }

    private void Disappearance()
    {
        platformCollider =  gameObject.GetComponent<BoxCollider>();
        platformMesh = gameObject.GetComponent<MeshRenderer>();

        platformMesh.enabled = false;
        platformCollider.enabled = false;
        platformEnable = false;

        timer = 10f;
    }

    private void Appearance()
    {
        platformCollider = gameObject.GetComponent<BoxCollider>();
        platformMesh = gameObject.GetComponent<MeshRenderer>();

        platformMesh.enabled = true;
        platformCollider.enabled = true;
        platformEnable = true;
        timerIsRunning = false;
    }
}
