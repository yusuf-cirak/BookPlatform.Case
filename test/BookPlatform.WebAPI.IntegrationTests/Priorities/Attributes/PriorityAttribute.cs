namespace BookPlatform.WebAPI.IntegrationTests.Priorities.Attributes;

public class PriorityAttribute(int priority) : Attribute
{
    public int Priority { get; private set; } = priority;
}