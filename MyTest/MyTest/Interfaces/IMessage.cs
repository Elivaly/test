using MyTest.Models;

namespace MyTest.Interfaces
{
    public interface IMessage
    {
        IEnumerable <Message> Messages { get; }
    }
}
