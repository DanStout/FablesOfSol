using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for enemies
/// 
/// Put anything in here 
/// </summary>
public abstract class Enemy : MonoBehaviour
{
    public abstract float damage { get; set; }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
