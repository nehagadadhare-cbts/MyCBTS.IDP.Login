using AutoMapper;
using Duende.IdentityServer.Hosting;
using Duende.IdentityServer.ResponseHandling;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Duende.IdentityServer.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCBTS.IDP.Login.Configuration;
using MyCBTS.IDP.Login.CustomIdentityServices.EndPoints;
using MyCBTS.IDP.Login.CustomIdentityServices.ResponseHandling;
using MyCBTS.IDP.Login.CustomIdentityServices.Services;
using MyCBTS.IDP.Login.CustomIdentityServices.Validators;
using MyCBTS.IDP.Login.Helpers;
using MyCBTS.IDP.Login.Models;
using MyCBTS.IDP.Login.Stores;
using MyCBTS.IDP.Login.Logger;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using MyCBTS.IDP.Login.Utility;
using MyCBTS.IDP.Login.Extensions;
using Duende.IdentityServer.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Generic;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;
using MyCBTS.IDP.Login.CustomIdentityServices.Secret;
using Microsoft.AspNetCore.Routing;
using MyCBTS.IDP.Login.Data;
using MyCBTS.IDP.Login.CustomIdentityServices.Constants;

namespace MyCBTS.IDP.Login
{
    public class Startup
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public IConfiguration Configuration { get; }
        public Startup(IWebHostEnvironment env)
        {
            this._hostingEnvironment = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"appsettings.{this._hostingEnvironment.EnvironmentName}.json", true, true);
            this.Configuration = builder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region register IOptions

            services.AddOptions();
            services.Configure<CBTSServiceConfiguration>(this.Configuration.GetSection("CBTSService"));
            services.Configure<MyCBTSDefaultURI>(this.Configuration.GetSection("MyCBTSDefaultURI"));
            services.Configure<AppConfiguration>(this.Configuration.GetSection("AppConfiguration"));
            services.Configure<AllowCBTSConfiguration>(this.Configuration.GetSection("AllowCBTS"));

            #endregion register IOptions

