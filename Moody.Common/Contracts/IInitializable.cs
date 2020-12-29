using System.Threading.Tasks;

namespace Moody.Common.Contracts
{
    public interface IInitializable
    {
        int Order { get; }
        Task Initialize();
    }
}