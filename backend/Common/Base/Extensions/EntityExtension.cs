using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace Common.Base.Extensions {
    public static class EntityExtension {

        /// <summary>
        /// Sets the value of an attribute if the attribute does not exist or has a different value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        public static void SetValue<T>(this Entity entity, string fieldName, T value) {
            if (entity.TryGetAttributeValue(fieldName, out T entityValue)) {
                if (!EqualityComparer<T>.Default.Equals(value, entityValue)) {
                    entity[fieldName] = value;
                }
            } else {
                entity[fieldName] = value;
            }
        }

        /// <summary>
        /// Checks if the entity contains any of the attributes
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static bool ContainsAny(this Entity entity, ColumnSet columns) {
            foreach (string attribute in columns.Columns) {
                if (entity.Attributes.Contains(attribute)) return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if the entity contains all attributes
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static bool ContainsAll(this Entity entity, ColumnSet columns) {
            foreach (string attribute in columns.Columns) {
                if (!entity.Attributes.Contains(attribute)) return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if an attribute has a null or default value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static bool IsNullOrDefault<T>(this Entity entity, string fieldName) {
            if (entity.TryGetAttributeValue(fieldName, out T value)) {
                return EqualityComparer<T>.Default.Equals(value, default(T));
            } else {
                return true;
            }
        }

        /// <summary>
        /// Retuns a ColumnSet of a entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns></returns>
        public static ColumnSet GetColumnSet(this Entity entity) {
            ColumnSet columnSet = new ColumnSet();
            foreach (KeyValuePair<string, object> attribute in entity.Attributes) {
                columnSet.AddColumn(attribute.Key);
            }

            return columnSet;
        }
    }
}
