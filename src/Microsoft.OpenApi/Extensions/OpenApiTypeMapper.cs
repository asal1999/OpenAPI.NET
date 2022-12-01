﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. 

using System;
using System.Collections.Generic;
using Microsoft.OpenApi.Models;

namespace Microsoft.OpenApi.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Type"/>.
    /// </summary>
    public static class OpenApiTypeMapper
    {
        private static readonly Dictionary<Type, Func<OpenApiSchema>> _simpleTypeToOpenApiSchema = new()
        {
            [typeof(bool)] = () => new OpenApiSchema { Type = "boolean" },
            [typeof(byte)] = () => new OpenApiSchema { Type = "string", Format = "byte" },
            [typeof(int)] = () => new OpenApiSchema { Type = "integer", Format = "int32" },
            [typeof(uint)] = () => new OpenApiSchema { Type = "integer", Format = "int32" },
            [typeof(long)] = () => new OpenApiSchema { Type = "integer", Format = "int64" },
            [typeof(ulong)] = () => new OpenApiSchema { Type = "integer", Format = "int64" },
            [typeof(float)] = () => new OpenApiSchema { Type = "number", Format = "float" },
            [typeof(double)] = () => new OpenApiSchema { Type = "number", Format = "double" },
            [typeof(decimal)] = () => new OpenApiSchema { Type = "number", Format = "double" },
            [typeof(DateTime)] = () => new OpenApiSchema { Type = "string", Format = "date-time" },
            [typeof(DateTimeOffset)] = () => new OpenApiSchema { Type = "string", Format = "date-time" },
            [typeof(Guid)] = () => new OpenApiSchema { Type = "string", Format = "uuid" },
            [typeof(char)] = () => new OpenApiSchema { Type = "string" },
            
            // Nullable types
            [typeof(bool?)] = () => new OpenApiSchema { Type = "boolean", Nullable = true },
            [typeof(byte?)] = () => new OpenApiSchema { Type = "string", Format = "byte", Nullable = true },
            [typeof(int?)] = () => new OpenApiSchema { Type = "integer", Format = "int32", Nullable = true },
            [typeof(uint?)] = () => new OpenApiSchema { Type = "integer", Format = "int32", Nullable = true },
            [typeof(long?)] = () => new OpenApiSchema { Type = "integer", Format = "int64", Nullable = true },
            [typeof(ulong?)] = () => new OpenApiSchema { Type = "integer", Format = "int64", Nullable = true },
            [typeof(float?)] = () => new OpenApiSchema { Type = "number", Format = "float", Nullable = true },
            [typeof(double?)] = () => new OpenApiSchema { Type = "number", Format = "double", Nullable = true },
            [typeof(decimal?)] = () => new OpenApiSchema { Type = "number", Format = "double", Nullable = true },
            [typeof(DateTime?)] = () => new OpenApiSchema { Type = "string", Format = "date-time", Nullable = true },
            [typeof(DateTimeOffset?)] = () => new OpenApiSchema { Type = "string", Format = "date-time", Nullable = true },
            [typeof(Guid?)] = () => new OpenApiSchema { Type = "string", Format = "uuid", Nullable = true },
            [typeof(char?)] = () => new OpenApiSchema { Type = "string", Nullable = true },
            
            [typeof(Uri)] = () => new OpenApiSchema { Type = "string" }, // Uri is treated as simple string
            [typeof(string)] = () => new OpenApiSchema { Type = "string" },
            [typeof(object)] = () => new OpenApiSchema { Type = "object" }
        };

        /// <summary>
        /// Maps a simple type to an OpenAPI data type and format.
        /// </summary>
        /// <param name="type">Simple type.</param>
        /// <remarks>
        /// All the following types from http://swagger.io/specification/#data-types-12 are supported.
        /// Other types including nullables and URL are also supported.
        /// Common Name      type    format      Comments
        /// ===========      ======= ======      =========================================
        /// integer          integer int32       signed 32 bits
        /// long             integer int64       signed 64 bits
        /// float            number  float
        /// double           number  double
        /// string           string  [empty]
        /// byte             string  byte        base64 encoded characters
        /// binary           string  binary      any sequence of octets
        /// boolean          boolean [empty]
        /// date             string  date        As defined by full-date - RFC3339
        /// dateTime         string  date-time   As defined by date-time - RFC3339
        /// password         string  password    Used to hint UIs the input needs to be obscured.
        /// If the type is not recognized as "simple", System.String will be returned.
        /// </remarks>
        public static OpenApiSchema MapTypeToOpenApiPrimitiveType(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return _simpleTypeToOpenApiSchema.TryGetValue(type, out var result)
                ? result()
                : new OpenApiSchema { Type = "string" };
        }
    }
}
