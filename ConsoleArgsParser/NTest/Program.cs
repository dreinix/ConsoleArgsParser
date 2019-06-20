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
            var control = new ArgsClass();
            Assert.Throws<NoSchemeException>(() => control.BoolArgs(scheme, null));
        }
        [Test]
        public void OneScheme()
        {
            var scheme = new string[1];
            scheme[0] = "-p";
            var control = new ArgsClass();
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
            var control = new ArgsClass();
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
            var control = new ArgsClass();
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
            var control = new ArgsClass();
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
            var control = new ArgsClass();
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
            var control = new ArgsClass();
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
            var control = new ArgsClass();
            var expected = new Dictionary<string, bool>();
            expected.Add(scheme[0], true);
            expected.Add(scheme[1], false);
            expected.Add(scheme[2], true);
            expected.Add(scheme[3], false);

            Assert.Throws<InvalidArgException>(() => control.BoolArgs(scheme, arg));
        }
    }
    [TestFixture]
    class GeneralTests{
        [Test]
        public void boolScheme()
        {
            var scheme = new Dictionary<string, bool>();
            scheme.Add("-p", new bool());
            var control = new ArgsClass();
            control.AddScheme(scheme);
            var Expected = new Dictionary<string, string>();
            Expected.Add("-p", "false");
            Assert.AreEqual(Expected, control.GetSchemes());
        }
        [Test]
        public void intScheme()
        {
            var scheme = new Dictionary<string, int>();
            scheme.Add("-e", new int());
            var control = new ArgsClass();
            control.AddScheme<int>(scheme);
            var Expected = new Dictionary<string, string>();
            Expected.Add("-e", "");
            Assert.AreEqual(Expected, control.GetSchemes());
        }
        [Test]
        public void SomeTypeScheme()
        {
            var scheme = new Dictionary<string, int>();
            scheme.Add("-e", new int());
            var control = new ArgsClass();
            control.AddScheme<int>(scheme);
            
            var scheme2 = new Dictionary<string, bool>();
            scheme2.Add("-b", new bool());
            control.AddScheme<bool>(scheme2);
            var Expected = new Dictionary<string, string>();

            Expected.Add("-e", "");
            Expected.Add("-b", "false");
            Assert.AreEqual(Expected, control.GetSchemes());
        }
        [Test]
        public void ArgWithMoreThanOneType()
        {
            var Expected = new Dictionary<string, string>();

            var control = new ArgsClass();
            control.AddScheme<int>(new Dictionary<string, int>() { { "-e", new int() } });
            control.AddScheme<bool>(new Dictionary<string, bool>() { { "-b", new bool() } });
            string Arg = "-e 8080 -b";
            Expected.Add("-e", "8080");
            Expected.Add("-b", "true");
            Assert.AreEqual(Expected, control.GeneralSParse(Arg));
        }
        [Test]
        public void ArgWithMoreThanOneTypeII()
        {
            var Expected = new Dictionary<string, string>();

            var control = new ArgsClass();
            control.AddScheme<int>(new Dictionary<string, int>() { { "-e", new int() }, { "-d", new int() } });
            control.AddScheme<bool>(new Dictionary<string, bool>() { { "-b", new bool() } });


            string Arg = "-e 8080 -b -d 1250";
            Expected.Add("-e", "8080");
            Expected.Add("-d", "1250");
            Expected.Add("-b", "true");
            Assert.AreEqual(Expected, control.GeneralSParse(Arg));
        }
        [Test]
        public void ArgWithMoreThanOneTypeAndTwoLessSign()
        {
            var Expected = new Dictionary<string, string>();

            var control = new ArgsClass();
            control.AddScheme<int>(new Dictionary<string, int>() { { "--e", new int() }, { "--d", new int() } });
            control.AddScheme<bool>(new Dictionary<string, bool>() { { "--b", new bool() } });


            string Arg = "--e 8080 --b --d 1250";
            Expected.Add("--e", "8080");
            Expected.Add("--d", "1250");
            Expected.Add("--b", "true");
            Assert.AreEqual(Expected, control.GeneralSParse(Arg));
        }

        [Test]
        public void InvalidArgWithSomeSchemes()
        {
            var control = new ArgsClass();
            control.AddScheme<int>(new Dictionary<string, int>() { { "-e", new int() }, { "-d", new int() } });
            control.AddScheme<bool>(new Dictionary<string, bool>() { { "-b", new bool() } });
            string Arg = "-e 8080 -b -d";
            Assert.Throws<InvalidArgException>(() => control.GeneralSParse(Arg));
        }
    }
}
