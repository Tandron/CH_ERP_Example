using Prism.Events;
using UsingEventAggregator.Core.Enums;

namespace UsingEventAggregator.Core
{
    public class MessageSentEvent : PubSubEvent<ErpMode> { }
}
