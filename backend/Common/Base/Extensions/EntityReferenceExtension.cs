using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace Common.Base.Extensions {
    public static class EntityReferenceExtension {

        /// <summary>
        /// Returns an empty Entity
        /// </summary>
        /// <param name="entityReference"></param>
        /// <returns></returns>
        public static Entity EntityReferenceToEntity(this EntityReference entityReference) {
            if (entityReference.LogicalName == null) throw new Exception("Error: Could not find LogicalName property of EntityReference.");
            return new Entity(entityReference.LogicalName, entityReference.Id);
        }

        /// <summary>
        /// Retrieve an Entity with selected attributes
        /// </summary>
        /// <param name="entityReference"></param>
        /// <param name="organizationService"></param>
        /// <param name="columnSet"></param>
        /// <returns></returns>
        public static Entity EntityReferenceToEntity(this EntityReference entityReference, IOrganizationService organizationService, ColumnSet columnSet) {
            if (entityReference.LogicalName == null) throw new Exception("Error: Could not find LogicalName property of EntityReference.");
            if (entityReference.Id == null) throw new Exception("Error: Could not find Id property of EntityReference.");
            return organizationService.Retrieve(entityReference.LogicalName, entityReference.Id, columnSet);
        }

        /// <summary>
        /// Retrieve an Entity with all attributes
        /// </summary>
        /// <param name="entityReference"></param>
        /// <param name="organizationService"></param>
        /// <param name="columnSet"></param>
        /// <returns></returns>
        public static Entity EntityReferenceToEntity(this EntityReference entityReference, IOrganizationService organizationService) {
            return entityReference.EntityReferenceToEntity(organizationService, new ColumnSet(true));
        }
    }
}
