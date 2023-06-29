using System;
using System.Collections.Specialized;
using System.Configuration;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using Common.Base.Extensions;
using Microsoft.Xrm.Sdk.Query;
using System.Collections.Generic;

namespace Tests {
    public class Tester {

        private CrmServiceClient CrmServiceClient;
        private bool Log = false;

        public static CrmServiceClient GetCrmServiceClient(string name) {
            if (!name.StartsWith("Environments/")) name = "Environments/" + name;

            NameValueCollection settings = ConfigurationManager.GetSection(name) as NameValueCollection;

            if (settings != null) {
                if (settings["OrganizationUrl"] == null) throw new Exception("Error: The key 'OrganizationUrl' is required.");
                if (settings["AppId"] == null) throw new Exception("Error: The key 'AppId' is required.");
                if (settings["AppSecret"] == null) throw new Exception("Error: The key 'AppSecret' is required.");

                CrmServiceClient crmServiceClient = new CrmServiceClient(new Uri(settings["OrganizationUrl"]), settings["AppId"], settings["AppSecret"], false, @"%appdata%\Microsoft\UserSecrets\Dynamics365CEBase\token.bin");

                if (crmServiceClient.IsReady) {
                    return crmServiceClient;
                } else {
                    throw new Exception(crmServiceClient.LastCrmError);
                }
            } else {
                throw new Exception("Error: Could not find configuration " + name + ".");
            }
        }

        public static CrmServiceClient GetCrmServiceClient() {
            return GetCrmServiceClient("Default");
        }

        /// <summary>
        /// Create a Tester class with environment
        /// </summary>
        /// <param name="name">Name of Environment</param>
        public Tester(string name) {
            CrmServiceClient = GetCrmServiceClient(name);
        }

        /// <summary>
        /// Create a Tester class with Default environment.
        /// </summary>
        public Tester() : this("Default") {

        }

        /// <summary>
        /// Create a new entity.
        /// </summary>
        /// <param name="entity">Entity to create</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public bool Create(Entity entity) {
            try {
                entity.Id = CrmServiceClient.Create(entity);
                return entity.Id != Guid.Empty;
            } catch (Exception) {
                if (Log) Console.WriteLine(CrmServiceClient.LastCrmError);
                return false;
            }
        }

        /// <summary>
        /// Update a entity.
        /// </summary>
        /// <param name="entity">Entity to update</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public bool Update(Entity entity) {
            try {
                CrmServiceClient.Update(entity);
                return true;
            } catch (Exception) {
                if (Log) Console.WriteLine(CrmServiceClient.LastCrmError);
                return false;
            }
        }

        /// <summary>
        /// Update a entity.
        /// </summary>
        /// <param name="entity">Entity to update</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public bool Delete(Entity entity) {
            try {
                return CrmServiceClient.DeleteEntity(entity.LogicalName, entity.Id);
            } catch (Exception) {
                if (Log) Console.WriteLine(CrmServiceClient.LastCrmError);
                return false;
            }
        }


        /// <summary>
        /// Retrieve a entity
        /// </summary>
        /// <param name="entityName">LogicalName of entity</param>
        /// <param name="id">Id of entity</param>
        /// <param name="columnSet">ColumnSet to retrieve</param>
        /// <returns>Returns a entity on success or null on failure.</returns>
        public Entity Retrieve(string entityName, Guid id, ColumnSet columnSet) {
            try {
                return CrmServiceClient.Retrieve(entityName, id, columnSet);
            } catch (Exception) {
                if (Log) Console.WriteLine(CrmServiceClient.LastCrmError);
                return null;
            }
        }

        /// <summary>
        /// Retrieve a multiple entities
        /// </summary>
        /// <param name="query">Query to retrieve</param>
        /// <returns>Returns a EntityCollection on success or null on failure.</returns>
        public EntityCollection RetrieveMultiple(QueryExpression query) {
            try {
                return CrmServiceClient.RetrieveMultiple(query);
            } catch (Exception) {
                if (Log) Console.WriteLine(CrmServiceClient.LastCrmError);
                return null;
            }
        }

        /// <summary>
        /// Execute a request
        /// </summary>
        /// <param name="request">Request to execute</param>
        /// <returns>Returns a OrganizationResponse on success or null on failure.</returns>
        public OrganizationResponse Execute(OrganizationRequest request) {
            try {
                return CrmServiceClient.Execute(request);
            } catch (Exception) {
                if (Log) Console.WriteLine(CrmServiceClient.LastCrmError);
                return null;
            }
        }

        /// <summary>
        /// Compare the attributes of 2 entities
        /// </summary>
        /// <param name="entity1">Entity 1</param>
        /// <param name="entity2">Entity 2</param>
        /// <returns>Returns true on success or false on failure.</returns>
        public bool CompareAttributes(Entity entity1, Entity entity2) {
            try {
                foreach (KeyValuePair<string, object> attribute in entity1.Attributes) {
                    if (entity1[attribute.Key] != entity2[attribute.Key]) {
                        return false;
                    }
                }

                foreach (KeyValuePair<string, object> attribute in entity2.Attributes) {
                    if (entity2[attribute.Key] != entity1[attribute.Key]) {
                        return false;
                    }
                }

                return true;
            } catch (Exception) {
                if (Log) Console.WriteLine(CrmServiceClient.LastCrmError);
                return false;
            }
        }

        /// <summary>
        /// Checks if a register exist
        /// </summary>
        /// <param name="logicalName">Name of Entity</param>
        /// <param name="id">Id of register</param>
        /// <returns></returns>
        public bool Exists(string logicalName, Guid id) {
            try {
                Entity newEntity = CrmServiceClient.Retrieve(logicalName, id, new ColumnSet(logicalName + "id"));
                return newEntity.Id == id;
            } catch (Exception) {
                if (Log) Console.WriteLine(CrmServiceClient.LastCrmError);
                return false;
            }
        }

        /// <summary>
        /// Enables or disables error logs
        /// </summary>
        /// <param name="log">True or False</param>
        /// <returns></returns>
        public Tester LogError(bool log) {
            Log = log;
            return this;
        }
    }
}