using Ertis.MongoDB.Configuration;
using ErtisAuth.Abstractions.Services.Interfaces;
using ErtisAuth.Core.Models.Roles;
using ErtisAuth.Dao.Repositories;
using ErtisAuth.Dao.Repositories.Interfaces;
using ErtisAuth.Infrastructure.Services;
using NUnit.Framework;

namespace ErtisAuth.Tests
{
	public class RoleTests
	{
		#region Services

		private IRoleService roleService;

		#endregion
		
		#region Setup

		[SetUp]
		public void Setup()
		{
			IDatabaseSettings databaseSettings = new DatabaseSettings
			{
				Host = "172.17.0.2",
				Port = 27017,
				DatabaseName = "ertisauth"
			};
			
			IMembershipRepository membershipRepository = new MembershipRepository(databaseSettings, null);
			IRoleRepository roleRepository = new RoleRepository(databaseSettings, null);
			IMembershipService membershipService = new MembershipService(membershipRepository);
			this.roleService = new RoleService(membershipService, roleRepository);
		}

		#endregion
		
		#region Methods

		[Test]
		public void RbacParseTest()
		{
			var rbac1 = Rbac.Parse("ertugrulozcan.users.write.*");
			Assert.AreEqual("ertugrulozcan", rbac1.Subject);
			Assert.AreEqual("users", rbac1.Resource);
			Assert.AreEqual("write", rbac1.Action);
			Assert.AreEqual(RbacSegment.All, rbac1.Object);
			
			var rbac2 = Rbac.Parse("*.users.write");
			Assert.AreEqual(RbacSegment.All, rbac2.Subject);
			Assert.AreEqual("users", rbac2.Resource);
			Assert.AreEqual("write", rbac2.Action);
			Assert.AreEqual(RbacSegment.All, rbac2.Object);
			
			var rbac3 = Rbac.Parse("*.users.write.5d46d74a92f36369307a312b");
			Assert.AreEqual(RbacSegment.All, rbac3.Subject);
			Assert.AreEqual("users", rbac3.Resource);
			Assert.AreEqual("write", rbac3.Action);
			Assert.AreEqual("5d46d74a92f36369307a312b", rbac3.Object);
		}

		#endregion
	}
}