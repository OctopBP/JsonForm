using UnityEngine;
using UnityEngine.UI;

public class DropdownScript : MonoBehaviour {

	private FormBuilder builder;

	private void Awake() {
		builder = FindObjectOfType<FormBuilder>();
	}

	public void OnEnumValueChanges(Dropdown dropdown) {
		builder.OnEnumValueChanges(dropdown.options[dropdown.value].text);
	}
}
