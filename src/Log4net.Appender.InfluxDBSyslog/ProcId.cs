﻿using log4net.Core;
using log4net.Layout;
using log4net.Util.TypeConverters;
using System;

namespace Log4net.Appender.InfluxDBSyslog
{
    /// <summary>
    /// Parameter type used by the <see cref="ProcId"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class provides the basic database parameter properties
    /// as defined by the <see cref="System.Data.IDbDataParameter"/> interface.
    /// </para>
    /// <para>This type can be subclassed to provide database specific
    /// functionality. The two methods that are called externally are
    /// <see cref="Prepare"/> and <see cref="FormatValue"/>.
    /// </para>
    /// </remarks>
    public class ProcId
    {
        #region Public Instance Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcId" /> class.
        /// </summary>
        /// <remarks>
        /// Default constructor for the ProcId class.
        /// </remarks>
        public ProcId()
        {

        }

        public ProcId(string value)
        {
            Value = value;
        }

        #endregion Public Instance Constructors

        #region Public Instance Properties

        /// <summary>
        /// Gets or sets the name of this parameter.
        /// </summary>
        /// <value>
        /// The name of this parameter.
        /// </value>
        /// <remarks>
        /// <para>
        /// The name of this parameter. The parameter name
        /// must match up to a named parameter to the SQL stored procedure
        /// or prepared statement.
        /// </para>
        /// </remarks>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IRawLayout"/> to use to
        /// render the logging event into an object for this
        /// parameter.
        /// </summary>
        /// <value>
        /// The <see cref="IRawLayout"/> used to render the
        /// logging event into an object for this parameter.
        /// </value>
        /// <remarks>
        /// <para>
        /// The <see cref="IRawLayout"/> that renders the value for this
        /// parameter.
        /// </para>
        /// <para>
        /// The <see cref="RawLayoutConverter"/> can be used to adapt
        /// any <see cref="ILayout"/> into a <see cref="IRawLayout"/>
        /// for use in the property.
        /// </para>
        /// </remarks>
        public IRawLayout Layout { get; set; }

        #endregion Public Instance Properties

        #region Public Instance Methods

        /// <summary>
        /// Renders the logging event and set the parameter value in the command.
        /// </summary>
        /// <param name="loggingEvent">The event to be rendered.</param>
        /// <remarks>
        /// <para>
        /// Renders the logging event using this parameters layout
        /// object. Sets the value of the parameter on the command object.
        /// </para>
        /// </remarks>
        virtual public void FormatValue(LoggingEvent loggingEvent)
        {
            if (Layout is IRawLayout)
            {
                // Format the value
                Value = Layout.Format(loggingEvent) as string;
            }
        }

        public override string ToString()
        {
            return Value;
        }

        #endregion Public Instance Methods
        #region Private Instance Fields

        #endregion Private Instance Fields
    }

    public class ConvertStringToProcId : IConvertFrom
    {
        bool IConvertFrom.CanConvertFrom(Type sourceType)
        {
            return sourceType == typeof(string);
        }

        object IConvertFrom.ConvertFrom(object source)
        {
            string str = source as string;
            if (str != null)
            {
                return new ProcId(str);
            }
            throw ConversionNotSupportedException.Create(typeof(ProcId), source);
        }
    }
}