using System.Threading.Tasks;

namespace StateImplements
{
    public interface IEnemyState
    {
        Task<StateResult> DoAction(object data);
    }
}