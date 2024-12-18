using Ciandt.FlowTools.FlowReviewer.ChangeTracking;
using Ciandt.FlowTools.FlowReviewer.Common;
using Ciandt.FlowTools.FlowReviewer.Persistence;
using Jab;

namespace Ciandt.FlowTools.FlowReviewer.DependencyInjection;

[ServiceProvider]
[Import(typeof(IFlowModule))]
[Import(typeof(IAgentModule))]
[Import(typeof(IExternalModule))]
[Singleton(typeof(AppJsonContext), Factory = nameof(AppJsonContextDefaultInstance))]
[Singleton(typeof(IRunner), typeof(Runner))]
[Singleton(typeof(IConfigurationService), typeof(ConfigurationService))]
[Singleton(typeof(IUserSessionService), typeof(UserSessionService))]
[Singleton(typeof(IGitDiffExtractor), typeof(GitDiffExtractor))]
public sealed partial class AppContainer
{
    private static AppJsonContext AppJsonContextDefaultInstance() => AppJsonContext.Default;
}
