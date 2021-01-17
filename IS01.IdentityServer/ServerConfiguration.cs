using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IS01.IdentityServer
{
    public static class ServerConfiguration
    {
        public static List<IdentityResource> IdentityResources
        {
            get
            {
                //Any resource you ask for, such as phone etc., the user will get a prompt for it to be granted

                List<IdentityResource> idResources = new List<IdentityResource>();

                idResources.Add(new IdentityResources.OpenId());
                idResources.Add(new IdentityResources.Address());
                idResources.Add(new IdentityResources.Email());
                idResources.Add(new IdentityResources.Phone());
                idResources.Add(new IdentityResources.Profile());
                idResources.Add(new IdentityResource("roles", "User Roles", new List<string> { "role" }));

                return idResources;
            }
        }

        public static List<ApiScope> ApiScopes
        {
            get
            {
                List<ApiScope> apiScopes = new List<ApiScope>();

                apiScopes.Add(new ApiScope("employeesWebApi", "Employees Web API"));

                return apiScopes;
            }
        }

        public static List<ApiResource> ApiResources
        {
            get
            {
                ApiResource apiResource1 = new ApiResource("employeesWebApiResource", "Employees Web API Resource")
                {
                    Scopes = { "employeesWebApi" },
                    UserClaims = { "role", "given_name", "family_name", "email", "phone", "address" }
                };

                List<ApiResource> apiResources = new List<ApiResource>();
                apiResources.Add(apiResource1);
                return apiResources;
            }
        }

        public static List<Client> Clients
        {
            get
            {
                Secret client1Secret = new Secret("client1_secret_code".Sha512());

                Client client1 = new Client()
                {
                    ClientId = "Client1",
                    ClientName = "Client Name 1",
                    ClientSecrets = new[] { client1Secret },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials, //Because this client directly invokes the Web API
                    AllowedScopes = { "openid", "roles", "employeesWebApi" }
                };

                List<Client> clients = new List<Client>();
                clients.Add(client1);
                return clients;
            }
        }

        public static List<TestUser> TestUsers { get {

                TestUser testUser1 = new TestUser
                {

                    SubjectId = "2f47f8f0-bea1-4f0e-ade1-88533a0eaf57",
                    Username = "user1",
                    Password = "password1",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "FirstName1"),
                        new Claim("family_name", "LastName1"),
                        new Claim("address", "Addr1"),
                        new Claim("email", "Email1@Example.com"),
                        new Claim("phone", "111-555-1212"),
                        new Claim("role", "Admin"),
                    }

                };

                TestUser testUser2 = new TestUser
                {

                    SubjectId = "5747df40-1bff-49ee-aadf-905bacb39a3a",
                    Username = "user2",
                    Password = "password2",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "FirstName2"),
                        new Claim("family_name", "LastName2"),
                        new Claim("address", "Addr2"),
                        new Claim("email", "Email2@Example.com"),
                        new Claim("phone", "222-555-1212"),
                        new Claim("role", "Operator"),
                    }

                };


                List<TestUser> testUsers = new List<TestUser>();
                testUsers.Add(testUser1);
                testUsers.Add(testUser2);
                return testUsers;

            } }
    }
}