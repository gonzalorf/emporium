//using Emporium.Domain.SeedWork;
//using Emporium.Infrastructure.Context;
//using Microsoft.Extensions.Configuration;

//namespace Emporium.Infrastructure.Domain.CosmosDBRepositories;

//public class EventRepository : IEventRepository
//{
//    private readonly IConfiguration _cfg;
//    private IContainerContext Context { get; }

//    private const string EVENT_TYPE = "domainEvent";
//    private readonly int DEFAULT_TTL;

//    public EventRepository(IContainerContext ctx, IConfiguration cfg)
//    {
//        _cfg = cfg;
//        DEFAULT_TTL = _cfg.GetSection("Events")?["Ttl"] == null
//            ? 120
//            : int.Parse(_cfg.GetSection("Events")?["Ttl"]);
//        Context = ctx;
//    }

//    public void Create(IDomainEvent e)
//    {
//        var o = new DataObject<DomainEventBase>(e.Id.ToString(), e.RelatedId.ToString(), EVENT_TYPE, e as DomainEventBase, null,
//            DEFAULT_TTL, EntityState.Created);
//        Context.Add(o);
//    }
//}