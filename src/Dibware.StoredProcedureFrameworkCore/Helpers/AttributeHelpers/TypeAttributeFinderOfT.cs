using System;
using System.Linq;
using System.Reflection;
using Dibware.StoredProcedureFrameworkCore.Generics;

namespace Dibware.StoredProcedureFrameworkCore.Helpers.AttributeHelpers
{
    public class TypeAttributeFinder<TAttribute>
        where TAttribute : class
    {
        #region Fields

        private readonly Type _type;
        private TAttribute _attribute;

        #endregion

        #region Construtors

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeAttributeFinder{TAttribute}"/> class.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">type</exception>
        protected TypeAttributeFinder(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            _type = type;
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
            var attributes = _type.GetTypeInfo().GetCustomAttributes().Where(a => a.GetType() == typeof(TAttribute)).ToList();
            var noAttributesFound = !attributes.Any();
            if (noAttributesFound) return;

            _attribute = attributes.First() as TAttribute;
        }

        #endregion
    }
}