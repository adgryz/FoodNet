using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FoodNet.DataAccessCore;
using Microsoft.EntityFrameworkCore;
using Autofac;
using FoodNET.WebAPI.Data;
using FoodNET.WebAPI.Data.Interfaces;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FoodNET.WebAPI.Data.DTOFacade;
using IdentityServer4.Models;
using IdentityServer4;
using FoodNet.ModelCore.Domain;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;

namespace FoodNET.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var connectionString = Configuration.GetConnectionString("FoodNetDbContext");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<FoodNetDbContext>(efbuilder =>
                efbuilder.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)));
            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<FoodNetDbContext>();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://foodnetwebapi2.azurewebsites.net/";
                    options.TokenValidationParameters.ValidateAudience = false;
                    options.TokenValidationParameters.ValidateIssuer = false;
                    options.RequireHttpsMetadata = false;
                });

            services.AddIdentityServer()
                        .AddDeveloperSigningCredential()
                        .AddInMemoryClients(Clients.Get())
                        .AddInMemoryApiResources(Resources.GetApiResources())
                        .AddInMemoryIdentityResources(Resources.GetIdentityResources())
                        .AddAspNetIdentity<User>()
                        .AddOperationalStore(options =>
                            options.ConfigureDbContext = efbuilder =>
                                efbuilder.UseSqlServer(connectionString,
                                    sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)));

            services.AddAutoMapper();

            services.AddCors(o => o.AddPolicy("MyPolicy", cbuilder =>
            {
                cbuilder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            // Autofac
            var builder = new ContainerBuilder();

            builder.RegisterType<FridgeRepository>().As<IFridgeRepository>();
            builder.RegisterType<ProductsRepository>().As<IProductsRepository>();
            builder.RegisterType<RecipesRepository>().As<IRecipesRepository>();
            builder.RegisterType<UsersRepository>().As<IUsersRepository>();
            builder.RegisterType<TagsRepository>().As<ITagsRepository>();
            builder.RegisterType<PriorityUserProductsRepository>().As<IPriorityUserProductsRepository>();
            builder.RegisterType<ResultDataService>().As<IResultDataService>();

            builder.RegisterType<ProductsDTOFacade>().AsSelf();
            builder.RegisterType<RecipesDTOFacade>().AsSelf();
            builder.RegisterType<FridgesDTOFacade>().AsSelf();
            builder.RegisterType<UsersDTOFacade>().AsSelf();
            builder.RegisterType<ResultDataDTOFacade>().AsSelf();
            builder.RegisterType<TagsDTOFacade>().AsSelf();

            builder.Populate(services);
            var container = builder.Build();
            return new AutofacServiceProvider(container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseMvc();
        }

        internal class Clients
        {
            public static IEnumerable<Client> Get()
            {
                return new List<Client> {
                    new Client
                    {
                            ClientId = "foodnetClient",
                            ClientName = "FoodNET Client",
                            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                            ClientSecrets = { new Secret("secret".Sha256()) },
                            RedirectUris = { "https://foodnetwebapi2.azurewebsites.net/" },
                            PostLogoutRedirectUris = { "https://foodnetwebapi2.azurewebsites.net/" },
                            BackChannelLogoutUri = "https://foodnetwebapi2.azurewebsites.net/" ,
                            AllowedScopes = new List<string>
                            {
                                IdentityServerConstants.StandardScopes.OpenId,
                                IdentityServerConstants.StandardScopes.Profile,
                                IdentityServerConstants.StandardScopes.Email,
                                "api"
                            },
                            RequireClientSecret=false,
                            AllowedCorsOrigins = { "https://fooodnet.github.io/", "http://fooodnet.github.io/" }
                    }
                };
            }
        }

        internal class Resources
        {
            public static IEnumerable<IdentityResource> GetIdentityResources()
            {
                return new List<IdentityResource> {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile(),
                    new IdentityResources.Email(),
                };
            }

            public static IEnumerable<ApiResource> GetApiResources()
            {
                return new List<ApiResource> {
                    new ApiResource("api", "FoodNetAPI")
                };
            }
        }
    }

}
