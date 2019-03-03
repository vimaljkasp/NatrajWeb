using Owin;
using Platform.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Platform;
using Microsoft.Owin.Security.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Net;
using System.Net.Http;
using FluentValidation.WebApi;
using System.Web.Http.ExceptionHandling;
using System.Configuration;
using Platform.DTO;

namespace PlatformWeb
{
   public class Startup
    {
       
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();
            //Microsoft.Owin.Cors.CorsOptions corsOptions = new Microsoft.Owin.Cors.CorsOptions();            
            //CorsExtensions.UseCors(appBuilder, corsOptions);
            config.EnableCors();
            //For Dependency Injection
            Register(config);

            //For JWT Security Token 
            ConfigureAuthApp(appBuilder);

            //For Creating Custom Response Header
            config.MessageHandlers.Add(new ResponseWrappingHandler());

            //For Model Validation
            config.Filters.Add(new ValidateModelStateFilter());

            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
            config.Services.Add(typeof(IExceptionLogger), new GlobalErrorLogger());

            config.MapHttpAttributeRoutes();
            config.EnableCors();
            //For Route Mechanism
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );
          



            //Invoke fluent validation
            FluentValidationModelValidatorProvider.Configure(config);
            appBuilder.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

           appBuilder.UseWebApi(config);
            appBuilder.Run(sample =>

            {

                sample.Response.ContentType = "text/plain";

                return sample.Response.WriteAsync("Hello from OWIN");

            });



        }

        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            //Customer
            container.RegisterType<ICustomerService, CustomerService>(new HierarchicalLifetimeManager());
            container.RegisterType<ICustomerBankService, CustomerBankService>(new HierarchicalLifetimeManager());
            container.RegisterType<ICustomerAgricultureService, CustomerAgricultureService>(new HierarchicalLifetimeManager());
            //container.RegisterType<ICustomerWalletService, CustomerWalletService>(new HierarchicalLifetimeManager());
            //container.RegisterType<ICustomerPaymentService, CustomerPaymentService>(new HierarchicalLifetimeManager());

            ////Site
            //container.RegisterType<ISiteService, SiteService>(new HierarchicalLifetimeManager());
            //container.RegisterType<ISiteConfigurationService, SiteConfigurationService>(new HierarchicalLifetimeManager());

            ////Product
            container.RegisterType<IProductService, ProductService>(new HierarchicalLifetimeManager());
            //container.RegisterType<IProductSiteMappingService, ProductSiteMappingService>(new HierarchicalLifetimeManager());
            //container.RegisterType<IProductOrderService, ProductOrderService>(new HierarchicalLifetimeManager());
            //container.RegisterType<IProductOrderDtlService, ProductOrderDtlService>(new HierarchicalLifetimeManager());
            //container.RegisterType<IProductSalesService, ProductSalesService>(new HierarchicalLifetimeManager());

            ////Employee
            container.RegisterType<ILoginService, LoginService>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserService, UserService>(new HierarchicalLifetimeManager());

            //container.RegisterType<IEmployeeService, EmployeeService>(new HierarchicalLifetimeManager());
            //container.RegisterType<IEmployeeRoleService, EmployeeRoleService>(new HierarchicalLifetimeManager());

            ////VLC
            container.RegisterType<IVLCService, VLCService>(new HierarchicalLifetimeManager());
            container.RegisterType<IVLCMilkCollectionService, VLCMilkCollectionService>(new HierarchicalLifetimeManager());

            container.RegisterType<IVLCAdminService, VLCAdminService>(new HierarchicalLifetimeManager());

            ////Dashboard

            //container.RegisterType<IDashboardService, DashboardService>(new HierarchicalLifetimeManager());


            //// Distribution Center
            container.RegisterType<IDistributionCenterService, DistributionCenterService>(new HierarchicalLifetimeManager());
            container.RegisterType<IDCAddressService, DCAddressServices>(new HierarchicalLifetimeManager());
            container.RegisterType<IDCOrderService, DCOrderService>(new HierarchicalLifetimeManager());
            container.RegisterType<IDCPaymentService, DCPaymentService>(new HierarchicalLifetimeManager());

            //Dock Collection
            container.RegisterType<IDockMilkCollectionService, DockMilkCollectionService>(new HierarchicalLifetimeManager());
        
            config.DependencyResolver = new UnityResolver(container);
            
        }

        public void ConfigureAuthApp(IAppBuilder appBuilder)
        {
            string UrlAddress = ConfigurationManager.AppSettings["BaseUri"].ToString();
            JwtBearerAuthenticationOptions jwtBearerAuthenticationOptions = new JwtBearerAuthenticationOptions();
            jwtBearerAuthenticationOptions.TokenValidationParameters =
               new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = UrlAddress,
                   ValidAudience = UrlAddress,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("PlatformSecretKey"))
               };


            appBuilder.UseJwtBearerAuthentication(jwtBearerAuthenticationOptions);
        }
    }

    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
            }
        }
    }


}
