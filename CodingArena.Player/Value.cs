using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CodingArena.Player
{
    public interface IValue
    {
        double Maximum { get; }
        double Actual { get; }
        double Percent { get; }
    }

    [ImmutableObject(true), Serializable]
    public class Value : IValue, IEquatable<Value>, IComparable<Value>, IComparable
    {
        public Value(double maximum, double actual)
        {
            if (maximum <= 0) throw new ArgumentOutOfRangeException(
                nameof(maximum), "Value is less than or equal to zero.");
            if (actual < 0) throw new ArgumentOutOfRangeException(
                nameof(actual), "Value is less than zero.");
            if (actual > maximum) throw new ArgumentOutOfRangeException(
                nameof(actual), $"Value is more than {nameof(maximum)}.");

            Maximum = maximum;
            Actual = actual;
            Percent = actual * 100 / Maximum;
        }

        public double Maximum { get; }
        public double Actual { get; }
        public double Percent { get; }

        public int CompareTo(IValue other)
        {
            if (Actual < other.Actual) return -1;
            if (Actual > other.Actual) return 1;
            return 0;
        }

        public bool Equals(Value other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Maximum.Equals(other.Maximum) && Actual.Equals(other.Actual);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Value)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Maximum.GetHashCode() * 397) ^ Actual.GetHashCode();
            }
        }

        public static bool operator ==(Value left, Value right) => Equals(left, right);
        public static bool operator !=(Value left, Value right) => !Equals(left, right);

        public int CompareTo(Value other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Actual.CompareTo(other.Actual);
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            return obj is Value other
                ? CompareTo(other)
                : throw new ArgumentException($"Object must be of type {nameof(Value)}");
        }

        public static bool operator <(Value left, Value right) =>
            Comparer<Value>.Default.Compare(left, right) < 0;

        public static bool operator >(Value left, Value right) =>
            Comparer<Value>.Default.Compare(left, right) > 0;

        public static bool operator <=(Value left, Value right) =>
            Comparer<Value>.Default.Compare(left, right) <= 0;

        public static bool operator >=(Value left, Value right) =>
            Comparer<Value>.Default.Compare(left, right) >= 0;

        public override string ToString() =>
            $"{nameof(Maximum)}: {Maximum}, {nameof(Actual)}: {Actual}, {nameof(Percent)}: {Percent}";
    }
}