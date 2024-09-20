using System;
using System.Collections.Generic;
using System.Linq;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public static class ObjectCoalesceExtension<T>
    {
        /// <summary>
        /// Deep Copy the top level properties from this object only if the corresponding property on the target object IS NULL.
        /// </summary>
        /// <param name="source">the source object to copy from</param>
        /// <param name="target">the target object to update</param>
        /// <returns>A reference to the Target instance for chaining, no changes to this instance.</returns>
        public static void CoalesceTo(object source, object target, List<string> strings = null, string errorMessage = "", bool setNotNullTarget = true, StringComparison propertyComparison = StringComparison.OrdinalIgnoreCase)
        {
            var sourceType = source.GetType();
            var targetType = target.GetType();
            var targetProperties = targetType.GetProperties();
            foreach (var sourceProp in sourceType.GetProperties())
            {
                if (sourceProp.CanRead)
                {
                    var sourceValue = sourceProp.GetValue(source);

                    // Don't copy across nulls or defaults
                    if (!IsNull(sourceValue, sourceProp.PropertyType))
                    {
                        var targetProp = targetProperties.FirstOrDefault(x => x.Name.Equals(sourceProp.Name, propertyComparison));
                        if (targetProp != null && targetProp.CanWrite)
                        {
                            if (!targetProp.CanRead)
                                continue; // special case, if we cannot verify the destination, assume it has a value.
                            else if (targetProp.PropertyType.IsArray || targetProp.PropertyType.IsGenericType // It is ICollection<T> or IEnumerable<T>
                                                                        && targetProp.PropertyType.GenericTypeArguments.Any()
                                                                        && targetProp.PropertyType.GetGenericTypeDefinition() != typeof(Nullable<>) // because that will also resolve GetElementType!
                                    )
                            {
                                var existingValue = targetProp.GetValue(target);
                                var sourceList = sourceValue as IEnumerable<object>;
                                var existingList = existingValue as IEnumerable<object>;
                                if (!IsNull(sourceList) && !IsNull(existingList))
                                {
                                    if (sourceList.Count() == existingList.Count())
                                    {
                                        for (int i = 0; i < sourceList.Count(); i++)
                                        {
                                            CoalesceTo(sourceList.ToList()[i], existingList.ToList()[i], strings);
                                        }
                                    }
                                    else
                                    {
                                        targetProp.SetValue(target, sourceValue);
                                    }

                                }

                            }
                            // continue; // special case, skip arrays and collections...
                            else
                            {
                                // You can do better than this, for now if conversion fails, just skip it
                                try
                                {
                                    var existingValue = targetProp.GetValue(target);
                                    if (IsValueType(targetProp.PropertyType))
                                    {
                                        // check that the destination is NOT already set.
                                        if ((IsNull(existingValue, targetProp.PropertyType) || setNotNullTarget) && (strings != null && !strings.Contains(targetProp.Name)))
                                        {
                                            // we do not overwrite a non-null destination value
                                            object targetValue = sourceValue;
                                            if (!targetProp.PropertyType.IsAssignableFrom(sourceProp.PropertyType))
                                            {
                                                // TODO: handle specific types that don't go across.... or try some brute force type conversions if neccessary
                                                if (targetProp.PropertyType == typeof(string))
                                                    targetValue = targetValue.ToString();
                                                else
                                                    targetValue = Convert.ChangeType(targetValue, targetProp.PropertyType);
                                            }

                                            targetProp.SetValue(target, targetValue);
                                        }
                                    }
                                    else if (!IsValueType(sourceProp.PropertyType))
                                    {
                                        // deep clone
                                        if (existingValue == null)
                                        {
                                            targetProp.SetValue(target, Activator.CreateInstance(targetProp.PropertyType));
                                            existingValue = targetProp.GetValue(target);
                                        }

                                        CoalesceTo(sourceValue, existingValue, strings);
                                    }
                                }
                                catch (Exception e)
                                {
                                    throw new Exception(errorMessage, e);
                                }

                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Check if a boxed value is null or not
        /// </summary>
        /// <remarks>
        /// Evaluate your own logic or definition of null in here.
        /// </remarks>
        /// <param name="value">Value to inspect</param>
        /// <param name="valueType">Type of the value, pass it in if you have it, otherwise it will be resolved through reflection</param>
        /// <returns>True if the value is null or primitive default, otherwise False</returns>
        public static bool IsNull(object value, Type valueType = null)
        {
            if (value is null)
                return true;

            if (valueType == null) valueType = value.GetType();

            if (valueType.IsPrimitive || valueType.IsEnum || valueType.IsValueType)
            {
                // Handle nullable types like float? or Nullable<Int>
                if (valueType.IsGenericType)
                    return value is null;
                else
                    return Activator.CreateInstance(valueType).Equals(value);
            }

            // treat empty string as null!
            if (value is string s)
                return String.IsNullOrWhiteSpace(s);

            return false;
        }
        /// <summary>
        /// Check if a type should be copied by value or if it is a complexe type that should be deep cloned
        /// </summary>
        /// <remarks>
        /// Evaluate your own logic or definition of Object vs Value/Primitive here.
        /// </remarks>
        /// <param name="valueType">Type of the value to check</param>
        /// <returns>True if values of this type can be straight copied, false if they should be deep cloned</returns>
        public static bool IsValueType(Type valueType)
        {
            // TODO: any specific business types that you want to treat as value types?

            // Standard .Net Types that can be treated as value types
            if (valueType.IsPrimitive || valueType.IsEnum || valueType.IsValueType || valueType == typeof(string))
                return true;

            // Support Nullable Types as Value types (Type.IsValueType) should deal with this, but just in case
            if (valueType.HasElementType // It is array/enumerable/nullable
                && valueType.IsGenericType && valueType.GetGenericTypeDefinition() == typeof(Nullable<>))
                return true;


            return false;
        }
    }
}
