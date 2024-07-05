namespace BookPlatform.WebAPI.IntegrationTests.Priorities.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class FactPriorityAttribute(int priority) : PriorityAttribute(priority);