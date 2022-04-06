using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
	public bool IsPatting { get; private set; }
	public bool IsEmpty { get => ObjectHolding == null; }
	public GameObject ObjectHolding { get; private set; }

	private GameObject objectToGrab;
	private Vector3 normalScale;
	private SpriteRenderer spriteRenderer;
	private IUIAction actionToDo = null;

	// Start is called before the first frame update
	void Start()
	{
		normalScale = transform.localScale;
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		this.transform.position = GetMouseWorldPosition();
		if(ObjectHolding != null)
		{
			ObjectHolding.transform.position = transform.position;
		}
		if (Input.GetButtonDown("Fire1"))
		{
			if(objectToGrab != null)
			{
				GrabObject();
			}
		}
		if (Input.GetButtonUp("Fire1"))
		{
			if(actionToDo != null)
			{
				transform.position = Vector3.zero;
				actionToDo.OnAction();
			}
			if(ObjectHolding != null)
			{
				ReleaseObject();
			}
			IsPatting = false;
			transform.localScale = normalScale;
		}
	}

	private void GrabObject()
	{
		ObjectHolding = objectToGrab;
		ObjectHolding.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
		ObjectHolding.GetComponent<Collider2D>().enabled = false;
		spriteRenderer.enabled = false;
	}

	private void ReleaseObject()
	{
		ObjectHolding.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
		ObjectHolding.GetComponent<Collider2D>().enabled = true;
		if (ObjectHolding.tag != "Food") 
			ObjectHolding.transform.position = new Vector3(0, -11, 0);
		ObjectHolding = null;
		spriteRenderer.enabled = true;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Food" || collision.tag == "Tool" || collision.tag == "PattingHand")
		{
			objectToGrab = collision.gameObject;
		}
		if(collision.tag == "Action")
		{
			actionToDo = collision.gameObject.GetComponent<IUIAction>();
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.gameObject == objectToGrab)
		{
			objectToGrab = null;
		}
		if(actionToDo != null && collision.gameObject == actionToDo.gameObject)
		{
			actionToDo = null;
		}
	}

	private static Vector3 GetMouseWorldPosition()
	{
		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.z = 0;
		return pos;
	}
	private Vector3 GetPattingLocalScale()
	{
		return new Vector3(normalScale.x, normalScale.y*0.8f, normalScale.z);
	}
}
