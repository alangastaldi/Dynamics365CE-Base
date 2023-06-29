using System;
using Microsoft.Xrm.Sdk;
using Common.Base.Extensions;

namespace Common.BusinessRules.Example {
    public class ExampleBusinessRule {

        private Entity MyEntity;

        public ExampleBusinessRule(Entity entity) {
            MyEntity = entity;
        }

        public void Prepare() {
            MyEntity.SetValue("name", "My Name");
        }

        public void Update(IOrganizationService organizationService) {
            organizationService.Update(MyEntity);
        }
    }
}
