using UnityEngine;
using System.Collections;

public abstract class BaseOperable : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    public float textBoxPadding = 10;

    private Vector2 _actionTextSize;
    private Rect _labelRect;
    private bool _doPrintActionText = false;

    void OnTriggerEnter(Collider other)
    {
        var scrPt = _camera.WorldToScreenPoint(transform.position);
        _labelRect = new Rect(scrPt.x, scrPt.y, _actionTextSize.x + textBoxPadding, _actionTextSize.y);
        _doPrintActionText = true;
    }

    void OnTriggerExit(Collider other)
    {
        _doPrintActionText = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("Submit"))
            Operate();
    }

    void OnGUI()
    {
        _actionTextSize = GUI.skin.label.CalcSize(new GUIContent(ActionText));

        if (_doPrintActionText)
        {
            GUI.Box(_labelRect, ActionText);
        }
    }

    public abstract void Operate();
    public abstract string ActionText { get; }
}
