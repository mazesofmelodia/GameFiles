using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoverInfoPopup : MonoBehaviour
{
    [SerializeField] private GameObject popupCanvasObject = null;       //Canvas of popup object
    [SerializeField] private RectTransform popupObject = null;          //Object to display on popup
    [SerializeField] private TextMeshProUGUI infoText = null;           //Text to display
    [SerializeField] private Vector3 offset = new Vector3(0f, 50f, 0f); //Offset value of popup
    [SerializeField] private float padding = 25f;                       //Padding on popup

    private Canvas popupCanvas = null;      //Canvas component

    //Get the canvas component of the popup canvas
    private void Start() => popupCanvas = popupCanvasObject.GetComponent<Canvas>();

    //Follow the mouse cursor
    private void Update() => FollowCursor();

    //Deactivate the canvas object
    public void HideInfo() => popupCanvasObject.SetActive(false);

    private void FollowCursor()
    {
        //If the popup object isn't active ignore this funtion
        if (!popupCanvasObject.activeSelf) { return; }

        //Set the position based on the mouse position and offset
        Vector3 newPos = Input.mousePosition + offset;
        //Make sure the popup window is 0 on the z axis
        newPos.z = 0f;

        //Measure how close the window is to the right edge of the screen
        float rightEdgeToScreenEdgeDistance = Screen.width - (newPos.x + popupObject.rect.width * popupCanvas.scaleFactor / 2) - padding;

        //If the window is beyond the right edge of the screen
        if (rightEdgeToScreenEdgeDistance < 0)
        {
            //Push it so the window remains on the screen
            newPos.x += rightEdgeToScreenEdgeDistance;
        }

        //Measure how close the popup window is to the left edge of the screen
        float leftEdgeToScreenEdgeDistance = 0 - (newPos.x - popupObject.rect.width * popupCanvas.scaleFactor / 2) + padding;

        //If the window is beyond the left screen edge
        if (leftEdgeToScreenEdgeDistance > 0)
        {
            //Push it back so the whole window remains on screen
            newPos.x += leftEdgeToScreenEdgeDistance;
        }

        //Measure the distance to the top edge of the screen
        float topEdgeToScreenEdgeDistance = Screen.height - (newPos.y + popupObject.rect.height * popupCanvas.scaleFactor) - padding;

        //If the window is beyond the top edge of the screen
        if (topEdgeToScreenEdgeDistance < 0)
        {
            //Push the window down so it remains within the screen
            newPos.y += topEdgeToScreenEdgeDistance;
        }

        //Reposition the popup object
        popupObject.transform.position = newPos;
    }

    //Function to display info based on the hotbar item
    public void DisplayInfo(HotbarItem infoItem)
    {
        //Create a new string builder
        StringBuilder builder = new StringBuilder();

        //Get the colored text of the item
        builder.Append("<size=35>").Append(infoItem.ColouredName).Append("</size>\n");

        //Get the display text from the item
        builder.Append(infoItem.GetInfoDisplayText());

        //Set the text based on the built text
        infoText.text = builder.ToString();

        //Activate the popup object
        popupCanvasObject.SetActive(true);

        //Rebuild the layout of the window
        LayoutRebuilder.ForceRebuildLayoutImmediate(popupObject);
    }
}
