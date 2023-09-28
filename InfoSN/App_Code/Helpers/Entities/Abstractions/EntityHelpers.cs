using InfoSN.Models.Entities.Abstractions;
using System.Data;

namespace InfoSN.App_Code.Helpers.Entities.Abstractions
{
	public abstract class EntityHelpers
	{
		public abstract Entity GenerateEntityFromDb(IDataReader reader);
	}
}
