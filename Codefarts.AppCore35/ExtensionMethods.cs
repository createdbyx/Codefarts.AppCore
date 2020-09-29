// <copyright file="ExtensionMethods.cs" company="Codefarts">
// Copyright (c) Codefarts
// </copyright>

namespace Codefarts.AppCore
{
    using Codefarts.AppCore.Interfaces;

    /// <summary>
    /// provides extension methods for the <see cref="ISettingsProvider"/> type.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>Gets the localized string.</summary>
        /// <summary>
        /// Gets a localized string.
        /// </summary>
        /// <param name="provider">The localization provider reference.</param>
        /// <param name="name">The name of the localized string.</param>
        /// <param name="defaultValue">The default value to return if unable to retrieve the localized string.</param>
        /// <returns>The value of the localized string.</returns>
        public static string GetString(this ILocalizationProvider provider, string name, string defaultValue)
        {
            try
            {
                if (provider == null)
                {
                    return defaultValue;

                }

                return provider.GetString(name);
            }
            catch
            {
                return defaultValue;
            }
        }

        ///// <summary>Gets the argument from a <see cref="IConstructorArguments" /> implementation.</summary>
        ///// <param name="args">The <see cref="IConstructorArguments"/> implementation.</param>
        ///// <param name="name">The name of the argument.</param>
        ///// <param name="defaultValue">The default value to return if unable to retrieve the argument.</param>
        ///// <returns>The value of the argument.</returns>
        //public static T GetArgument<T>(this IConstructorArguments args, string name, T defaultValue)
        //{
        //    try
        //    {
        //        var actualKey = args.Arguments.Keys.FirstOrDefault(x => x.Equals(name, StringComparison.OrdinalIgnoreCase));
        //        return (T)args.Arguments[actualKey];
        //    }
        //    catch
        //    {
        //        return defaultValue;
        //    }
        //}

        //private static MemberInfo GetMemberInfo(Expression expression)
        //{
        //    var lambda = (LambdaExpression)expression;

        //    MemberExpression memberExpression;
        //    var unaryExpression = lambda.Body as UnaryExpression;
        //    if (unaryExpression != null)
        //    {
        //        memberExpression = (MemberExpression)unaryExpression.Operand;
        //    }
        //    else
        //    {
        //        memberExpression = (MemberExpression)lambda.Body;
        //    }

        //    return memberExpression.Member;
        //}
    }
}
