using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReflectionIT.DisposeGenerator.Attributes;

namespace ReflectionIT.DisposeGenerator;

internal class DisposeInfo : IEquatable<DisposeInfo?> {

    public string MemberName { get; }
    public ITypeSymbol ContainingType { get; }

    public bool SetToNull { get; }

    public int Order { get; }

    public DisposeInfo(ISymbol symbol, string typeName) {

        MemberName = symbol.Name;

        ContainingType = symbol.ContainingType;

        var attribute = symbol.GetAttributes()
             .First(a => a.AttributeClass?.ToDisplayString() == typeName);

        SetToNull = attribute.NamedArguments.FirstOrDefault(n => n.Key == nameof(DisposeAttribute.SetToNull)).Value.ToCSharpString() == "true";
        Order = GetDisposeOrder(symbol, attribute);
    }

    public override bool Equals(object? obj) => Equals(obj as DisposeInfo);

    public bool Equals(DisposeInfo? other) => other is not null && MemberName == other.MemberName;

    public override int GetHashCode() => 30165064 + EqualityComparer<string>.Default.GetHashCode(MemberName);

    private static int GetDisposeOrder(ISymbol symbol, AttributeData disposeAttribute) {
        var disposeOrderAttribute = symbol.GetAttributes()
            .FirstOrDefault(static a => a.AttributeClass?.ToDisplayString() == typeof(DisposeOrderAttribute).FullName);

        return disposeOrderAttribute is not null
            ? (int)(disposeOrderAttribute.ConstructorArguments.FirstOrDefault().Value ?? int.MaxValue)
            : int.MaxValue;
    }
}
