using UnityEngine;
using UnityEngine.UI;

public class BurronUtilsPauseMenu : MonoBehaviour
{
    public static void ResetButtonSprites(GameObject menu)
    {
        Button[] buttons = menu.GetComponentsInChildren<Button>(true);
        foreach (Button btn in buttons)
        {
            // Reset visual state
            btn.OnDeselect(null);
            btn.OnPointerExit(null);
        }
    }
}