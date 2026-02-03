using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    public float rotate;

    public enum InversionX { Disabled = 0, Enabled = 1};
    public enum InversionY { Disabled = 0, Enabled = 1};

    [Header("General")]
    public float sensitivity = 2; // чувствительность мыши
    public float distance = 5; // расстояние между камерой и игроком
    public float height = 2.3f;

    [Header("Over The Shoulder")]
    public float offsetPosition; // смещение камеры вправо или влево

    [Header("Clamp Angle")]
    public float minY = 15f; // ограничение углов при наклоне
    public float maxY = 15f;

    [Header("Invert")]
    public InversionX inversionX = InversionX.Disabled;
    public InversionY inversionY = InversionY.Disabled;

    private float rotationY;
    private int inversY, inversX;
    private Transform player;
    private PlayerInput playerInput;
    private InputAction cameraInput;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameObject.tag = "MainCamera";
        playerInput = player.GetComponent<PlayerInput>();
        cameraInput = playerInput.actions.FindAction("Look");
    }

    // проверка, есть ли на пути луча, от игрока до камеры, какое-либо препятствие
    Vector3 PositionCorrection(Vector3 target, Vector3 position)
    {
        RaycastHit hit;
        Debug.DrawLine(target, position, Color.blue);
        if (Physics.Linecast(target, position, out hit))
        {
            float tempDistance = Vector3.Distance(target, hit.point);
            Vector3 pos = target - (transform.rotation * Vector3.forward * tempDistance);
            position = new Vector3(pos.x, position.y, pos.z);
        }
        return position;
    }

    void LateUpdate()
    {
        if (player)
        {
            if (inversionX == InversionX.Disabled) inversX = 1; else inversX = -1;
            if (inversionY == InversionY.Disabled) inversY = -1; else inversY = 1;

            // вращение камеры вокруг игрока
            transform.RotateAround(player.position, Vector3.up, cameraInput.ReadValue<Vector2>().x * sensitivity * inversX);

            // поворот камеры по оси X
            rotationY += cameraInput.ReadValue<Vector2>().y * sensitivity;
            rotationY = Mathf.Clamp(rotationY, -Mathf.Abs(minY), Mathf.Abs(maxY));
            float desiredYaw = transform.eulerAngles.y;           
            Quaternion desired = Quaternion.Euler(rotationY, desiredYaw * inversY, 0);

            // установка камеры на указанной дистанции
            Vector3 targetPosition =
                player.position
                + Vector3.up * height
                - desired * Vector3.forward * distance
                + desired * Vector3.right * offsetPosition;

            targetPosition = PositionCorrection(player.position + Vector3.up * height, targetPosition);
            transform.position = targetPosition;
            transform.rotation = desired;
        }
    }
}
