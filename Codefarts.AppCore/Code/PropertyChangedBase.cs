namespace Codefarts.AppCore
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    /// <summary>
    /// A base class that implements the infrastructure for property change notification and automatically performs UI thread marshalling.
    /// </summary>
#if !ANDROID44
    [DataContract]
#endif
    public class PropertyChangedBase : INotifyPropertyChanged
    {
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
        [XmlIgnore]
        public virtual bool IsNotifying { get; set; }

        /// <summary>
        /// Raises a change notification indicating that collection has changed.
        /// </summary>
        /// <remarks>Useful for when you make sweeping changes to a collection and want any UI binding to update.
        /// Property name will be <see cref="string.Empty"/>.</remarks>
        public virtual void Refresh()
        {
            this.NotifyOfPropertyChange(string.Empty);
        }

        /// <summary>
        /// Notifies subscribers of the property change.
        /// </summary>
        /// <param name="propertyName">Name of the property that changed.</param>
        public virtual void NotifyOfPropertyChange(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (this.IsNotifying && handler != null)
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
