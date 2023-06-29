using System;
using Microsoft.Xrm.Sdk;
using Common.Base.Helpers;
using Common.Base.Extensions;
using Common.BusinessRules.Example;

namespace Example {
    public class Example : PluginBase {
        public override void ExecutePlugin() {
            Entity target = Context.GetInputParameter<Entity>("Target");

            ExampleBusinessRule exampleBusinessRule = new ExampleBusinessRule(target);

            exampleBusinessRule.Prepare();

            exampleBusinessRule.Update(UserService);
        }
    }
}
