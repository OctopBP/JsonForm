using System;

[Serializable]
public class SchemaBase {
	public Schema schema;
}

[Serializable]
public class Schema {
	public string load;
	public Group[] groups;
}

[Serializable]
public class Group {
	public string name;
	public DefaultFeild[] fields;
}

[Serializable]
public class DefaultFeild {
	public string name;
	public string type;
	public string[] items;
	public DefaultFeild[] array_fields;
}