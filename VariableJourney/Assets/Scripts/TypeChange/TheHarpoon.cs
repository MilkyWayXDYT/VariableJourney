using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TheHarpoon : MonoBehaviour
{
    private Transform player;
    private bool back = false;
    private Transform joinObj; 

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ToBeAttracted")
        {
            var joint = collision.gameObject.AddComponent<CharacterJoint>();
            joint.connectedBody = this.GetComponent<Rigidbody>();
            joinObj = collision.transform;
        }
        else if (collision.gameObject.tag == "Attracted")
        {
            var joint = this.AddComponent<CharacterJoint>();
            joint.connectedBody = collision.gameObject.GetComponent<Rigidbody>();
            joinObj = collision.transform;
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
            transform.position = Vector3.MoveTowards(transform.position, player.position, 10 * Time.deltaTime);

        if (joinObj && joinObj.tag == "ToBeAttracted")
        {
            joinObj.position = Vector3.MoveTowards(joinObj.position, player.position, 10 * Time.deltaTime / joinObj.GetComponent<Rigidbody>().mass);
        }
        else if (joinObj && joinObj.tag == "Attracted")
        {
            player.position = Vector3.MoveTowards(player.position, joinObj.position, 10 * Time.deltaTime / player.GetComponent<Rigidbody>().mass);
        }

        if (joinObj && Vector3.Distance(player.position, joinObj.position) < 2.0f)
        {
            player.GetComponent<HarpoonSpawn>().sent = false;
            Destroy(gameObject);
        }
    }
}
