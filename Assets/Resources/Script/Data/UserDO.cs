
public class UserDO  {

    private static UserDO _instance;
    public static UserDO Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UserDO();

            }

            return _instance;
        }
    }

    private UserDO() {
        _stage = 0;
        _charactor = "";
    }

    private int _stage;
    private string _charactor;

    public void SetStage(int s)
    {
        _stage = s;
    }

    public int GetStage()
    {
        return _stage;
    }

    public void SetCharactor(string c)
    {
        _charactor = c;
    }

    public string GetCharactor()
    {
        return _charactor;
    }


}
