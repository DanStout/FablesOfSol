using UnityEngine;
using System.Collections;

public abstract class BaseOperable : MonoBehaviour
{
    public string ActionText;
    public float textBoxPadding = 10;

    private Rect _labelRect;
    private bool isWithinRange = false;
    private Vector3 _screenPoint;

	private float radius = 1f;
    void Start()
    {
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            isWithinRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            isWithinRange = false;
    }

    void Update()
    {
        _screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        _screenPoint.y = (Screen.height - _screenPoint.y);
		
    }

    void OnGUI()
    {
        if (isWithinRange)
        {
            var actionTextSize = GUI.skin.label.CalcSize(new GUIContent(ActionText));
            var rect = new Rect(_screenPoint.x, _screenPoint.y, actionTextSize.x + textBoxPadding, actionTextSize.y);
            GUI.Box(rect, ActionText);

			if (Input.GetButtonDown("Activate"))
			{
				Operate();
			}
        }
    }

    public abstract void Operate();
}
