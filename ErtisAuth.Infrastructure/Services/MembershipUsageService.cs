using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErtisAuth.Abstractions.Services.Interfaces;
using ErtisAuth.Core.Models;

namespace ErtisAuth.Infrastructure.Services
{
    public class MembershipUsageService : IMembershipUsageService
    {
        #region Services

        private readonly IUserService userService;
        private readonly IApplicationService applicationService;
        private readonly IRoleService roleService;
        private readonly IProviderService providerService;
        private readonly IWebhookService webhookService;
        
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="applicationService"></param>
        /// <param name="roleService"></param>
        /// <param name="providerService"></param>
        /// <param name="webhookService"></param>
        public MembershipUsageService(
            IUserService userService,
            IApplicationService applicationService,
            IRoleService roleService,
            IProviderService providerService,
            IWebhookService webhookService)
        {
            this.userService = userService;
            this.applicationService = applicationService;
            this.roleService = roleService;
            this.providerService = providerService;
            this.webhookService = webhookService;
        }

        #endregion
        
        #region Methods

        public IEnumerable<MembershipBoundedResource> GetMembershipBoundedResources(string membershipId, int limit = 10) =>
            this.GetMembershipBoundedResourcesAsync(membershipId, limit).ConfigureAwait(false).GetAwaiter().GetResult();

        public async Task<IEnumerable<MembershipBoundedResource>> GetMembershipBoundedResourcesAsync(string membershipId, int limit = 10)
        {
            var getUsersTask = this.userService.GetAsync(membershipId, 0, limit, false, null, null).AsTask();
            var getApplicationsTask = this.applicationService.GetAsync(membershipId, 0, limit, false, null, null).AsTask();
            var getRolesTask = this.roleService.GetAsync(membershipId, 0, limit, false, null, null).AsTask();
            var getProvidersTask = this.providerService.GetAsync(membershipId, 0, limit, false, null, null).AsTask();
            var getWebhooksTask = this.webhookService.GetAsync(membershipId, 0, limit, false, null, null).AsTask();

            await Task.WhenAll(getUsersTask, getApplicationsTask, getRolesTask, getProvidersTask, getWebhooksTask);
            
            var users = (await getUsersTask).Items;
            var applications = (await getApplicationsTask).Items;
            var roles = (await getRolesTask).Items;
            var providers = (await getProvidersTask).Items;
            var webhooks = (await getWebhooksTask).Items;
            
            var cumulativeList = new List<MembershipBoundedResource>();
            cumulativeList.AddRange(users);
            cumulativeList.AddRange(applications);
            cumulativeList.AddRange(roles);
            cumulativeList.AddRange(providers);
            cumulativeList.AddRange(webhooks);

            return cumulativeList.Take(limit);
        }

        #endregion
    }
}