﻿using System.Reflection;

namespace Emporium.Domain.SeedWork;

public abstract class ValueObject : IEquatable<ValueObject>
{
    private List<PropertyInfo>? _properties;
    private List<FieldInfo>? _fields;

    public static bool operator ==(ValueObject obj1, ValueObject obj2)
    {
        return object.Equals(obj1, null) ? object.Equals(obj2, null) : obj1.Equals(obj2);
    }

    public static bool operator !=(ValueObject obj1, ValueObject obj2)
    {
        return !(obj1 == obj2);
    }

    public bool Equals(ValueObject obj)
    {
        return Equals(obj as object);
    }

    public override bool Equals(object obj)
    {
        return obj != null && GetType() == obj.GetType()
&& GetProperties().All(p => PropertiesAreEqual(obj, p))
            && GetFields().All(f => FieldsAreEqual(obj, f));
    }

    private bool PropertiesAreEqual(object obj, PropertyInfo p)
    {
        return object.Equals(p.GetValue(this, null), p.GetValue(obj, null));
    }

    private bool FieldsAreEqual(object obj, FieldInfo f)
    {
        return object.Equals(f.GetValue(this), f.GetValue(obj));
    }

    private IEnumerable<PropertyInfo> GetProperties()
    {
        _properties ??= GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.GetCustomAttribute(typeof(IgnoreMemberAttribute)) == null)
                .ToList();

        return _properties;
    }

    private IEnumerable<FieldInfo> GetFields()
    {
        _fields ??= GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(p => p.GetCustomAttribute(typeof(IgnoreMemberAttribute)) == null)
                .ToList();

        return _fields;
    }

    public override int GetHashCode()
    {
        unchecked   //allow overflow
        {
            var hash = 17;
            foreach (var prop in GetProperties())
            {
                var value = prop.GetValue(this, null);
                hash = HashValue(hash, value);
            }

            foreach (var field in GetFields())
            {
                var value = field.GetValue(this);
                hash = HashValue(hash, value);
            }

            return hash;
        }
    }

    private static int HashValue(int seed, object value)
    {
        var currentHash = value?.GetHashCode() ?? 0;

        return (seed * 23) + currentHash;
    }
}