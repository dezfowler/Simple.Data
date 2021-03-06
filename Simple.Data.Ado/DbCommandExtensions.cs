﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Simple.Data.Ado
{
    static class DbCommandExtensions
    {
        public static IEnumerable<IDictionary<string, object>> ToAsyncEnumerable(this IDbCommand command)
        {
            if (command.Connection == null) throw new InvalidOperationException("Command has no connection.");
            return ToObservable(command).ToEnumerable();
        }

        public static IObservable<IDictionary<string, object>> ToObservable(this IDbCommand command)
        {
            return new ObservableDataReader(command);
        }

        public static IDbDataParameter AddParameter(this IDbCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value ?? DBNull.Value;
            command.Parameters.Add(parameter);
            return parameter;
        }
    }
}
