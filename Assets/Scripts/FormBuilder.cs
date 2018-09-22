using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormBuilder: MonoBehaviour {

	public Text titlePrefab;
	public InputField inputFieldPrefab;
	public InputField bigInputFieldPrefab;
	public Dropdown dropdownPrefab;
	public Toggle togglePrefab;

	public InputField textField;

	public int leftOffset;
	public int topOffset;
	public int lineHeight;

	private SchemaBase schemaBase = new SchemaBase();

	private void Start() {
		ReadSchema();
	}

	private void ReadSchema() {
		string fileName = "Schema";

		schemaBase = JsonReader.ReadJson<SchemaBase>(fileName);

		foreach (Group group in schemaBase.schema.groups) {
			if (group.name == schemaBase.schema.load) {
				LoadGroup(group);
			}
		}
	}
	private void LoadGroup(Group group) {
		number = 0;
		RectTransform groupParent = new GameObject(group.name, typeof(RectTransform)).GetComponent<RectTransform>();

		groupParent.anchorMin = new Vector2(0, 1);
		groupParent.anchorMax = new Vector2(0, 1);
		groupParent.pivot = new Vector2(0, 1);

		groupParent.transform.localPosition = new Vector3(leftOffset, topOffset, 0);
		groupParent.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

		AddTitle(group.name, groupParent);
		foreach (DefaultFeild f in group.fields) {
			switch (f.type) {
				case "number":
				case "string":				AddInputField(f, groupParent);		break;
				case "big_string":			AddBigInputField(f, groupParent);	break;
				case "enum":				AddDropdown(f, groupParent);		break;
				case "bool":				AddToggle(f, groupParent);			break;
				case "dictionary_array":	AddDictionary(f, groupParent);		break;
			}
		}
	}

	int number = 0;
	public void AddTitle(string titleText, RectTransform parent) {
		Text title = Instantiate(titlePrefab);

		title.name = "Text " + titleText;
		title.transform.localPosition = new Vector3(0, -lineHeight * number, 0);
		title.transform.SetParent(parent, false);
		title.text = titleText;

		number++;
	}
	public void AddInputField(DefaultFeild feild, RectTransform parent) {
		InputField inputField = Instantiate(inputFieldPrefab);
			
		inputField.contentType = feild.type == "number" ? InputField.ContentType.IntegerNumber : InputField.ContentType.Standard;

		inputField.name = "Input Field " + feild.name;
		inputField.transform.localPosition = new Vector3(0, -lineHeight * number, 0);
		inputField.transform.SetParent(parent, false);

		inputField.placeholder.GetComponent<Text>().text = feild.name;

		number++;
	}
	public void AddBigInputField(DefaultFeild feild, RectTransform parent) {
		InputField inputField = Instantiate(bigInputFieldPrefab);

		inputField.contentType = feild.type == "number" ? InputField.ContentType.IntegerNumber : InputField.ContentType.Standard;

		inputField.name = "Big Input Field " + feild.name;
		inputField.transform.localPosition = new Vector3(0, -lineHeight * number, 0);
		inputField.transform.SetParent(parent, false);

		inputField.placeholder.GetComponent<Text>().text = feild.name;

		number += 2;
	}
	public void AddDropdown(DefaultFeild feild, RectTransform parent) {
		Dropdown dropdown = Instantiate(dropdownPrefab);

		dropdown.name = "Dropdown " + feild.name;
		dropdown.transform.localPosition = new Vector3(0, -lineHeight * number, 0);
		dropdown.transform.SetParent(parent, false);

		List<string> options = new List<string>();
		options.AddRange(feild.items);

		dropdown.ClearOptions();
		dropdown.AddOptions(options);

		number++;

		OnEnumValueChanges(dropdown.options[dropdown.value].text);
	}
	public void AddToggle(DefaultFeild feild, RectTransform parent) {
		Toggle toggle = Instantiate(togglePrefab);

		toggle.name = "Toggle " + feild.name;
		toggle.transform.localPosition = new Vector3(0, -lineHeight * number, 0);
		toggle.transform.SetParent(parent, false);

		toggle.GetComponentInChildren<Text>().text = feild.name;

		number++;
	}
	public void AddDictionary(DefaultFeild feild, RectTransform parent) {
		RectTransform groupParent = new GameObject(feild.name, typeof(RectTransform)).GetComponent<RectTransform>();

		groupParent.anchorMin = new Vector2(0, 1);
		groupParent.anchorMax = new Vector2(0, 1);
		groupParent.pivot = new Vector2(0, 1);

		groupParent.transform.localPosition = new Vector3(0, 0, 0);
		groupParent.transform.SetParent(parent, false);

		foreach (DefaultFeild f in feild.array_fields) {
			switch (f.type) {
				case "number":
				case "string": AddInputField(f, groupParent); break;
				case "big_string": AddBigInputField(f, groupParent); break;
				case "enum": AddDropdown(f, groupParent); break;
				case "bool": AddToggle(f, groupParent); break;
				case "dictionary_array": break;
			}
		}
	}

	public void OnEnumValueChanges(string dropdownText) {
		foreach (Group group in schemaBase.schema.groups) {
			if (group.name.ToLower() == dropdownText.ToLower()) {
				leftOffset += 300;
				LoadGroup(group);
			}
		}
	}

	public void GenerateJson() {
		string text = "\"card\": {\n";

		textField.text = text;
	}
}
