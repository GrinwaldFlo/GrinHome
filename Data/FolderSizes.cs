namespace GrinHome.Data;

public class FolderSizes
{
	public class FolderItem
	{
		public DateTime Date { get; set; }
		public string Name { get; set; } = string.Empty;
		public long Size { get; set; }

		public bool OK { get; set; }
		public FolderItem(string data)
		{
			string[] d = data.Split(new char[] { '\t' });
			if (d.Length == 2)
			{
				Size = GetSize(d[0]);
				Name = d[1];
				OK = Size > 0;
			}
		}

		private static long GetSize(string data)
		{
			if (data.Length < 2)
				return 0;

			char unit = data.Last();
			string value = data[..^1];
			if (!long.TryParse(value, out long size))
				return 0;

			return unit switch
			{
				'G' => size * 1024,
				'M' => size,
				_ => 0,
			};
		}
	}

	public static void AnalyseFolder(string path)
	{
		var folders = new List<FolderItem>();

		if (!Directory.Exists(path))
		{
			return;
		}
	}
}
