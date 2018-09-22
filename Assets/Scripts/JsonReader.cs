using System;
using UnityEngine;

public static class JsonReader {

	public static T ReadJson<T>(string filePath) {
		try {
			TextAsset targetFile = Resources.Load<TextAsset>(filePath);
			return JsonUtility.FromJson<T>(targetFile.text);
		}
		catch (Exception e) {
			Debug.LogError("Cannot load JSON data!");
			Debug.LogError(e.Message);
			return default(T);
		}
	}

	//public static T[] ReadJsonArr<T>(string filePath) {
	//	try {
	//		TextAsset targetFile = Resources.Load<TextAsset>(filePath);
	//		Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(targetFile.text);

	//		return wrapper.schema;
	//	}
	//	catch (Exception e) {
	//		Debug.LogError("Cannot load JSON data!");
	//		Debug.LogError(e.Message);
	//		return null;
	//	}
	//}

	//[Serializable]
	//class Wrapper<T> {
	//	public T[] schema;
	//}

}