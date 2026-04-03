using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class CreateDoorFromLevers : MonoBehaviour
{
    [SerializeField]
    private List<Transform> levers;
    [SerializeField]
    private GameObject rightDoorPref;
    [SerializeField]
    private GameObject wrongDoorPref;

    private Transform doorEndPoint;

    public int enableLevers;
    public bool rightDoor = true;


    private void Start()
    {
        levers = GetComponentsInChildren<Transform>().Where(l => l.tag == "Lever").ToList();
        doorEndPoint = GetComponentsInChildren<Transform>().FirstOrDefault(p => p.name == "DoorEnd");
        SetGlitchLever();
    }

    public void SetGlitchLever()
    {
        int glitchLeverNum = Random.Range(0, levers.Count);
        InteractiveObj lever = levers[glitchLeverNum].GetComponent<InteractiveObj>();
        Debug.Log(glitchLeverNum);
        lever.isGlitch = true;
    }

    public void LeverPress()
    {
        if (enableLevers == levers.Count - 1)
        {
            CreateDoor();
        }
    }

    private void CreateDoor()
    {
        Transform door = doorEndPoint.GetComponentInChildren<Transform>();
        Destroy(door.GetComponentsInChildren<Transform>()[1].gameObject);
        rightDoor = true;

        foreach (var lever in levers)
        {
            InteractiveObj leverInteractive = lever.GetComponent<InteractiveObj>();
            if (leverInteractive.objEnable && leverInteractive.isGlitch)
                rightDoor = false;
        }
        if (rightDoor)
            Instantiate(rightDoorPref, doorEndPoint);
        else
            Instantiate(wrongDoorPref, doorEndPoint);
    }
}
