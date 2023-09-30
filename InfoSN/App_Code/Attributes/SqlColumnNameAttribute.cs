namespace InfoSN.App_Code.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class SqlColumnNameAttribute : Attribute
	{
		public string Name { get; }

		public SqlColumnNameAttribute(string name)
		{
			Name = name;
		}

	}
}
