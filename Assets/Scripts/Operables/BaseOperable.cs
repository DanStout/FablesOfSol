using UnityEngine;
using System.Collections;

public abstract class BaseOperable : MonoBehaviour
{
    public float textBoxPadding = 10;

    private Rect _labelRect;
    private bool _doPrintActionText = false;
    private Vector3 _screenPoint;

    void Start()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        _doPrintActionText = true;
    }

    void OnTriggerExit(Collider other)
    {
        _doPrintActionText = false;
    }

    void OnTriggerStay(Collider other)
    {
        _screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        _screenPoint.y = Screen.height - _screenPoint.y;

        if (Input.GetButtonDown("Activate"))
            Operate();
    }

    void OnGUI()
    {
        //GUI.Box(new Rect(_screenPoint.x, _screenPoint.y + 25, 100, 25), string.Format("({0:F0}, {1:F0})", _screenPoint.x, _screenPoint.y));

        if (_doPrintActionText)
        {
            var actionTextSize = GUI.skin.label.CalcSize(new GUIContent(ActionText));
            var rect = new Rect(_screenPoint.x, _screenPoint.y, actionTextSize.x + textBoxPadding, actionTextSize.y);
            GUI.Box(rect, ActionText);
        }
    }

    public abstract void Operate();
    public abstract string ActionText { get; }
}
