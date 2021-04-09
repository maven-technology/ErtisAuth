using System.Threading.Tasks;
using Ertis.Core.Collections;
using Ertis.Core.Models.Response;
using ErtisAuth.Core.Models.Identity;
using ErtisAuth.Core.Models.Memberships;

namespace ErtisAuth.Sdk.Services.Interfaces
{
	public interface IMembershipService
	{
		IResponseResult<Membership> GetMembership(string membershipId, TokenBase token);
		
		Task<IResponseResult<Membership>> GetMembershipAsync(string membershipId, TokenBase token);
		
		IResponseResult<IPaginationCollection<Membership>> GetMemberships(TokenBase token, int? skip = null, int? limit = null, bool? withCount = null, string orderBy = null, SortDirection? sortDirection = null, string searchKeyword = null);
		
		Task<IResponseResult<IPaginationCollection<Membership>>> GetMembershipsAsync(TokenBase token, int? skip = null, int? limit = null, bool? withCount = null, string orderBy = null, SortDirection? sortDirection = null, string searchKeyword = null);
		
		IResponseResult<IPaginationCollection<Membership>> QueryMemberships(TokenBase token, string query, int? skip = null, int? limit = null, bool? withCount = null, string orderBy = null, SortDirection? sortDirection = null);
		
		Task<IResponseResult<IPaginationCollection<Membership>>> QueryMembershipsAsync(TokenBase token, string query, int? skip = null, int? limit = null, bool? withCount = null, string orderBy = null, SortDirection? sortDirection = null);
	}
}