﻿namespace Microsoft.HockeyApp.Extensibility
{
    using System;
    using System.Reflection;
    using TestFramework;
#if WINDOWS_PHONE || WINDOWS_STORE || WINDOWS_UWP
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
    using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif
    using Assert = Xunit.Assert;

    [TestClass]
    public class SequencePropertyInitializerTest
    {
        [TestMethod]
        public void ClassImplementsITelemetryInitializerBecauseSequenceChangesForEachTelemetry()
        {
            Assert.True(typeof(ITelemetryInitializer).GetTypeInfo().IsAssignableFrom(typeof(SequencePropertyInitializer).GetTypeInfo()));
        }

        [TestMethod]
        public void InitializeSetsSequencePropertyValue()
        {
            var telemetry = new StubTelemetry();
            new SequencePropertyInitializer().Initialize(telemetry);
            Assert.NotEmpty(telemetry.Sequence);
        }

        [TestMethod]
        public void InitializePreservesExistingSequencePropertyValue()
        {
            string originalValue = Guid.NewGuid().ToString();
            var telemetry = new StubTelemetry { Sequence = originalValue };
            new SequencePropertyInitializer().Initialize(telemetry);
            Assert.Equal(originalValue, telemetry.Sequence);
        }

        [TestMethod]
        public void InitializeGeneratesUniqueSequenceValuesWhenCalledMultipleTimes()
        {
            var initializer = new SequencePropertyInitializer();

            var telemetry1 = new StubTelemetry();
            initializer.Initialize(telemetry1);
            var telemetry2 = new StubTelemetry();
            initializer.Initialize(telemetry2);

            Assert.NotEqual(telemetry1.Sequence, telemetry2.Sequence);
        }

        [TestMethod]
        public void InitializeGeneratesUniqueValuesWhenCalledOnMultipleInstances()
        {
            var telemetry1 = new StubTelemetry();
            new SequencePropertyInitializer().Initialize(telemetry1);

            var telemetry2 = new StubTelemetry();
            new SequencePropertyInitializer().Initialize(telemetry2);

            Assert.NotEqual(telemetry1.Sequence, telemetry2.Sequence);
        }

        [TestMethod]
        public void InitializeSeparatesStableIdAndNumberWithColonToConformWithVortexSpecification()
        {
            var telemetry = new StubTelemetry();
            new SequencePropertyInitializer().Initialize(telemetry);
            Assert.Contains(":", telemetry.Sequence, StringComparison.Ordinal);
        }

        [TestMethod]
        public void InitializeDoesNotIncludeBase64PaddingInSequenceToReduceDataSize()
        {
            var telemetry = new StubTelemetry();
            new SequencePropertyInitializer().Initialize(telemetry);
            Assert.DoesNotContain("=", telemetry.Sequence, StringComparison.Ordinal);
        }
    }
}
