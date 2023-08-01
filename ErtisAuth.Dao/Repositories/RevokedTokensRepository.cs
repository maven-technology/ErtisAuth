using Ertis.MongoDB.Configuration;
using Ertis.MongoDB.Models;
using ErtisAuth.Dao.Repositories.Interfaces;
using ErtisAuth.Dto.Models.Identity;

namespace ErtisAuth.Dao.Repositories
{
	public class RevokedTokensRepository : RepositoryBase<RevokedTokenDto>, IRevokedTokensRepository
	{
		#region Properties
        
		protected override IIndexDefinition[] Indexes => new IIndexDefinition[]
		{
			new SingleIndexDefinition("access_token")
		};

		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="settings"></param>
		/// <param name="clientSettings"></param>
		public RevokedTokensRepository(IDatabaseSettings settings, IClientSettings clientSettings) : base(settings, "revoked_tokens", clientSettings, null)
		{
			
		}

		#endregion
	}
}