public class Controller
{
    private readonly UserAccount _account;

    public Controller(UserAccount user)
    {
        _account = user;
    }

    public UserAccount Account
    { 
        get => _account; 
    }

}