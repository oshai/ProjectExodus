﻿// -----------------------------------------------------------------------
//   <copyright file="VisitorHelpers.cs" company="Asynkron HB">
//       Copyright (C) 2015-2017 Asynkron HB All rights reserved
//   </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ConsoleApplication3
{
    public partial class KotlinTranspilerVisitor
    {
        private string GetKotlinType(TypeSyntax type)
        {
            var si = _model.GetSymbolInfo(type);
            var s = si.Symbol;
            return GetKotlinType(s as ITypeSymbol);
        }

        private string GetKotlinType(ITypeSymbol s)
        {
            var named = s as INamedTypeSymbol;
            if (named != null)
            {
                if (s.TypeKind == TypeKind.Delegate)
                {
                    var args = named.DelegateInvokeMethod.Parameters.Select(p => p.Type).Select(GetKotlinType);
                    var ret = GetKotlinType(named.DelegateInvokeMethod.ReturnType);
                    return $"({string.Join(", ", args)}) -> {ret}";
                }

                if (named?.IsGenericType == true)
                {
                    var args = named.TypeArguments.Select(GetKotlinType);
                    return $"{named.Name}<{string.Join(", ", args)}>";
                }
            }

            if (s.Kind == SymbolKind.ArrayType)
            {
                var arr = (IArrayTypeSymbol) s;
                return $"Array<{GetKotlinType(arr.ElementType)}>";
            }
            var str = s.Name;
            switch (str)
            {
                case "Void":
                    return "Unit";
                case "Int32":
                    return "Int";
                case "String":
                    return "String";
            }

            return str;
        }

        public string GetKotlinPackageName(string ns)
        {
            return ns.ToLowerInvariant();
        }

        private string GetArgList(ParameterListSyntax node)
        {
            List<string> GetArgumentList(ParameterListSyntax parameterList)
            {
                return parameterList.Parameters.Select(p =>
                {
                    if (p.Type == null)
                    {
                        return p.Identifier.ToString();
                    }

                    return p.Identifier + " : " + GetKotlinType(p.Type);
                }).ToList();
            }

            var arg = string.Join(", ", GetArgumentList(node));
            return arg;
        }

        private void WriteStart(string text)
        {
            Console.Write(GetIndent() + text);
        }

        private void Write(string text)
        {
            Console.Write(text);
        }

        private void WriteLine()
        {
            Console.WriteLine();
        }

        private void WriteLine(string text)
        {
            Console.WriteLine(GetIndent() + text);
        }

        private string GetIndent()
        {
            return new string(' ', _indent * 4);
        }

        private static string ToCamelCase(string name)
        {
            return char.ToLowerInvariant(name[0]) + name.Substring(1);
        }
    }
}