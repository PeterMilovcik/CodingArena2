using CodingArena.Annotations;
using CodingArena.Common;
using CodingArena.Main.Battlefields;
using FluentAssertions;
using NUnit.Framework;
using System.Windows;

namespace CodingArena.Tests
{
    internal class TestableMovable : Movable
    {
        public TestableMovable([NotNull] Battlefield battlefield) : base(battlefield)
        {
        }

        public void SetDirection(Vector newDirection) => Direction = newDirection;

        public void SetAngle(double newAngle) => Angle = newAngle;
    }

    public class MovableTests
    {
        [Test]
        public void Angle2Direction()
        {
            var sut = new TestableMovable(new Battlefield(1, 1));
            sut.SetAngle(0);
            sut.Direction.Should().Be(new Vector(1, 0));
            sut.SetAngle(90);
            sut.Direction.X.Should().BeApproximately(0, 0.0001);
            sut.Direction.Y.Should().BeApproximately(1, 0.0001);
            sut.SetAngle(180);
            sut.Direction.X.Should().BeApproximately(-1, 0.0001);
            sut.Direction.Y.Should().BeApproximately(0, 0.0001);
            sut.SetAngle(270);
            sut.Direction.X.Should().BeApproximately(0, 0.0001);
            sut.Direction.Y.Should().BeApproximately(-1, 0.0001);
        }

        [Test]
        public void Direction2Angle()
        {
            var sut = new TestableMovable(new Battlefield(1, 1));
            sut.SetDirection(new Vector(1, 0));
            sut.Angle.Should().Be(0);
            sut.SetDirection(new Vector(0, 1));
            sut.Angle.Should().Be(90);
            sut.SetDirection(new Vector(-1, 0));
            sut.Angle.Should().Be(180);
            sut.SetDirection(new Vector(0, -1));
            sut.Angle.Should().Be(270);
        }
    }
}