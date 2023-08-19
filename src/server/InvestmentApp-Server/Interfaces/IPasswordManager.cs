namespace InvestmentApp.Interfaces;

public interface IPasswordManager
{
    string Hash(string input);

    bool Verify(string input, string hashString);
}
