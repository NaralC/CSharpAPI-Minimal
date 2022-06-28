using Models;

namespace Data {
    public interface ICommandRepo
    {
        Task SaveChangesAsync();
        Task<Command?> GetCommandById(int id);
        Task<IEnumerable<Command>> GetAllCommands();
        Task CreateCommand(Command cmd);
        void DeleteCommand(Command cmd);
    }
}