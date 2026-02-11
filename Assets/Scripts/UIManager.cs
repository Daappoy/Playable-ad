using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject movementButtons;
    public Text scoreText;

    void Start()
    {
        AdjustButtonPositions();
    }

    void OnRectTransformDimensionsChange()
    {
        AdjustButtonPositions();
    }

    bool IsPortrait()
    {
        return Screen.height >= Screen.width;
    }

    void AdjustButtonPositions()
    {
        if (IsPortrait())
        {
            Debug.Log("Portrait Mode");
            movementButtons.transform.localPosition = new Vector3(0, -330, 0);
            scoreText.transform.localPosition = new Vector3(0, 620, 0);
        }
        else
        {
            Debug.Log("Landscape Mode");
            movementButtons.transform.localPosition = new Vector3(-450, -25, 0);
            scoreText.transform.localPosition = new Vector3(0, 400, 0);
        }
    }
}