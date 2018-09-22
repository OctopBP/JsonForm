public static class MyExtension {
	public static string GetLast(this string source, int tail_length) {
		if (tail_length >= source.Length)
			return source;
		return source.Substring(source.Length - tail_length);
	}

	public static char GetLastChar(this string source) {
		return source.GetLast(1)[0];
	}
}
