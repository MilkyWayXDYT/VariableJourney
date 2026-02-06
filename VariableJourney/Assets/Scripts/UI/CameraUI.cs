using TMPro;
using UnityEngine;

public class CameraUI : MonoBehaviour
{
    private TMP_Text typeIndexText;
    private TMP_Text typeNameText;

    private void Start()
    {
        typeIndexText = transform.Find("TypeText").Find("TypeIndexText").GetComponent<TMP_Text>();
        typeNameText = transform.Find("TypeText").Find("TypeNameText").GetComponent<TMP_Text>();
    }

    public void SetTypeIndex(int typeIndex)
    {
        typeIndexText.text = typeIndex.ToString();
        switch (typeIndex)
        {
            case 0:
                typeNameText.text = "int";
                break;
            case 1:
                typeNameText.text = "bool";
                break;
            case 2:
                typeNameText.text = "string";
                break;
        }
    }
}
