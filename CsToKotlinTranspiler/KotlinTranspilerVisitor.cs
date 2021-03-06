﻿// -----------------------------------------------------------------------
//   <copyright file="KotlinTranspilerVisitor.cs" company="Asynkron HB">
//       Copyright (C) 2015-2017 Asynkron HB All rights reserved
//   </copyright>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ConsoleApplication3
{
    public partial class KotlinTranspilerVisitor : CSharpSyntaxWalker
    {
        private readonly SemanticModel _model;
        private int _indent;

        public KotlinTranspilerVisitor(SemanticModel model, SyntaxWalkerDepth depth = SyntaxWalkerDepth.Node) : base(depth)
        {
            _model = model;
        }

        public override void VisitConversionOperatorDeclaration(ConversionOperatorDeclarationSyntax node)
        {
            base.VisitConversionOperatorDeclaration(node);
        }

        public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            var arg = GetArgList(node.ParameterList);
            WriteLine($"constructor({arg}) {{");
            _indent++;
            Visit(node.Body);
            _indent--;
            WriteLine("}");
        }

        public override void VisitConstructorInitializer(ConstructorInitializerSyntax node)
        {
            base.VisitConstructorInitializer(node);
        }

        public override void VisitDestructorDeclaration(DestructorDeclarationSyntax node)
        {
            base.VisitDestructorDeclaration(node);
        }

        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            base.VisitPropertyDeclaration(node);
        }

        public override void VisitTypeConstraint(TypeConstraintSyntax node)
        {
            base.VisitTypeConstraint(node);
        }

        public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            WriteLine($"var {node.Declaration.Variables.First().Identifier} : {GetKotlinType(node.Declaration.Type)}");
            base.VisitFieldDeclaration(node);
        }

        public override void VisitEventFieldDeclaration(EventFieldDeclarationSyntax node)
        {
            base.VisitEventFieldDeclaration(node);
        }

        public override void VisitExplicitInterfaceSpecifier(ExplicitInterfaceSpecifierSyntax node)
        {
            base.VisitExplicitInterfaceSpecifier(node);
        }

        public override void VisitUsingDirective(UsingDirectiveSyntax node)
        {
            //   base.VisitUsingDirective(node);
        }

        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            WriteLine($"package {GetKotlinPackageName(node.Name.ToString())}");
            //base.VisitNamespaceDeclaration(node);
            foreach (var m in node.Members)
            {
                Visit(m);
                WriteLine();
            }
        }

        public override void VisitAttributeList(AttributeListSyntax node)
        {
            base.VisitAttributeList(node);
        }

        public override void VisitAttributeTargetSpecifier(AttributeTargetSpecifierSyntax node)
        {
            base.VisitAttributeTargetSpecifier(node);
        }

        public override void VisitTypeParameter(TypeParameterSyntax node)
        {
            base.VisitTypeParameter(node);
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            WriteLine();
            WriteLine($"class {node.Identifier} {{");
            _indent++;
            base.VisitClassDeclaration(node);
            _indent--;
            WriteLine("}");
        }

        public override void VisitStructDeclaration(StructDeclarationSyntax node)
        {
            base.VisitStructDeclaration(node);
        }

        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            base.VisitInterfaceDeclaration(node);
        }

        public override void VisitEnumDeclaration(EnumDeclarationSyntax node)
        {
            base.VisitEnumDeclaration(node);
        }

        public override void VisitDelegateDeclaration(DelegateDeclarationSyntax node)
        {
            base.VisitDelegateDeclaration(node);
        }

        public override void VisitEnumMemberDeclaration(EnumMemberDeclarationSyntax node)
        {
            base.VisitEnumMemberDeclaration(node);
        }

        public override void VisitBaseList(BaseListSyntax node)
        {
            base.VisitBaseList(node);
        }

        public override void VisitSimpleBaseType(SimpleBaseTypeSyntax node)
        {
            base.VisitSimpleBaseType(node);
        }

        public override void VisitTypeParameterConstraintClause(TypeParameterConstraintClauseSyntax node)
        {
            base.VisitTypeParameterConstraintClause(node);
        }

        public override void VisitConstructorConstraint(ConstructorConstraintSyntax node)
        {
            base.VisitConstructorConstraint(node);
        }

        public override void VisitClassOrStructConstraint(ClassOrStructConstraintSyntax node)
        {
            base.VisitClassOrStructConstraint(node);
        }

        public override void VisitEqualsValueClause(EqualsValueClauseSyntax node)
        {
            base.VisitEqualsValueClause(node);
        }

        public override void VisitExpressionStatement(ExpressionStatementSyntax node)
        {
            WriteStart("");
            base.VisitExpressionStatement(node);
            WriteLine();
        }

        public override void VisitEmptyStatement(EmptyStatementSyntax node)
        {
            base.VisitEmptyStatement(node);
        }

        public override void VisitLabeledStatement(LabeledStatementSyntax node)
        {
            base.VisitLabeledStatement(node);
        }

        public override void VisitGotoStatement(GotoStatementSyntax node)
        {
            base.VisitGotoStatement(node);
        }

        public override void VisitBreakStatement(BreakStatementSyntax node)
        {
            base.VisitBreakStatement(node);
        }

        public override void VisitContinueStatement(ContinueStatementSyntax node)
        {
            base.VisitContinueStatement(node);
        }

        public override void VisitReturnStatement(ReturnStatementSyntax node)
        {
            WriteStart("return ");
            base.VisitReturnStatement(node);
            WriteLine();
        }

        public override void VisitThrowStatement(ThrowStatementSyntax node)
        {
            base.VisitThrowStatement(node);
        }

        public override void VisitYieldStatement(YieldStatementSyntax node)
        {
            base.VisitYieldStatement(node);
        }

        public override void VisitWhileStatement(WhileStatementSyntax node)
        {
            base.VisitWhileStatement(node);
        }

        public override void VisitDoStatement(DoStatementSyntax node)
        {
            base.VisitDoStatement(node);
        }

        public override void VisitForStatement(ForStatementSyntax node)
        {
            base.VisitForStatement(node);
        }

        public override void VisitForEachStatement(ForEachStatementSyntax node)
        {
            WriteStart("for(");
            Write(node.Identifier.ToString());
            Write(" in ");
            Visit(node.Expression);
            Write(")");
            VisitMaybeBlock(node.Statement);
        }

        public override void VisitUsingStatement(UsingStatementSyntax node)
        {
            base.VisitUsingStatement(node);
        }

        public override void VisitFixedStatement(FixedStatementSyntax node)
        {
            base.VisitFixedStatement(node);
        }

        public override void VisitCheckedStatement(CheckedStatementSyntax node)
        {
            base.VisitCheckedStatement(node);
        }

        public override void VisitUnsafeStatement(UnsafeStatementSyntax node)
        {
            base.VisitUnsafeStatement(node);
        }

        public override void VisitLockStatement(LockStatementSyntax node)
        {
            base.VisitLockStatement(node);
        }

        public override void VisitIfStatement(IfStatementSyntax node)
        {
            WriteStart("if (");
            Visit(node.Condition);
            Write(")");
            VisitMaybeBlock(node.Statement);
        }

        private void VisitMaybeBlock(StatementSyntax node)
        {
            if (node is BlockSyntax)
            {
                Visit(node);
            }
            else
            {
                WriteLine();
                _indent++;
                Visit(node);
                _indent--;
            }
        }

        public override void VisitElseClause(ElseClauseSyntax node)
        {
            base.VisitElseClause(node);
        }

        public override void VisitSwitchStatement(SwitchStatementSyntax node)
        {
            base.VisitSwitchStatement(node);
        }

        public override void VisitSwitchSection(SwitchSectionSyntax node)
        {
            base.VisitSwitchSection(node);
        }

        public override void VisitCaseSwitchLabel(CaseSwitchLabelSyntax node)
        {
            base.VisitCaseSwitchLabel(node);
        }

        public override void VisitDefaultSwitchLabel(DefaultSwitchLabelSyntax node)
        {
            base.VisitDefaultSwitchLabel(node);
        }

        public override void VisitTryStatement(TryStatementSyntax node)
        {
            base.VisitTryStatement(node);
        }

        public override void VisitCatchClause(CatchClauseSyntax node)
        {
            base.VisitCatchClause(node);
        }

        public override void VisitCatchDeclaration(CatchDeclarationSyntax node)
        {
            base.VisitCatchDeclaration(node);
        }

        public override void VisitCatchFilterClause(CatchFilterClauseSyntax node)
        {
            base.VisitCatchFilterClause(node);
        }

        public override void VisitFinallyClause(FinallyClauseSyntax node)
        {
            base.VisitFinallyClause(node);
        }

        public override void VisitCompilationUnit(CompilationUnitSyntax node)
        {
            base.VisitCompilationUnit(node);
        }

        public override void VisitExternAliasDirective(ExternAliasDirectiveSyntax node)
        {
            base.VisitExternAliasDirective(node);
        }

        public override void VisitSizeOfExpression(SizeOfExpressionSyntax node)
        {
            base.VisitSizeOfExpression(node);
        }

        public override void VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            base.VisitInvocationExpression(node);
        }

        public override void VisitElementAccessExpression(ElementAccessExpressionSyntax node)
        {
            base.VisitElementAccessExpression(node);
        }

        //public override void VisitIdentifierName(IdentifierNameSyntax node)
        //{
        //    if (node.Identifier.ToString() == "ToLowerInvariant")
        //    {

        //    }
        //    base.VisitIdentifierName(node);
        //}

        public override void VisitPostfixUnaryExpression(PostfixUnaryExpressionSyntax node)
        {
            base.VisitPostfixUnaryExpression(node);
        }

        public override void VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            var methodName = node.Name.ToString();
            var sym = _model.GetSymbolInfo(node).Symbol;
            var containingTypeName = sym?.ContainingType?.Name;

            switch (containingTypeName)
            {
                case nameof(Enumerable):
                    switch (methodName)
                    {
                        case nameof(Enumerable.Select):
                            Visit(node.Expression);
                            Write(".");
                            Write("map");
                            break;
                        case nameof(Enumerable.Where):
                            Visit(node.Expression);
                            Write(".");
                            Write("filter");
                            break;
                        case nameof(Enumerable.ToList):
                            Visit(node.Expression);
                            Write(".");
                            Write("toList");
                            break;
                        default:
                            break;
                    }
                    break;
                case nameof(Console):
                    switch (methodName)
                    {
                        case nameof(Console.WriteLine):
                            Write("println");
                            break;
                        case nameof(Console.Write):
                            Write("print");
                            break;
                        case nameof(Console.ReadLine):
                            Write("readLine");
                            break;
                    }
                    break;
                default:
                    Visit(node.Expression);
                    Write(".");
                    var name = node.Name.ToString();
                    name = ToCamelCase(name);
                    Write(name);
                    break;
            }

            //  base.VisitMemberAccessExpression(node);
        }

        public override void VisitConditionalAccessExpression(ConditionalAccessExpressionSyntax node)
        {
            base.VisitConditionalAccessExpression(node);
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var arg = GetArgList(node.ParameterList);
            var methodName = ToCamelCase(node.Identifier.Text);
            WriteStart($"fun {methodName} ({arg}) : {GetKotlinType(node.ReturnType)}");

            base.VisitMethodDeclaration(node);
        }

        public override void VisitOperatorDeclaration(OperatorDeclarationSyntax node)
        {
            base.VisitOperatorDeclaration(node);
        }

        public override void VisitToken(SyntaxToken token)
        {
            base.VisitToken(token);
        }

        public override void VisitLeadingTrivia(SyntaxToken token)
        {
            base.VisitLeadingTrivia(token);
        }

        public override void VisitTrailingTrivia(SyntaxToken token)
        {
            base.VisitTrailingTrivia(token);
        }

        public override void VisitTrivia(SyntaxTrivia trivia)
        {
            base.VisitTrivia(trivia);
        }

        public override void VisitIdentifierName(IdentifierNameSyntax node)
        {
            var si = _model.GetSymbolInfo(node);
            var sym = si.Symbol;
            if (sym.Kind == SymbolKind.Method)
            {
                var name = ToCamelCase(node.Identifier.Text);
                Write(name);
                return;
            } 
            Write(node.Identifier.Text);
        }

        public override void VisitQualifiedName(QualifiedNameSyntax node)
        {
            base.VisitQualifiedName(node);
        }

        public override void VisitGenericName(GenericNameSyntax node)
        {
            base.VisitGenericName(node);
        }

        public override void VisitTypeArgumentList(TypeArgumentListSyntax node)
        {
            base.VisitTypeArgumentList(node);
        }

        public override void VisitMemberBindingExpression(MemberBindingExpressionSyntax node)
        {
            base.VisitMemberBindingExpression(node);
        }

        public override void VisitElementBindingExpression(ElementBindingExpressionSyntax node)
        {
            base.VisitElementBindingExpression(node);
        }

        public override void VisitImplicitElementAccess(ImplicitElementAccessSyntax node)
        {
            base.VisitImplicitElementAccess(node);
        }

        public override void VisitBinaryExpression(BinaryExpressionSyntax node)
        {
            Visit(node.Left);
            Write(" ");
            Write(node.OperatorToken.Text);
            Write(" ");
            Visit(node.Right);
        }

        public override void VisitAccessorDeclaration(AccessorDeclarationSyntax node)
        {
            base.VisitAccessorDeclaration(node);
        }

        public override void VisitParameterList(ParameterListSyntax node)
        {
            base.VisitParameterList(node);
        }

        public override void VisitBracketedParameterList(BracketedParameterListSyntax node)
        {
            base.VisitBracketedParameterList(node);
        }

        public override void VisitParameter(ParameterSyntax node)
        {
            base.VisitParameter(node);
        }

        public override void VisitIncompleteMember(IncompleteMemberSyntax node)
        {
            base.VisitIncompleteMember(node);
        }

        public override void VisitSkippedTokensTrivia(SkippedTokensTriviaSyntax node)
        {
            base.VisitSkippedTokensTrivia(node);
        }

        public override void VisitDocumentationCommentTrivia(DocumentationCommentTriviaSyntax node)
        {
            base.VisitDocumentationCommentTrivia(node);
        }

        public override void VisitTypeCref(TypeCrefSyntax node)
        {
            base.VisitTypeCref(node);
        }

        public override void VisitQualifiedCref(QualifiedCrefSyntax node)
        {
            base.VisitQualifiedCref(node);
        }

        public override void VisitNameMemberCref(NameMemberCrefSyntax node)
        {
            base.VisitNameMemberCref(node);
        }

        public override void VisitIndexerMemberCref(IndexerMemberCrefSyntax node)
        {
            base.VisitIndexerMemberCref(node);
        }

        public override void VisitOperatorMemberCref(OperatorMemberCrefSyntax node)
        {
            base.VisitOperatorMemberCref(node);
        }

        public override void VisitConversionOperatorMemberCref(ConversionOperatorMemberCrefSyntax node)
        {
            base.VisitConversionOperatorMemberCref(node);
        }

        public override void VisitCrefParameterList(CrefParameterListSyntax node)
        {
            base.VisitCrefParameterList(node);
        }

        public override void VisitCrefBracketedParameterList(CrefBracketedParameterListSyntax node)
        {
            base.VisitCrefBracketedParameterList(node);
        }

        public override void VisitCrefParameter(CrefParameterSyntax node)
        {
            base.VisitCrefParameter(node);
        }

        public override void VisitXmlElement(XmlElementSyntax node)
        {
            base.VisitXmlElement(node);
        }

        public override void VisitXmlElementStartTag(XmlElementStartTagSyntax node)
        {
            base.VisitXmlElementStartTag(node);
        }

        public override void VisitXmlElementEndTag(XmlElementEndTagSyntax node)
        {
            base.VisitXmlElementEndTag(node);
        }

        public override void VisitXmlEmptyElement(XmlEmptyElementSyntax node)
        {
            base.VisitXmlEmptyElement(node);
        }

        public override void VisitXmlName(XmlNameSyntax node)
        {
            base.VisitXmlName(node);
        }

        public override void VisitXmlPrefix(XmlPrefixSyntax node)
        {
            base.VisitXmlPrefix(node);
        }

        public override void VisitXmlTextAttribute(XmlTextAttributeSyntax node)
        {
            base.VisitXmlTextAttribute(node);
        }

        public override void VisitXmlCrefAttribute(XmlCrefAttributeSyntax node)
        {
            base.VisitXmlCrefAttribute(node);
        }

        public override void VisitXmlNameAttribute(XmlNameAttributeSyntax node)
        {
            base.VisitXmlNameAttribute(node);
        }

        public override void VisitXmlText(XmlTextSyntax node)
        {
            base.VisitXmlText(node);
        }

        public override void VisitXmlCDataSection(XmlCDataSectionSyntax node)
        {
            base.VisitXmlCDataSection(node);
        }

        public override void VisitXmlProcessingInstruction(XmlProcessingInstructionSyntax node)
        {
            base.VisitXmlProcessingInstruction(node);
        }

        public override void VisitXmlComment(XmlCommentSyntax node)
        {
            base.VisitXmlComment(node);
        }

        public override void VisitIfDirectiveTrivia(IfDirectiveTriviaSyntax node)
        {
            base.VisitIfDirectiveTrivia(node);
        }

        public override void VisitElifDirectiveTrivia(ElifDirectiveTriviaSyntax node)
        {
            base.VisitElifDirectiveTrivia(node);
        }

        public override void VisitElseDirectiveTrivia(ElseDirectiveTriviaSyntax node)
        {
            base.VisitElseDirectiveTrivia(node);
        }

        public override void VisitEndIfDirectiveTrivia(EndIfDirectiveTriviaSyntax node)
        {
            base.VisitEndIfDirectiveTrivia(node);
        }

        public override void VisitRegionDirectiveTrivia(RegionDirectiveTriviaSyntax node)
        {
            base.VisitRegionDirectiveTrivia(node);
        }

        public override void VisitEndRegionDirectiveTrivia(EndRegionDirectiveTriviaSyntax node)
        {
            base.VisitEndRegionDirectiveTrivia(node);
        }

        public override void VisitErrorDirectiveTrivia(ErrorDirectiveTriviaSyntax node)
        {
            base.VisitErrorDirectiveTrivia(node);
        }

        public override void VisitWarningDirectiveTrivia(WarningDirectiveTriviaSyntax node)
        {
            base.VisitWarningDirectiveTrivia(node);
        }

        public override void VisitBadDirectiveTrivia(BadDirectiveTriviaSyntax node)
        {
            base.VisitBadDirectiveTrivia(node);
        }

        public override void VisitDefineDirectiveTrivia(DefineDirectiveTriviaSyntax node)
        {
            base.VisitDefineDirectiveTrivia(node);
        }

        public override void VisitUndefDirectiveTrivia(UndefDirectiveTriviaSyntax node)
        {
            base.VisitUndefDirectiveTrivia(node);
        }

        public override void VisitLineDirectiveTrivia(LineDirectiveTriviaSyntax node)
        {
            base.VisitLineDirectiveTrivia(node);
        }

        public override void VisitPragmaWarningDirectiveTrivia(PragmaWarningDirectiveTriviaSyntax node)
        {
            base.VisitPragmaWarningDirectiveTrivia(node);
        }

        public override void VisitPragmaChecksumDirectiveTrivia(PragmaChecksumDirectiveTriviaSyntax node)
        {
            base.VisitPragmaChecksumDirectiveTrivia(node);
        }

        public override void VisitReferenceDirectiveTrivia(ReferenceDirectiveTriviaSyntax node)
        {
            base.VisitReferenceDirectiveTrivia(node);
        }

        public override void VisitIndexerDeclaration(IndexerDeclarationSyntax node)
        {
            base.VisitIndexerDeclaration(node);
        }

        public override void VisitAccessorList(AccessorListSyntax node)
        {
            base.VisitAccessorList(node);
        }

        public override void VisitAliasQualifiedName(AliasQualifiedNameSyntax node)
        {
            base.VisitAliasQualifiedName(node);
        }

        public override void VisitPredefinedType(PredefinedTypeSyntax node)
        {
            base.VisitPredefinedType(node);
        }

        public override void VisitCastExpression(CastExpressionSyntax node)
        {
            base.VisitCastExpression(node);
        }

        public override void VisitAnonymousMethodExpression(AnonymousMethodExpressionSyntax node)
        {
            base.VisitAnonymousMethodExpression(node);
        }

        public override void VisitSimpleLambdaExpression(SimpleLambdaExpressionSyntax node)
        {
            if (node.Body is BinaryExpressionSyntax bin && bin.Left is IdentifierNameSyntax name)
            {
                Write("{");
                Write("it " + bin.OperatorToken + " ");
                Visit(bin.Right);
                Write("}");
            }
            else if (node.Body is BlockSyntax block)
            {
                Write("{");
                Write(node.Parameter.Identifier.ToString());
                Write(" -> ");
                WriteLine();
                _indent++;
                foreach (var s in block.Statements)
                {
                    Visit(s);
                }
                _indent--;
                WriteLine("}");
            }
            else
            {
                Write("{");
                Write(node.Parameter.Identifier.ToString());
                Write(" -> ");
                Visit(node.Body);
                Write("}");
            }
        }

        public override void VisitParenthesizedLambdaExpression(ParenthesizedLambdaExpressionSyntax node)
        {
            Write("{");
            var arg = GetArgList(node.ParameterList);
            Write(arg);
            Write(" -> ");
            if (node.Body is BlockSyntax block)
            {
                WriteLine();
                _indent++;
                foreach (var s in block.Statements)
                {
                    Visit(s);
                }
                _indent--;
                WriteLine("}");
            }
            else
            {
                Visit(node.Body);
                Write("}");
            }
        }

        public override void VisitInitializerExpression(InitializerExpressionSyntax node)
        {
            var first = true;
            Write("arrayOf(");
            foreach (var e in node.Expressions)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    Write(", ");
                }
                Visit(e);
            }
            Write(")");
        }

        public override void VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            base.VisitObjectCreationExpression(node);
        }

        public override void VisitAnonymousObjectCreationExpression(AnonymousObjectCreationExpressionSyntax node)
        {
            base.VisitAnonymousObjectCreationExpression(node);
        }

        public override void VisitAnonymousObjectMemberDeclarator(AnonymousObjectMemberDeclaratorSyntax node)
        {
            base.VisitAnonymousObjectMemberDeclarator(node);
        }

        public override void VisitBracketedArgumentList(BracketedArgumentListSyntax node)
        {
            base.VisitBracketedArgumentList(node);
        }

        public override void VisitArgument(ArgumentSyntax node)
        {
            base.VisitArgument(node);
        }

        public override void VisitNameColon(NameColonSyntax node)
        {
            base.VisitNameColon(node);
        }

        public override void VisitArgumentList(ArgumentListSyntax node)
        {
            //this is a method call where there is a single arg which is a lambda.
            //thus we can remove the parens around it
            if (node.Arguments.Count == 1)
            {
                var arg = node.Arguments.First();
                var t = _model.GetSymbolInfo(arg.Expression);
                var sym = t.Symbol;
                if (sym != null && sym.ToString() == "lambda expression") //TODO: I have no idea how to check this correctly
                {
                    Visit(arg);
                    return;
                }

            }

            Write("(");
            var first = true;
            foreach (var a in node.Arguments)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    Write(", ");
                }
                Visit(a);
            }
            Write(")");
        }

        public override void VisitArrayCreationExpression(ArrayCreationExpressionSyntax node)
        {
            base.VisitArrayCreationExpression(node);
        }

        public override void VisitImplicitArrayCreationExpression(ImplicitArrayCreationExpressionSyntax node)
        {
            base.VisitImplicitArrayCreationExpression(node);
        }

        public override void VisitStackAllocArrayCreationExpression(StackAllocArrayCreationExpressionSyntax node)
        {
            base.VisitStackAllocArrayCreationExpression(node);
        }

        public override void VisitQueryExpression(QueryExpressionSyntax node)
        {
            base.VisitQueryExpression(node);
        }

        public override void VisitQueryBody(QueryBodySyntax node)
        {
            base.VisitQueryBody(node);
        }

        public override void VisitFromClause(FromClauseSyntax node)
        {
            base.VisitFromClause(node);
        }

        public override void VisitLetClause(LetClauseSyntax node)
        {
            base.VisitLetClause(node);
        }

        public override void VisitJoinClause(JoinClauseSyntax node)
        {
            base.VisitJoinClause(node);
        }

        public override void VisitJoinIntoClause(JoinIntoClauseSyntax node)
        {
            base.VisitJoinIntoClause(node);
        }

        public override void VisitWhereClause(WhereClauseSyntax node)
        {
            base.VisitWhereClause(node);
        }

        public override void VisitOrderByClause(OrderByClauseSyntax node)
        {
            base.VisitOrderByClause(node);
        }

        public override void VisitOrdering(OrderingSyntax node)
        {
            base.VisitOrdering(node);
        }

        public override void VisitSelectClause(SelectClauseSyntax node)
        {
            base.VisitSelectClause(node);
        }

        public override void VisitGroupClause(GroupClauseSyntax node)
        {
            base.VisitGroupClause(node);
        }

        public override void VisitQueryContinuation(QueryContinuationSyntax node)
        {
            base.VisitQueryContinuation(node);
        }

        public override void VisitOmittedArraySizeExpression(OmittedArraySizeExpressionSyntax node)
        {
            base.VisitOmittedArraySizeExpression(node);
        }

        public override void VisitInterpolatedStringExpression(InterpolatedStringExpressionSyntax node)
        {
            base.VisitInterpolatedStringExpression(node);
        }

        public override void VisitInterpolatedStringText(InterpolatedStringTextSyntax node)
        {
            base.VisitInterpolatedStringText(node);
        }

        public override void VisitInterpolation(InterpolationSyntax node)
        {
            base.VisitInterpolation(node);
        }

        public override void VisitInterpolationAlignmentClause(InterpolationAlignmentClauseSyntax node)
        {
            base.VisitInterpolationAlignmentClause(node);
        }

        public override void VisitInterpolationFormatClause(InterpolationFormatClauseSyntax node)
        {
            base.VisitInterpolationFormatClause(node);
        }

        public override void VisitGlobalStatement(GlobalStatementSyntax node)
        {
            base.VisitGlobalStatement(node);
        }

        public override void VisitBlock(BlockSyntax node)
        {
            Write(" {");
            WriteLine();
            _indent++;
            base.VisitBlock(node);
            _indent--;
            WriteLine("}");
        }

        public override void VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
            base.VisitLocalDeclarationStatement(node);
        }

        public override void VisitVariableDeclaration(VariableDeclarationSyntax node)
        {
            foreach (var v in node.Variables)
            {
                WriteStart($"var {v.Identifier} : {GetKotlinType(node.Type)} = ");
                Visit(v.Initializer);
                WriteLine();
            }
        }

        public override void VisitVariableDeclarator(VariableDeclaratorSyntax node)
        {
            base.VisitVariableDeclarator(node);
        }

        public override void VisitArrayRankSpecifier(ArrayRankSpecifierSyntax node)
        {
            base.VisitArrayRankSpecifier(node);
        }

        public override void VisitPointerType(PointerTypeSyntax node)
        {
            base.VisitPointerType(node);
        }

        public override void VisitNullableType(NullableTypeSyntax node)
        {
            base.VisitNullableType(node);
        }

        public override void VisitOmittedTypeArgument(OmittedTypeArgumentSyntax node)
        {
            base.VisitOmittedTypeArgument(node);
        }

        public override void VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
        {
            base.VisitParenthesizedExpression(node);
        }

        public override void VisitPrefixUnaryExpression(PrefixUnaryExpressionSyntax node)
        {
            base.VisitPrefixUnaryExpression(node);
        }

        public override void VisitAwaitExpression(AwaitExpressionSyntax node)
        {
            base.VisitAwaitExpression(node);
        }

        public override void VisitArrayType(ArrayTypeSyntax node)
        {
            base.VisitArrayType(node);
        }

        public override void VisitArrowExpressionClause(ArrowExpressionClauseSyntax node)
        {
            base.VisitArrowExpressionClause(node);
        }

        public override void VisitEventDeclaration(EventDeclarationSyntax node)
        {
            base.VisitEventDeclaration(node);
        }

        public override void VisitAssignmentExpression(AssignmentExpressionSyntax node)
        {
            Visit(node.Left);
            Write($" {node.OperatorToken} ");
            Visit(node.Right);
        }

        public override void VisitConditionalExpression(ConditionalExpressionSyntax node)
        {
            Write("if (");
            Visit(node.Condition);
            Write(") ");
            Visit(node.WhenTrue);
            Write(" else ");
            Visit(node.WhenFalse);
        }

        public override void VisitThisExpression(ThisExpressionSyntax node)
        {
            base.VisitThisExpression(node);
        }

        public override void VisitBaseExpression(BaseExpressionSyntax node)
        {
            Write("super");
            base.VisitBaseExpression(node);
        }

        public override void VisitLiteralExpression(LiteralExpressionSyntax node)
        {
            Write(node.ToString());
            // base.VisitLiteralExpression(node);
        }

        public override void VisitMakeRefExpression(MakeRefExpressionSyntax node)
        {
            base.VisitMakeRefExpression(node);
        }

        public override void VisitRefTypeExpression(RefTypeExpressionSyntax node)
        {
            base.VisitRefTypeExpression(node);
        }

        public override void VisitRefValueExpression(RefValueExpressionSyntax node)
        {
            base.VisitRefValueExpression(node);
        }

        public override void VisitCheckedExpression(CheckedExpressionSyntax node)
        {
            base.VisitCheckedExpression(node);
        }

        public override void VisitDefaultExpression(DefaultExpressionSyntax node)
        {
            base.VisitDefaultExpression(node);
        }

        public override void VisitTypeOfExpression(TypeOfExpressionSyntax node)
        {
            base.VisitTypeOfExpression(node);
        }

        public override void VisitAttribute(AttributeSyntax node)
        {
            base.VisitAttribute(node);
        }

        public override void VisitAttributeArgument(AttributeArgumentSyntax node)
        {
            base.VisitAttributeArgument(node);
        }

        public override void VisitNameEquals(NameEqualsSyntax node)
        {
            base.VisitNameEquals(node);
        }

        public override void VisitTypeParameterList(TypeParameterListSyntax node)
        {
            base.VisitTypeParameterList(node);
        }

        public override void VisitAttributeArgumentList(AttributeArgumentListSyntax node)
        {
            base.VisitAttributeArgumentList(node);
        }
    }
}