using System.Threading.Tasks;
using ErtisAuth.Core.Models.Memberships;
using ErtisAuth.Core.Models.Users;

namespace ErtisAuth.Abstractions.Services.Interfaces
{
	public interface IMigrationService
	{
		Task<dynamic> MigrateAsync(string connectionString, Membership _membership, UserWithPassword _user);
	}
}