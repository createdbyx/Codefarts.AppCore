// <copyright file="PropertyChangedBase.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace Codefarts.AppCore
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using Codefarts.AppCore.Interfaces;

    /// <summary>
    /// A base class that implements the infrastructure for property change notification and automatically performs UI thread marshalling.
    /// </summary>
    public class PropertyChangedBase : INotifyPropertyChangedEx
    {
        private bool isNotifying;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyChangedBase"/> class.
        /// </summary>
        public PropertyChangedBase()
        {
            this.IsNotifying = true;
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public virtual event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets a value indicating whether property changes will raise notification events.
        /// </summary>
        public virtual bool IsNotifying
        {
            get
            {
                return this.isNotifying;
            }

            set
            {
                var currentValue = this.isNotifying;
                if (currentValue != value)
                {
                    this.isNotifying = value;
                    this.NotifyOfPropertyChange(() => this.IsNotifying);
                }
            }
        }

        /// <summary>
        /// Raises a change notification indicating that collection has changed.
        /// </summary>
        /// <remarks>Useful for when you make sweeping changes to a collection and want any UI binding to update.
        /// Property name will be <see cref="string.Empty"/>.</remarks>
        public virtual void Refresh()
        {
            // this.PropertyChanged.Notify(string.Empty);
            this.NotifyOfPropertyChange(string.Empty);
        }

        /// <summary>
        /// Notifies subscribers of the property change.
        /// </summary>
        /// <param name="propertyName">Name of the property that changed.</param>
        public virtual void NotifyOfPropertyChange(string propertyName = null)
        {
            var handler = this.PropertyChanged;
            var notifying = this.IsNotifying;
            if (notifying && handler != null)
            {
                this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Notifies subscribers of the property change.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="property">The property expression.</param>
        public void NotifyOfPropertyChange<TProperty>(Expression<Func<TProperty>> property)
        {
            // this.PropertyChanged.Notify(this, property);
           // this.PropertyChanged.Notify(this, this.GetMemberInfo(property).Name);
             this.NotifyOfPropertyChange(this.GetMemberInfo(property).Name);
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged" /> event directly.
        /// </summary>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Gets the member information for a given expression.
        /// </summary>
        /// <param name="expression">The expression to get the member info from.</param>
        /// <returns>A reference to a <see cref="MemberInfo"/> object.</returns>
        private MemberInfo GetMemberInfo(Expression expression)
        {
            var lambda = (LambdaExpression)expression;

            MemberExpression memberExpression;
            var unaryExpression = lambda.Body as UnaryExpression;
            if (unaryExpression != null)
            {
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            return memberExpression.Member;
        }
    }
}