            #region MyCBTS.Client
            services.AddSingleton<ApiClientHelper>();
            var authToken = Encoding.ASCII.GetBytes($"{Configuration.GetSection("CBTSService").GetValue<string>("AuthUser").DecryptSecret()}:{Configuration.GetSection("CBTSService").GetValue<string>("AuthPassword").DecryptSecret()}");
            //services.AddHttpClient<MyCBTS.Api.Client.ICBTSServiceClient, MyCBTS.Api.Client.CBTSServiceClient>(c =>
            //{
            //    c.BaseAddress = new Uri(Configuration.GetSection("CBTSService").GetValue<string>("BaseURI"));
            //    c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
            //    c.DefaultRequestHeaders.Add("ApplicationName", Configuration.GetSection("CBTSService").GetValue<string>("Application"));
            //});
            AddClientServices(services);
            #endregion MyCBTS.Client            

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.IdleTimeout = TimeSpan.FromMinutes(20);
            });
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICommonUtility, CommonUtility>();
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddSingleton<UserStore>();
            services.AddTransient<IUserStore<User>, UserStore>();
            services.AddSingleton<LoggerManager>();
            services.AddAutoMapper(typeof(Startup));
            services.AddHttpContextAccessor();

            //services.AddIdentity<User, Role>()
            //        .AddDefaultTokenProviders();
            var builder = services.AddIdentityServer(options =>
            {
                options.KeyManagement.Enabled = false;
                //// new key every x days
                //options.KeyManagement.RotationInterval = TimeSpan.FromDays(Convert.ToDouble(this.Configuration.GetSection("KMDuende:RotationInterval").Value));
                //// announce new key x days in advance in discovery
                //options.KeyManagement.PropagationTime = TimeSpan.FromDays(Convert.ToDouble(this.Configuration.GetSection("KMDuende:PropagationTime").Value));
                //// keep old key for x days in discovery for validation of tokens
                //options.KeyManagement.RetentionDuration = TimeSpan.FromDays(Convert.ToDouble(this.Configuration.GetSection("KMDuende:RetentionDuration").Value));
                //// don't delete keys after their retention period is over
                //options.KeyManagement.DeleteRetiredKeys = false;
                //options.KeyManagement.SigningAlgorithms = new[]{
                //               // RS256 for older clients (with additional X.509 wrapping)
                //                new SigningAlgorithmOptions(SecurityAlgorithms.RsaSha256) { UseX509Certificate = true } };
                //options.KeyManagement.KeyPath = this.Configuration.GetSection("KMDuende:Path").Value;
                options.LicenseKey = LoadDuendeLicense();
            });
            builder.Services.AddDataProtection()
                    .PersistKeysToFileSystem(new DirectoryInfo(this.Configuration.GetSection("KMDuende:Path").Value))
                    .SetApplicationName("mycbts.login");

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie("Cookies", options => { options.Cookie.SameSite = SameSiteMode.Lax; });

            builder.Services.Configure<ForwardedHeadersOptions>(options =>
            {
                List<string> ipproxies = new List<string>();
                ipproxies = this.Configuration.GetSection("KnownProxies").Get<List<string>>();
                options.ForwardedHeaders =
                   ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                foreach (var ip in ipproxies)
                {
                    options.KnownProxies.Add(IPAddress.Parse(ip));
                }
                options.ForwardedForHeaderName = "X-Forwarded-For";
            });

            AddIdentityServerCustomServices(builder);

            AddSingletonClases(services);

            services.AddControllersWithViews();
            services.AddRazorPages();           
        }
        public void AddClientServices(IServiceCollection services)
        {
            var authToken = Encoding.ASCII.GetBytes($"{Configuration.GetSection("CBTSService").GetValue<string>("AuthUser").DecryptSecret()}:{Configuration.GetSection("CBTSService").GetValue<string>("AuthPassword").DecryptSecret()}");
            var baseURL = new Uri(Configuration.GetSection("CBTSService").GetValue<string>("BaseURI").DecryptSecret());
            var application = Configuration.GetSection("CBTSService").GetValue<string>("Application");

            services.AddHttpClient<MyCBTS.Api.Client.IAccountClient, MyCBTS.Api.Client.AccountClient>(c =>
            { AddClient(c, authToken, baseURL, application); });

            services.AddHttpClient<MyCBTS.Api.Client.IAlertClient, MyCBTS.Api.Client.AlertClient>(c =>
            { AddClient(c, authToken, baseURL, application); });

            services.AddHttpClient<MyCBTS.Api.Client.IBillClient, MyCBTS.Api.Client.BillClient>(c =>
            { AddClient(c, authToken, baseURL, application); });

            services.AddHttpClient<MyCBTS.Api.Client.IBillHistoryClient, MyCBTS.Api.Client.BillHistoryClient>(c =>
            { AddClient(c, authToken, baseURL, application); });

            services.AddHttpClient<MyCBTS.Api.Client.IBillingAddressClient, MyCBTS.Api.Client.BillingAddressClient>(c =>
            { AddClient(c, authToken, baseURL, application); });

            services.AddHttpClient<MyCBTS.Api.Client.IBillPdfClient, MyCBTS.Api.Client.BillPdfClient>(c =>
            { AddClient(c, authToken, baseURL, application); });

            services.AddHttpClient<MyCBTS.Api.Client.IBillReadyClient, MyCBTS.Api.Client.BillReadyClient>(c =>
            { AddClient(c, authToken, baseURL, application); });

            services.AddHttpClient<MyCBTS.Api.Client.IBillReviewClient, MyCBTS.Api.Client.BillReviewClient>(c =>
            { AddClient(c, authToken, baseURL, application); });

            services.AddHttpClient<MyCBTS.Api.Client.IEmailVerificationClient, MyCBTS.Api.Client.EmailVerificationClient>(c =>
            { AddClient(c, authToken, baseURL, application); });

            services.AddHttpClient<MyCBTS.Api.Client.IEventClient, MyCBTS.Api.Client.EventClient>(c =>
            { AddClient(c, authToken, baseURL, application); });

            services.AddHttpClient<MyCBTS.Api.Client.ILoggerClient, MyCBTS.Api.Client.LoggerClient>(c =>
            { AddClient(c, authToken, baseURL, application); });

            services.AddHttpClient<MyCBTS.Api.Client.IMessageTemplateClient, MyCBTS.Api.Client.MessageTemplateClient>(c =>
            { AddClient(c, authToken, baseURL, application); });

            services.AddHttpClient<MyCBTS.Api.Client.IReportClient, MyCBTS.Api.Client.ReportClient>(c =>
            { AddClient(c, authToken, baseURL, application); });

            services.AddHttpClient<MyCBTS.Api.Client.ITextVerificationClient, MyCBTS.Api.Client.TextVerificationClient>(c =>
            { AddClient(c, authToken, baseURL, application); });

            services.AddHttpClient<MyCBTS.Api.Client.ITokenClient, MyCBTS.Api.Client.TokenClient>(c =>
            { AddClient(c, authToken, baseURL, application); });

            services.AddHttpClient<MyCBTS.Api.Client.IUserClient, MyCBTS.Api.Client.UserClient>(c =>
            { AddClient(c, authToken, baseURL, application); });
            services.AddHttpClient<MyCBTS.Api.Client.ICreateUserClient, MyCBTS.Api.Client.CreateUserClient>(c =>
            { AddClient(c, authToken, baseURL, application); });

            services.AddHttpClient<MyCBTS.Api.Client.IUserProfileClient, MyCBTS.Api.Client.UserProfileClient>(c =>
            { AddClient(c, authToken, baseURL, application); });

            services.AddHttpClient<MyCBTS.Api.Client.IValidateUserClient, MyCBTS.Api.Client.ValidateUserClient>(c =>
            { AddClient(c, authToken, baseURL, application); });

            services.AddHttpClient<MyCBTS.Api.Client.IIdentityScopeClient, MyCBTS.Api.Client.IdentityScopeClient>(c =>
            { AddClient(c, authToken, baseURL, application); });

            services.AddHttpClient<MyCBTS.Api.Client.IIdentityClientClient, MyCBTS.Api.Client.IdentityClientClient>(c =>
            { AddClient(c, authToken, baseURL, application); });

            services.AddHttpClient<MyCBTS.Api.Client.IIdentityTokenClient, MyCBTS.Api.Client.IdentityTokenClient>(c =>
            { AddClient(c, authToken, baseURL, application); });

            services.AddHttpClient<MyCBTS.Api.Client.IAuditClient, MyCBTS.Api.Client.AuditClient>(c =>
            { AddClient(c, authToken, baseURL, application); });

        }

        public System.Net.Http.HttpClient AddClient(System.Net.Http.HttpClient client, byte[] authToken, Uri baseURL, string application)
        {
            client.BaseAddress = baseURL;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
            client.DefaultRequestHeaders.Add("ApplicationName", application);
            return client;
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders();
            if (env.EnvironmentName.ToString().ToUpper() == "PRODUCTION" || env.EnvironmentName.ToString().ToUpper() == "SANDBOX")
            {
                app.UseExceptionHandler("/Home/Error/errorId={0}");
                app.UseStatusCodePagesWithRedirects("/home/error?errorId={0}");
            }
            else
                app.UseDeveloperExceptionPage();

            NLog.Web.NLogBuilder.ConfigureNLog($"nlog.{this._hostingEnvironment.EnvironmentName}.config").GetCurrentClassLogger();

            
            app.UseStaticFiles();           
            app.UseSession();
            app.UseMvc(ConfigureRoutes);
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization(); 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }

        //cbe_16161
        private void AddSingletonClases(IServiceCollection services)
        {
            services.AddSingleton<IEncrypDecrypBySymmetricKey, EncrypDecrypBySymmetricKey>();
        }

        private void AddIdentityServerCustomServices(IIdentityServerBuilder builder)
        {
            if (LoadCertificate() != null)
            {
                builder.AddSigningCredential(LoadCertificate());
            }
            else
            {
                builder.AddDeveloperSigningCredential();
                //builder.AddTemporarySigningCredential();
            }
            builder.Services.AddTransient<IResourceStore, CustomResourceStore>();
            builder.Services.AddTransient<IClientStore, CustomClientStore>();
            builder.Services.AddTransient<IResourceOwnerPasswordValidator, CustomResourceOwnerPasswordValidator>();
            builder.Services.AddTransient<IProfileService, CustomProfileService>();
            builder.Services.AddTransient<ITokenService, CustomTokenService>();
            builder.Services.AddTransient<ITokenCreationService, CustomTokenCreationService>();
            builder.Services.AddTransient<ITokenResponseGenerator, CustomTokenResponseGenerator>();
            builder.Services.AddTransient<IReferenceTokenStore, CustomReferenceTokenStore>();
            builder.Services.AddTransient<IIntrospectionRequestValidator, CustomIntrospectionRequestValidator>();
            builder.Services.AddTransient<ITokenValidator, CustomTokenValidator>();
            builder.Services.AddTransient<IRefreshTokenService, CustomRefreshTokenService>();
            builder.Services.AddTransient<IRefreshTokenStore, CustomRefreshTokenStore>();
            builder.Services.AddTransient<ITokenRequestValidator, CustomTokenRequestValidator>();
            builder.Services.AddTransient<IAuthorizationCodeStore, CustomAuthorizationCodeStore>();
            builder.Services.AddTransient<IScopeParser, DefaultScopeParser>();
            builder.Services.AddTransient<IResourceValidator, DefaultResourceValidator>();
            builder.Services.AddTransient<ISecretsListParser, DefaultSecretParser>();
            builder.Services.AddTransient<IIntrospectionResponseGenerator, CustomIntrospectionResponseGenerator>();
            builder.Services.AddTransient<IEndpointRouter, CustomEndpointRouter>();
            builder.Services.AddTransient<IEndSessionRequestValidator, CustomEndsessionRequestValidator>();
            builder.Services.AddTransient<IDiscoveryResponseGenerator, CustomDiscoveryResponseGenerator>();
            builder.Services.AddTransient<IEndpointHandler, CustomEndSessionEndpoint>();
            builder.AddEndpoint<CustomEndSessionEndpoint>("CustomEndsession", IDPConstants.ProtocolRoutePaths.EndSession);
        }
        private void ConfigureRoutes(IRouteBuilder obj)
        {
            obj.MapRoute("Default", "{controller=Account}/{action=Login}");
        }
        public X509Certificate2 LoadCertificate()
        {
            try
            {
                var certFile = Directory.GetCurrentDirectory() + @"\Certificates\MyCBTS.IDP.Login.CodeSigningCert.pfx";

                return new X509Certificate2(certFile, "11@Master");
            }
            catch (Exception) { return null; }
        }

        public string LoadDuendeLicense()
        {
            try
            {
                return DuendeHelper.ReadFileContent(Directory.GetCurrentDirectory() + @"\Certificates\DuendeKey.key");
            }
            catch (Exception) { return null; }
        }
    }
}