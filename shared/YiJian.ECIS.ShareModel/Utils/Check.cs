namespace YiJian.ECIS;

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Volo.Abp;

/// <summary>
/// Defines the <see cref="Check" />.
/// </summary>
[DebuggerStepThrough]
public static class Check
{
    /// <summary>
    /// 非空.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    /// <param name="value">The value.</param>
    /// <param name="parameterName">The parameterName<see cref="string"/>.</param>
    [ContractAnnotation("value:null => halt")]
    public static T NotNull<T>(
        T value,
        [InvokerParameterName][NotNull] string parameterName)
    {
        if (value == null)
        {
            throw new UserFriendlyException(parameterName + "不能为空");
        }

        return value;
    }

    /// <summary>
    /// 非空.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    /// <param name="value">The value.</param>
    /// <param name="parameterName">The parameterName<see cref="string"/>.</param>
    /// <param name="message">The message<see cref="string"/>.</param>
    [ContractAnnotation("value:null => halt")]
    public static T NotNull<T>(
        T value,
        [InvokerParameterName][NotNull] string parameterName,
        string message)
    {
        if (value == null)
        {
            throw new UserFriendlyException(parameterName + message);
        }

        return value;
    }

    /// <summary>
    /// 非空.
    /// </summary>
    /// <param name="value">The value<see cref="string"/>.</param>
    /// <param name="parameterName">The parameterName<see cref="string"/>.</param>
    /// <param name="maxLength">The maxLength<see cref="int"/>.</param>
    /// <param name="minLength">The minLength<see cref="int"/>.</param>
    /// <returns>The <see cref="string"/>.</returns>
    [ContractAnnotation("value:null => halt")]
    public static string NotNull(
        string value,
        [InvokerParameterName][NotNull] string parameterName,
        int maxLength = int.MaxValue,
        int minLength = 0)
    {
        if (value == null)
        {
            throw new UserFriendlyException($"{parameterName} 不能为空!");
        }

        if (value.Length > maxLength)
        {
            throw new UserFriendlyException($"{parameterName} 长度不能超过 {maxLength}!", parameterName);
        }

        if (minLength > 0 && value.Length < minLength)
        {
            throw new UserFriendlyException($"{parameterName} 长度至少为 {minLength}!");
        }

        return value;
    }

    /// <summary>
    /// 非空.
    /// </summary>
    /// <param name="value">The value<see cref="string"/>.</param>
    /// <param name="parameterName">The parameterName<see cref="string"/>.</param>
    /// <param name="maxLength">The maxLength<see cref="int"/>.</param>
    /// <param name="minLength">The minLength<see cref="int"/>.</param>
    /// <returns>The <see cref="string"/>.</returns>
    [ContractAnnotation("value:null => halt")]
    public static string NotNullOrWhiteSpace(
        string value,
        [InvokerParameterName][NotNull] string parameterName,
        int maxLength = int.MaxValue,
        int minLength = 0)
    {
        if (value.IsNullOrWhiteSpace())
        {
            throw new UserFriendlyException($"{parameterName} 不能为空!");
        }

        if (value.Length > maxLength)
        {
            throw new UserFriendlyException($"{parameterName} 长度不能超过 {maxLength}!");
        }

        if (minLength > 0 && value.Length < minLength)
        {
            throw new UserFriendlyException($"{parameterName} 长度至少为 {minLength}!");
        }

        return value;
    }

    /// <summary>
    /// 非空.
    /// </summary>
    /// <param name="value">The value<see cref="string"/>.</param>
    /// <param name="parameterName">The parameterName<see cref="string"/>.</param>
    /// <param name="maxLength">The maxLength<see cref="int"/>.</param>
    /// <param name="minLength">The minLength<see cref="int"/>.</param>
    /// <returns>The <see cref="string"/>.</returns>
    [ContractAnnotation("value:null => halt")]
    public static string NotNullOrEmpty(
        string value,
        [InvokerParameterName][NotNull] string parameterName,
        int maxLength = int.MaxValue,
        int minLength = 0)
    {
        if (value.IsNullOrEmpty())
        {
            throw new UserFriendlyException($"{parameterName} 不能为空!");
        }

        if (value.Length > maxLength)
        {
            throw new UserFriendlyException($"{parameterName} 长度不能超过 {maxLength}!");
        }

        if (minLength > 0 && value.Length < minLength)
        {
            throw new UserFriendlyException($"{parameterName} 长度至少为 {minLength}!");
        }

        return value;
    }

    /// <summary>
    /// 非空.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    /// <param name="value">The value<see cref="ICollection{T}"/>.</param>
    /// <param name="parameterName">The parameterName<see cref="string"/>.</param>
    /// <returns>The <see cref="ICollection{T}"/>.</returns>
    [ContractAnnotation("value:null => halt")]
    public static ICollection<T> NotNullOrEmpty<T>(ICollection<T> value, [InvokerParameterName][NotNull] string parameterName)
    {
        if (value.IsNullOrEmpty())
        {
            throw new UserFriendlyException(parameterName + " 不能为空!");
        }

        return value;
    }

    /// <summary>
    /// The AssignableTo.
    /// </summary>
    /// <typeparam name="TBaseType">.</typeparam>
    /// <param name="type">The type<see cref="Type"/>.</param>
    /// <param name="parameterName">The parameterName<see cref="string"/>.</param>
    /// <returns>The <see cref="Type"/>.</returns>
    [ContractAnnotation("type:null => halt")]
    public static Type AssignableTo<TBaseType>(
        Type type,
        [InvokerParameterName][NotNull] string parameterName)
    {
        NotNull(type, parameterName);

        if (!type.IsAssignableTo<TBaseType>())
        {
            throw new ArgumentException($"{parameterName} (type of {type.AssemblyQualifiedName}) should be assignable to the {typeof(TBaseType).GetFullNameWithAssemblyName()}!");
        }

        return type;
    }

    /// <summary>
    /// 长度
    /// </summary>
    /// <param name="value">The value<see cref="string"/>.</param>
    /// <param name="parameterName">The parameterName<see cref="string"/>.</param>
    /// <param name="maxLength">The maxLength<see cref="int"/>.</param>
    /// <param name="minLength">The minLength<see cref="int"/>.</param>
    /// <returns>The <see cref="string"/>.</returns>
    public static string Length(
        [CanBeNull] string value,
        [InvokerParameterName][NotNull] string parameterName,
        int maxLength,
        int minLength = 0)
    {
        if (minLength > 0)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new UserFriendlyException(parameterName + " 不能为空!");
            }

            if (value.Length < minLength)
            {
                throw new UserFriendlyException($"{parameterName} 长度至少为 {minLength}!");
            }
        }

        if (value != null && value.Length > maxLength)
        {
            throw new UserFriendlyException($"{parameterName} 长度不能超过 {maxLength}!");
        }

        return value;
    }
}
