using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TheHarpoon : MonoBehaviour
{
    private Transform player;
    private bool back = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ToBeAttracted")
        {

        }
        else if (collision.gameObject.tag == "Attracted")
        {

        }
        else if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<HarpoonSpawn>().sent = false;
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Ground")
        {
            back = true;
        }
    }

    private void Update()
    {
        if (back) 
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, 10 * Time.deltaTime);
        }
    }
}
