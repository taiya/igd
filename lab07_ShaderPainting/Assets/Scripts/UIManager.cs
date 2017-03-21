using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	public GameObject panel;
	public Painter painter;

	RectTransform panelRect;

	void Start () {
		panelRect = panel.GetComponent<RectTransform> ();
	}

	void Update () {

		if (Input.GetKeyDown (KeyCode.H)) {
			panel.SetActive (!panel.activeSelf);
		}

		if (Input.GetMouseButtonDown (0) 
			&& (!panelRect.rect.Contains (Input.mousePosition) || !panel.activeSelf)) {
			painter.painting = true;
		}
		if (Input.GetMouseButtonUp (0)) {
			painter.painting = false;
		}

	}
}
