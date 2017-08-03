using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoBehaviour {

    private static FloatingText popupText;
    private static GameObject canvas;

    public void Start()
    {
        canvas = GameObject.Find("Interface");
        if (!popupText)
            popupText = Resources.Load<FloatingText>("Prefabs/PopupTextParent");
    }

    public static void CreateFloatingText(string text, Transform location, bool damageDisplayed)
    {
        if (!popupText)
        {
            canvas = GameObject.Find("Interface");
            popupText = Resources.Load<FloatingText>("Prefabs/PopupTextParent");
        }



        FloatingText instance = Instantiate(popupText);

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector2(location.position.x + Random.Range(.5f, -.5f), location.position.y + 1));
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.SetText(text, damageDisplayed);
    }
}
