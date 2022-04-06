using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IUIAction
{
	GameObject gameObject { get; }
	void OnAction();
}
