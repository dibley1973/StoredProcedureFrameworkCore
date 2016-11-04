using System;
using System.Reflection;
using Dibware.StoredProcedureFrameworkCore.Generics;
using System.Linq;

namespace Dibware.StoredProcedureFrameworkCore.Helpers.AttributeHelpers
{
    public abstract class PropertyAttributeFinder<TAttribute>
        where TAttribute : class 
    {
        #region Fields

        private readonly PropertyInfo _property;
        private TAttribute _attribute;

        #endregion

        #region Construtors

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyAttributeFinder{TAttribute}"/> class.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <exception cref="System.ArgumentNullException">property</exception>
        protected PropertyAttributeFinder(PropertyInfo property)
        {
            if (property == null) throw new ArgumentNullException(nameof(property));

            _property = property;

            SetAttributeIfExists();
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Gets a value indicating whether this instance has found an attribute.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has found an attribute; otherwise, <c>false</c>.
        /// </value>
        public bool HasFoundAttribute => _attribute != null;

        /// <summary>
        /// Gets the result containing the attribute if one was found.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public Maybe<TAttribute> GetResult()
        {
            return !HasFoundAttribute
                ? new Maybe<TAttribute>()
                : new Maybe<TAttribute>(_attribute);
        }

        #endregion

        #region Private Members

        private void SetAttributeIfExists()
        {
            var attributes = _property.GetCustomAttributes().Where(a => a.GetType() == typeof(TAttribute)).ToList();
            var noAttributesFound = !attributes.Any();
            if (noAttributesFound) return;

            _attribute = attributes.First() as TAttribute;
        }

        #endregion
    }
}