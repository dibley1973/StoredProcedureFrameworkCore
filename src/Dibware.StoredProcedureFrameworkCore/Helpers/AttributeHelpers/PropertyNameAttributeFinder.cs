using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dibware.StoredProcedureFrameworkCore.Generics;
using Dibware.StoredProcedureFrameworkCore.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFrameworkCore.Helpers.AttributeHelpers
{
    public class PropertyNameAttributeFinder
    {
        #region Fields

        private readonly PropertyInfo _property;
        private NameAttribute _attribute;

        #endregion

        #region Construtors

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyNameAttributeFinder"/> class.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <exception cref="System.ArgumentNullException">property</exception>
        public PropertyNameAttributeFinder(PropertyInfo property)
        {
            if (property == null) throw new ArgumentNullException("property");

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
        public bool HasFoundAttribute
        {
            get { return _attribute != null; }
        }

        /// <summary>
        /// Gets the result containing the attribute if one was found.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public Maybe<NameAttribute> GetResult()
        {
            if (!HasFoundAttribute)
            {
                return new Maybe<NameAttribute>();
            }
            return new Maybe<NameAttribute>(_attribute);
        }

        #endregion

        #region Private Members

        private void SetAttributeIfExists()
        {
            //_attribute = _property.PropertyType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(NameAttribute)));
            var properties = _property.PropertyType.GetProperties().Where(prop => prop.IsDefined(typeof(NameAttribute))).ToList();

            if (properties.Any())
            {
                var property = properties.First();
                _attribute = property.GetType().GetTypeInfo().GetCustomAttribute<NameAttribute>();
                //_attribute = attributes.First() as NameAttribute;
                //_attribute = _property.PropertyType.GetTypeInfo().GetCustomAttribute<NameAttribute>();
            }
        }
        #endregion
    }
}