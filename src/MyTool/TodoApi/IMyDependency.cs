using System.Threading.Tasks;

namespace TodoApi
{
    public interface  IMyDependency
    {
        Task WriteMessage(string message);
    }
}