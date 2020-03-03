﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Omex.Extensions.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Microsoft.Omex.Extensions.TimedScopes.UnitTests
{
	[TestClass]
	public class TimedScopeTests
	{
		[TestMethod]
		public void CreateWithoutReplayer()
		{
			CreateTimedScope(null);
		}


		[TestMethod]
		public void CreateWithReplayer()
		{
			CreateTimedScope(new Mock<ILogEventReplayer>().Object);
		}


		[TestMethod]
		public void StartMethodStartsActivity()
		{
			(TimedScope scope, _) = CreateTimedScope(null);

			Assert.IsNull(scope.Activity.Id);

			scope.Start();

			Assert.IsNotNull(scope.Activity.Id);
		}


		[TestMethod]
		public void MultipleCallsOfStopIgnored()
		{
			(TimedScope scope, _) = CreateTimedScope(null);
			scope.Start();
			scope.Stop();

			Assert.IsTrue(scope.IsFinished);

			scope.Stop();
		}


		[TestMethod]
		public void MultipleCallsOfDisposeIgnored()
		{
			(TimedScope scope, _) = CreateTimedScope(null);
			scope.Start();

			IDisposable disposable = scope;
			disposable.Dispose();

			Assert.IsTrue(scope.IsFinished);

			disposable.Dispose();
		}


		[TestMethod]
		public void StopCallsEventSource()
		{
			(TimedScope scope, Mock<ITimedScopeEventSender> source) = CreateTimedScope(null);

			scope.Start();

			scope.Stop();
			source.Verify(s => s.LogTimedScopeEndEvent(scope), Times.Once);
			source.Invocations.Clear();

			scope.Stop();
			source.Verify(s => s.LogTimedScopeEndEvent(It.IsAny<TimedScope>()), Times.Never);
		}


		[DataTestMethod]
		[DynamicData(nameof(AllResults), DynamicDataSourceType.Method)]
		public void StopNotCallsLogReplayerIfItNotExist(TimedScopeResult result)
		{
			Mock<ILogEventReplayer> replayer = new Mock<ILogEventReplayer>();
			(TimedScope scope, Mock<ITimedScopeEventSender> source) = CreateTimedScope(null);
			scope.Result = result;

			scope.Start();

			scope.Stop();
		}


		public static IEnumerable<object[]> AllResults() =>
			Enum.GetValues(typeof(TimedScopeResult)).Cast<object>().Select(e => new object[] { e });


		[DataTestMethod]
		[DataRow(TimedScopeResult.SystemError)]
		public void StopCallsLogReplayerInCaseOfError(TimedScopeResult result)
		{
			Mock<ILogEventReplayer> replayer = new Mock<ILogEventReplayer>();
			(TimedScope scope, Mock<ITimedScopeEventSender> source) = CreateTimedScope(replayer.Object);
			scope.Result = result;

			scope.Start();

			scope.Stop();
			replayer.Verify(r => r.ReplayLogs(scope.Activity), Times.Once);
			replayer.Invocations.Clear();

			scope.Stop();
			replayer.Verify(s => s.ReplayLogs(It.IsAny<Activity>()), Times.Never);
		}


		[DataTestMethod]
		[DataRow(TimedScopeResult.ExpectedError)]
		[DataRow(TimedScopeResult.Success)]
		public void StopNotCallsLogReplayerInCaseOfSucces(TimedScopeResult result)
		{
			Mock<ILogEventReplayer> replayer = new Mock<ILogEventReplayer>();
			(TimedScope scope, Mock<ITimedScopeEventSender> source) = CreateTimedScope(replayer.Object);
			scope.Result = result;

			scope.Start();

			scope.Stop();
			replayer.Verify(s => s.ReplayLogs(It.IsAny<Activity>()), Times.Never);
		}


		private (TimedScope, Mock<ITimedScopeEventSender>) CreateTimedScope(ILogEventReplayer? replayer)
		{
			Activity activity = new Activity("TestName");
			TimedScopeResult result = TimedScopeResult.Success;

			Mock<ITimedScopeEventSender> eventSource = new Mock<ITimedScopeEventSender>();

			TimedScope scope = new TimedScope(eventSource.Object, activity, result, replayer);

			Assert.ReferenceEquals(activity, scope.Activity);
			Assert.AreEqual(result, scope.Result);
			Assert.IsNotNull(scope.SubType);
			Assert.IsNotNull(scope.MetaData);
			Assert.IsFalse(scope.IsFinished);

			return (scope, eventSource);
		}
	}
}
