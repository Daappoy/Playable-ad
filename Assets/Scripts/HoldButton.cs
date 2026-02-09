using UnityEngine;
using UnityEngine.EventSystems;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [Tooltip("Set -1 for left, 1 for right")]
    public int direction = 1;
    public Player player;

    private bool isPressed = false;

    void Awake()
    {
        // Auto-find if not assigned
        if (player == null)
            player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (!isPressed) return;

        // Global release safety
        if (Input.GetMouseButtonUp(0))
        {
            StopPress();
            return;
        }

        for (int i = 0; i < Input.touchCount; i++)
        {
            var phase = Input.touches[i].phase;
            if (phase == TouchPhase.Ended || phase == TouchPhase.Canceled)
            {
                StopPress();
                break;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        if (player != null) player.SetMove(direction);
    }

    public void OnPointerUp(PointerEventData eventData) { StopPress(); }
    public void OnPointerExit(PointerEventData eventData) { StopPress(); }

    private void StopPress()
    {
        if (!isPressed) return;
        isPressed = false;
        if (player != null) player.StopMove();
    }
}