using BookPlatform.WebAPI.IntegrationTests.Priorities.Attributes;
using Xunit.Abstractions;

namespace BookPlatform.WebAPI.IntegrationTests.Priorities;

public sealed class CollectionPriorityOrderer : ITestCollectionOrderer
{
    public IEnumerable<ITestCollection> OrderTestCollections(
        IEnumerable<ITestCollection> testCollections)
    {
        string assemblyName = typeof(CollectionPriorityAttribute).AssemblyQualifiedName!;
        var sortedMethods = new SortedDictionary<int, List<ITestCollection>>();
        foreach (ITestCollection testCollection in testCollections)
        {
            int priority = testCollection.CollectionDefinition
                .GetCustomAttributes(assemblyName)
                .FirstOrDefault()
                ?.GetNamedArgument<int>(nameof(CollectionPriorityAttribute.Priority)) ?? 0;

            GetOrCreate(sortedMethods, priority).Add(testCollection);
        }

        foreach (ITestCollection testCollection in
                 sortedMethods.Keys.SelectMany(
                     priority => sortedMethods[priority].OrderBy(
                         testCollection => testCollection.DisplayName)))
        {
            yield return testCollection;
        }
    }

    private static TValue GetOrCreate<TKey, TValue>(
        IDictionary<TKey, TValue> dictionary, TKey key)
        where TKey : struct
        where TValue : new() =>
        dictionary.TryGetValue(key, out TValue? result)
            ? result
            : (dictionary[key] = new TValue());
}