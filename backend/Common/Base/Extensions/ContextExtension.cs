using System;
using Microsoft.Xrm.Sdk;

namespace Common.Base.Extensions {
    public static class ContextExtension {
        /// <summary>
        /// Get a input parameter of context or your default type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="inputParameter">Name of Input Parameter</param>
        /// <returns></returns>
        public static T GetInputParameter<T>(this IPluginExecutionContext context, string inputParameter) {
            if (context.InputParameters.Contains(inputParameter) && context.InputParameters[inputParameter] is T) {
                return (T)context.InputParameters[inputParameter];
            } else {
                return default(T);
            }
        }

        /// <summary>
        /// Get a output parameter of context or your default type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="inputParameter">Name of Input Parameter</param>
        /// <returns></returns>
        public static T GetOutputParameter<T>(this IPluginExecutionContext context, string inputParameter) {
            if (context.OutputParameters.Contains(inputParameter) && context.OutputParameters[inputParameter] is T) {
                return (T)context.OutputParameters[inputParameter];
            } else {
                return default(T);
            }
        }

        /// <summary>
        /// Get a pre image of context or your default type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="inputParameter">Name of Input Parameter</param>
        /// <returns></returns>
        public static Entity GetPreEntityImage(this IPluginExecutionContext context, string ImageName) {
            if (context.PreEntityImages.Contains(ImageName)) {
                return context.PreEntityImages[ImageName];
            } else {
                return default;
            }
        }

        /// <summary>
        /// Get a post image of context or your default type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="inputParameter">Name of Input Parameter</param>
        /// <returns></returns>
        public static Entity GetPostEntityImage(this IPluginExecutionContext context, string ImageName) {
            if (context.PostEntityImages.Contains(ImageName)) {
                return context.PostEntityImages[ImageName];
            } else {
                return default;
            }
        }

        /// <summary>
        /// Get a parameter of context
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="inputParameter"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public static bool TryGetInputParameter<T>(this IPluginExecutionContext context, string inputParameter, out T output) {
            if (context.InputParameters.Contains(inputParameter) && context.InputParameters[inputParameter] is T) {
                output = (T)context.InputParameters[inputParameter];
                return true;
            } else {
                output = default(T);
                return false;
            }
        }

        /// <summary>
        /// Get a parameter of context
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="inputParameter"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public static bool TryGetOutputParameter<T>(this IPluginExecutionContext context, string inputParameter, out T output) {
            if (context.OutputParameters.Contains(inputParameter) && context.OutputParameters[inputParameter] is T) {
                output = (T)context.OutputParameters[inputParameter];
                return true;
            } else {
                output = default(T);
                return false;
            }
        }

        /// <summary>
        /// Get Pre Image of context.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="image"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public static bool TryGetPreImage(this IPluginExecutionContext context, string image, out Entity output) {
            if (context.PreEntityImages.Contains(image)) {
                output = context.PreEntityImages[image];
                return true;
            } else {
                output = null;
                return false;
            }

        }

        /// <summary>
        /// Get Post Image of context.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="image"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public static bool TryGetPostImage(this IPluginExecutionContext context, string image, out Entity output) {
            if (context.PostEntityImages.Contains(image)) {
                output = context.PostEntityImages[image];
                return true;
            } else {
                output = null;
                return false;
            }
        }
    }
}
