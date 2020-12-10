using System.Collections.Generic;
using System.Threading.Tasks;
using Ertis.Core.Collections;

namespace ErtisAuth.Abstractions.Services.Interfaces
{
	public interface ITestService
	{
		Task<IPaginationCollection<dynamic>> QueryAsync(
			string query, 
			int? skip = null, 
			int? limit = null,
			bool? withCount = null, 
			string sortField = null, 
			SortDirection? sortDirection = null,
			IDictionary<string, bool> selectFields = null);
	}
}