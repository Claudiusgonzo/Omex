﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Omex.Extensions.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Microsoft.Omex.Extensions.TimedScopes.UnitTests
{
	[TestClass]
	public class TaskExtensionsTests
	{
		[TestMethod]
		public void TestSuccesWithTaskOfT() => TestSuccesfulExecution(s_wrapTaskOfTAction);


		[TestMethod]
		public void TestSuccesWithTask() => TestSuccesfulExecution(s_wrapTaskAction);


		[TestMethod]
		public void TestSuccesWithValueTaskOfT() => TestSuccesfulExecution(s_wrapValueTaskOfTAction);


		[TestMethod]
		public void TestSuccesWithValueTask() => TestSuccesfulExecution(s_wrapValueTaskAction);


		[TestMethod]
		public void TestFailedWithTaskOfT() => TestFailedExecution(s_wrapTaskOfTAction);


		[TestMethod]
		public void TestFailedWithTask() => TestFailedExecution(s_wrapTaskAction);


		[TestMethod]
		public void TestFailedWithValueTaskOfT() => TestFailedExecution(s_wrapValueTaskOfTAction);


		[TestMethod]
		public void TestFailedWithValueTask() => TestFailedExecution(s_wrapValueTaskAction);


		private void TestSuccesfulExecution(Action<Task<bool>, ITimedScopeProvider, TimedScopeDefinition> wrapTask, [CallerMemberName] string scopeName = "") =>
			TestExecution(scopeName, wrapTask, tcs => tcs.SetResult(true), TimedScopeResult.Success);


		private void TestFailedExecution(Action<Task<bool>, ITimedScopeProvider, TimedScopeDefinition> wrapTask, [CallerMemberName] string scopeName = "") =>
			TestExecution(scopeName, wrapTask, tcs => tcs.SetException(new Exception("Some failure")), TimedScopeResult.SystemError);


		private void TestExecution(
			string scopeName,
			Action<Task<bool>, ITimedScopeProvider, TimedScopeDefinition> createTask,
			Action<TaskCompletionSource<bool>> finishTask,
			TimedScopeResult expectedResult)
		{
			Mock<ITimedScopeProvider> providerMock = new Mock<ITimedScopeProvider>();
			Mock<ITimedScopeEventSender> mockSender = new Mock<ITimedScopeEventSender>();
			TimedScopeDefinition timedScopeDefinition = new TimedScopeDefinition(scopeName);
			Activity activity = new Activity(scopeName);

			TimedScope scope = new TimedScope(mockSender.Object, activity, TimedScopeResult.SystemError, null);
			providerMock.Setup(p => p.Start(timedScopeDefinition, TimedScopeResult.SystemError)).Returns(scope);

			TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();

			createTask(taskCompletionSource.Task, providerMock.Object, timedScopeDefinition);

			providerMock.Verify(p => p.Start(timedScopeDefinition, TimedScopeResult.SystemError), Times.Once);
			Assert.AreEqual(scope.Result, TimedScopeResult.SystemError);

			finishTask(taskCompletionSource);

			Assert.IsTrue(scope.IsFinished);
			Assert.AreEqual(scope.Result, expectedResult);
		}


		private static Action<Task<bool>, ITimedScopeProvider, TimedScopeDefinition> s_wrapTaskOfTAction =
			(task, provider, name) => task.WithTimedScope(provider, name);


		private static Action<Task<bool>, ITimedScopeProvider, TimedScopeDefinition> s_wrapTaskAction =
			(task, provider, name) => ((Task)task).WithTimedScope(provider, name);


		private static Action<Task<bool>, ITimedScopeProvider, TimedScopeDefinition> s_wrapValueTaskOfTAction =
			async (task, provider, name) => await new ValueTask<bool>(task).WithTimedScope(provider, name);


		private static Action<Task<bool>, ITimedScopeProvider, TimedScopeDefinition> s_wrapValueTaskAction =
			async (task, provider, name) => await new ValueTask(task).WithTimedScope(provider, name);
	}
}
