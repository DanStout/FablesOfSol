using UnityEngine;
using System.Collections;

public abstract class BaseOperable : MonoBehaviour
{
    public string ActionText;
    public float textBoxPadding = 10;
    public float reactOutsideDistance;

    private Rect _labelRect;
    private bool isWithinRange = false;
    private Vector3 _screenPoint;
    private bool wasWithinRange = false;
    private GameObject player;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isWithinRange = true;
            wasWithinRange = true;
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            isWithinRange = false;
    }

    protected virtual void Update()
    {
        _screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        _screenPoint.y = (Screen.height - _screenPoint.y);

        if (isWithinRange)
        {
            if (Input.GetButtonDown("Activate"))
            {
                Operate();
            }
        }

        var playerPos = player.transform.position;
        var playerDist = Vector3.Distance(transform.position, playerPos);

        if (wasWithinRange && playerDist >= reactOutsideDistance)
        {
            ReactLeftRange();
            wasWithinRange = false;
        }
    }

    void OnGUI()
    {
        if (isWithinRange)
        {
            var actionTextSize = GUI.skin.label.CalcSize(new GUIContent(ActionText));
            var rect = new Rect(_screenPoint.x, _screenPoint.y, actionTextSize.x + textBoxPadding, actionTextSize.y);
            GUI.Box(rect, ActionText);
        }
    }

    public abstract void Operate();

    protected virtual void ReactLeftRange()
    {
        //optionally overridden by subclasses
    }
}
