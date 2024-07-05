namespace BookPlatform.WebAPI.IntegrationTests.Priorities.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class CollectionPriorityAttribute(int priority) : PriorityAttribute(priority);