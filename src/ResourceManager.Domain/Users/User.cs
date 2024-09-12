using ResourceManager.SharedKernel;

namespace ResourceManager.Domain.Users;

public sealed class User : Entity
{
    private User(
        Guid id,
        string name,
        string username,
        Actor actor,
        Level level)
    {
        Name = name;
        Username = username;
        Actor = actor;
        Level = level;
    }

    private User() { }

    public string Name { get; private set; }
    public string Username { get; private set; }

    public Actor Actor { get; private set; }

    public Level Level { get; private set; }

    public static User Create(
        string name,
        string username,
        Actor actor,
        Level level)
    {
        var user = new User(
            Guid.NewGuid(),
            name,
            username,
            actor,
            level);

        return user;
    }

    public void Update(
        string name,
        string username,
        Actor actor,
        Level level)
    {
        Name = name;
        Username = username;
        Actor = actor;
        Level = level;
    }
}
