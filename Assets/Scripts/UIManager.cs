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
    bool isPortrait()
    {
        return Screen.height >= Screen.width;
    }

    private void AdjustButtonPositions()
    {
        if (isPortrait() == true)
        {
            //portrait
            Debug.Log("Portrait Mode");
            movementButtons.transform.localPosition = new Vector3(0, -330, 0);
            scoreText.transform.localPosition = new Vector3(0, 620, 0);
        }
        else if (isPortrait() == false)
        {
            //landscape
            Debug.Log("Landscape Mode");
            movementButtons.transform.localPosition = new Vector3(-555, -25, 0);
            scoreText.transform.localPosition = new Vector3(0, 400, 0);
        }
    }
}
