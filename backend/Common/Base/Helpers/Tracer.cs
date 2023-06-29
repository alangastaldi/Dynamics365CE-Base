using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;

namespace Common.Base.Helpers {
    public class Tracer {
        private ITracingService tracingService;
        private IOrganizationService systemService;

        public Tracer(ITracingService tracingService, IOrganizationService systemService) {
            this.tracingService = tracingService;
            this.systemService = systemService;
        }

        public void TraceContext(IPluginExecutionContext context) {
            try {
                tracingService.Trace(ValueToString(context));
            } catch (Exception e) {
                tracingService.Trace("Error: An error occurred while trying to log the context details. Details:\n");
                tracingService.Trace(e.Message);
            }
        }

        public string ValueToString(object value, string idention = "", string name = "") {
            if (name != "") name = "[" + name + "] ";
            List<string> list = new List<string>();
            string i = idention + "    ";

            switch (value) {
                case byte _:
                    return "byte";
                case String varString:
                    return $"{idention + name}(String) {varString}";
                case int varInt:
                    return $"{idention + name}(Int) {varInt}";
                case Double varDouble:
                    return $"{idention + name}(Double) {varDouble}";
                case Decimal varDecimal:
                    return $"{idention + name}(Decimal) {varDecimal}";
                case float varFloat:
                    return $"{idention + name}(Float) {varFloat}";
                case Boolean varBoolean:
                    return $"{idention + name}(Boolean) {varBoolean}";
                case DateTime varDateTime:
                    return $"{idention + name}(DateTime) {varDateTime.ToString("O")}";
                case Money varMoney:
                    return $"{idention + name}(Money) {varMoney.Value}";
                case OptionSetValue varOptionSetValue:
                    return $"{idention + name}(OptionSetValue) {varOptionSetValue.Value}";
                case AliasedValue varAliasedValue:
                    return $"{idention + name}(AliasedValue) [{varAliasedValue.EntityLogicalName}/{varAliasedValue.AttributeLogicalName}]: {ValueToString(varAliasedValue.Value, "")}";
                case FetchExpression varFetchExpression:
                    return $"{idention + name}(FetchExpression) {varFetchExpression.Query}";
                case Relationship varRelationship:
                    return $"{idention + name}(Relationship) {varRelationship.SchemaName}";
                case EntityReference varEntityReference:
                    return $"{idention + name}(EntityReference) [{varEntityReference?.LogicalName}]:{varEntityReference?.LogicalName}/{varEntityReference?.Id}";
                case ColumnSet varColumnSet:
                    if (varColumnSet.AllColumns) return $"{idention + name}(ColumnSet): AllColumns";
                    else return idention + $"{idention + name}(ColumnSet):" + string.Join(",", varColumnSet.Columns.ToArray());
                case Entity varEntity:
                    foreach (var attribute in varEntity.Attributes) {
                        list.Add(ValueToString(attribute.Value, i, attribute.Key));
                    }
                    return $"{idention + name}(Entity) [{varEntity?.LogicalName}]:{varEntity.Id}\n{string.Join("\n", list.ToArray())}";
                case EntityCollection varEntityCollection:
                    foreach (Entity entity in varEntityCollection.Entities) {
                        list.Add(ValueToString(entity, i));
                    }
                    return $"{idention + name}(EntityCollection){varEntityCollection.EntityName}\n{string.Join("\n", list.ToArray())}";
                case EntityImageCollection varEntityImageCollection:
                    foreach (KeyValuePair<string, Entity> entityImage in varEntityImageCollection) {
                        list.Add(ValueToString(entityImage.Value, i, entityImage.Key));
                    }
                    return $"{idention + name}(EntityImageCollection)\n{string.Join("\n", list.ToArray())}";
                case ParameterCollection param:
                    foreach (KeyValuePair<string, object> p in param) {
                        list.Add(ValueToString(p.Value, i, p.Key));
                    }
                    return $"{idention + name}(ParameterCollection)\n{string.Join("\n", list.ToArray())}";
                case IPluginExecutionContext context:
                    Entity InitiatingUserId = systemService.Retrieve("systemuser", context.InitiatingUserId, new ColumnSet("fullname", "internalemailaddress"));
                    Entity UserId = systemService.Retrieve("systemuser", context.UserId, new ColumnSet("fullname", "internalemailaddress"));

                    list.Add("");
                    list.Add("#ContextInfo ---------------------------");
                    list.Add($"InitiatingUserId: {context.InitiatingUserId}");
                    list.Add($"InitiatingUserId.fullname: {InitiatingUserId.GetAttributeValue<string>("fullname")}");
                    list.Add($"InitiatingUserId.internalemailaddress: {InitiatingUserId.GetAttributeValue<string>("internalemailaddress")}");
                    list.Add($"UserId: {context.InitiatingUserId}");
                    list.Add($"UserId.fullname: {UserId.GetAttributeValue<string>("fullname")}");
                    list.Add($"UserId.internalemailaddress: {UserId.GetAttributeValue<string>("internalemailaddress")}");
                    list.Add($"MessageName: {context.MessageName}");
                    list.Add($"Stage: {context.Stage}");
                    list.Add($"PrimaryEntityName: {context.PrimaryEntityName}");
                    list.Add($"PrimaryEntityId: {context.PrimaryEntityId}");
                    list.Add($"Depth: {context.Depth}");
                    list.Add(ValueToString(context.InputParameters, i, "InputParameters"));
                    list.Add(ValueToString(context.PreEntityImages, i, "PreEntityImages"));
                    list.Add(ValueToString(context.PostEntityImages, i, "PostEntityImages"));
                    return string.Join("\n", list.ToArray());
                default:
                    return $"{idention + name} ({value.GetType()}) {value}";
            }
        }

    }
}
