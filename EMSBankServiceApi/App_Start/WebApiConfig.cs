using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace EMSBankServiceApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "RequestSessionApi",
                routeTemplate: "api/RequestSession/{loginId}/{password}",
                defaults: new { loginId = RouteParameter.Optional, password = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "VerifySessionApi",
                routeTemplate: "api/VerifySession/{sessionKey}",
                defaults: new { sessionKey = RouteParameter.Optional}
            );

            config.Routes.MapHttpRoute(
                name: "VerifyStudentApi",
                routeTemplate: "api/VerifyStudent/{sessionKey}/{StudentId}",
                defaults: new { loginId = RouteParameter.Optional, password = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "PostPaymentApi",
                routeTemplate: "api/PostPayment/{sessionKey}/{StudentId}/{Amount}/{TransactionCode}/{BankName}",
                defaults: new
                {
                    sessionKey = RouteParameter.Optional,
                    StudentId = RouteParameter.Optional,
                    Amount = RouteParameter.Optional,
                    TransactionCode = RouteParameter.Optional,
                    BankName = RouteParameter.Optional
                }
            );

            config.Routes.MapHttpRoute(
                name: "CancelPaymentApi",
                routeTemplate: "api/CancelPayment/{sessionKey}/{transactionCode}/{refConfirmationNo}",
                defaults: new
                {
                    sessionKey = RouteParameter.Optional, 
                    TransactionCode = RouteParameter.Optional,
                    refConfirmationNo = RouteParameter.Optional
                }
            );

            config.Routes.MapHttpRoute(
                name: "VerifyPaymentApi",
                routeTemplate: "api/VerifyPayment/{sessionKey}/{StudentId}/{Amount}/{TransactionCode}/{BankName}",
                defaults: new
                {
                    sessionKey = RouteParameter.Optional,
                    StudentId = RouteParameter.Optional,
                    Amount = RouteParameter.Optional,
                    TransactionCode = RouteParameter.Optional,
                    BankName = RouteParameter.Optional 
                }
            );
        }
    }
}
