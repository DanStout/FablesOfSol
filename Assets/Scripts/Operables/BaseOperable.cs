using UnityEngine;
using System.Collections;

public abstract class BaseOperable : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    public float textBoxPadding = 10;

    private Rect _labelRect;
    private bool _doPrintActionText = false;
    private Vector3 _screenPoint;

    void OnTriggerEnter(Collider other)
    {
        _screenPoint = _camera.WorldToScreenPoint(transform.position);
        
        _doPrintActionText = true;
    }

    void OnTriggerExit(Collider other)
    {
        _doPrintActionText = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("Activate"))
            Operate();
    }

    void OnGUI()
    {
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
