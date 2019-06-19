using CodeKatas;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace NTest
{
    //Group Only Bools
    [TestFixture]
    class BoolsTest
    {
        [Test]
        public void EmptyScheme()
        {
            var scheme = new string[0];
            var control = new ArgsClass<bool>();
            Assert.Throws<NoSchemeException>(() => control.BoolArgs(scheme, null));
        }
        [Test]
        public void OneScheme()
        {
            var scheme = new string[1];
            scheme[0] = "-p";
            var control = new ArgsClass<bool>();
            var expected = new Dictionary<string, bool>();
            expected.Add(scheme[0], false);

            var actual = control.BoolArgs(scheme, null);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TwoScheme()
        {
            var scheme = new string[2];
            scheme[0] = "-p";
            scheme[1] = "-x";
            var control = new ArgsClass<bool>();
            var expected = new Dictionary<string, bool>();
            expected.Add(scheme[0], false);
            expected.Add(scheme[1], false);

            var actual = control.BoolArgs(scheme, null);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void InvalidScheme()
        {
            var scheme = new string[2];
            scheme[0] = "-px";
            scheme[1] = "-x";
            var control = new ArgsClass<bool>();
            var expected = new Dictionary<string, bool>();
            expected.Add(scheme[0], false);
            expected.Add(scheme[1], false);

            Assert.Throws<InvalidSchemeException>(() => control.BoolArgs(scheme, null));
        }
        [Test]
        public void NoArgs()
        {
            var scheme = new string[2];
            scheme[0] = "-p";
            scheme[1] = "-x";
            string arg = "-p";
            var control = new ArgsClass<bool>();
            var expected = new Dictionary<string, bool>();
            expected.Add(scheme[0], true);
            expected.Add(scheme[1], false);

            Assert.AreEqual(expected, control.BoolArgs(scheme, arg));
        }
        [Test]
        public void SomeArgs()
        {
            var scheme = new string[4];
            scheme[0] = "-p";
            scheme[1] = "-x";
            scheme[2] = "-y";
            scheme[3] = "-z";
            string arg = "-p -y";
            var control = new ArgsClass<bool>();
            var expected = new Dictionary<string, bool>();
            expected.Add(scheme[0], true);
            expected.Add(scheme[1], false);
            expected.Add(scheme[2], true);
            expected.Add(scheme[3], false);
            Assert.AreEqual(expected, control.BoolArgs(scheme, arg));
        }
        [Test]
        public void InvalidArgs()
        {
            var scheme = new string[4];
            scheme[0] = "-p";
            scheme[1] = "-x";
            scheme[2] = "-y";
            scheme[3] = "-z";
            string arg = "-p x";
            var control = new ArgsClass<bool>();
            var expected = new Dictionary<string, bool>();
            expected.Add(scheme[0], true);
            expected.Add(scheme[1], false);
            expected.Add(scheme[2], true);
            expected.Add(scheme[3], false);

            Assert.Throws<InvalidArgException>(() => control.BoolArgs(scheme, arg));
        }
        [Test]
        public void InvalidArgsNotDeclared()
        {
            var scheme = new string[4];
            scheme[0] = "-p";
            scheme[1] = "-x";
            scheme[2] = "-y";
            scheme[3] = "-z";
            string arg = "-p -x -a";
            var control = new ArgsClass<bool>();
            var expected = new Dictionary<string, bool>();
            expected.Add(scheme[0], true);
            expected.Add(scheme[1], false);
            expected.Add(scheme[2], true);
            expected.Add(scheme[3], false);

            Assert.Throws<InvalidArgException>(() => control.BoolArgs(scheme, arg));
        }
    }
    [TestFixture]
    class IntTests{
        [Test]
        public void SomeScheme()
        {
            var scheme = new string[0];
            var control = new ArgsClass<bool>();
            Assert.Throws<NoSchemeException>(() => control.BoolArgs(scheme, null));
        }
    }
}
