using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace Common.Base.Helpers {
    public abstract class PluginBase : IPlugin {

        public IServiceProvider ServiceProvider { get; private set; }
        public IPluginExecutionContext Context { get; private set; }
        public ITracingService TracingService { get; private set; }
        public IOrganizationService UserService { get; private set; }
        public IOrganizationService SystemService { get; private set; }

        public void Execute(IServiceProvider sp) {
            try {
                ServiceProvider = sp;
                Context = (IPluginExecutionContext)ServiceProvider.GetService(typeof(IPluginExecutionContext));
                IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)ServiceProvider.GetService(typeof(IOrganizationServiceFactory));
                TracingService = (ITracingService)ServiceProvider.GetService(typeof(ITracingService));
                UserService = serviceFactory.CreateOrganizationService(Context.UserId);
                SystemService = serviceFactory.CreateOrganizationService(null);

                if (InitialValidation()) {
                    ExecutePlugin();
                }
            } catch (Exception exception) {
                OnError(exception);
            }

        }
        /// <summary>
        /// Checks if a plugin should be executed.
        /// </summary>
        /// <returns></returns>
        public virtual bool InitialValidation() {
            switch (Context.MessageName.ToLower()) {
                case "create":
                case "update":
                    return Context.InputParameters.Contains("Target") && Context.InputParameters["Target"] is Entity;
                case "delete":
                case "assign":
                case "grantaccess":
                case "modifyaccess":
                case "retrieve":
                case "retrieveprincipalaccess":
                case "revokeaccess":
                    return Context.InputParameters.Contains("Target") && Context.InputParameters["Target"] is EntityReference;
                case "retrievemultiple":
                    return Context.InputParameters.Contains("Query") && Context.InputParameters["Query"] is FetchExpression;
                default:
                    return true;
            }
        }

        /// <summary>
        /// Plugin logic.
        /// </summary>
        public abstract void ExecutePlugin();

        /// <summary>
        /// If an error has caused this method, it will be called. 
        /// </summary>
        /// <param name="exception"></param>
        public virtual void OnError(Exception exception) {
            Tracer tracer = new Tracer(TracingService, SystemService);
            tracer.TraceContext(Context);
            throw exception;
        }
    }
}
