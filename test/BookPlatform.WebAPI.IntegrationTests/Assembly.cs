using BookPlatform.WebAPI.IntegrationTests.Priorities;


[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: TestCollectionOrderer(
    ordererTypeName: PriorityConstants.CollectionPriorityOrdererTypeName,
    ordererAssemblyName: PriorityConstants.AssemblyName)]