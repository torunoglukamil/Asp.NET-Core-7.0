using System.Globalization;

namespace YMA.Entities.Models;

public class AccountModel
{
    private readonly TextInfo _textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;

    private string? _first_name;
    private string? _last_name;
    private string? _email;
    private string? _phone;

    public int id { get; set; }

    public string? first_name
    {
        get { return _first_name; }
        set { _first_name = _textInfo.ToTitleCase(value!.Trim()); }
    }

    public string? last_name
    {
        get { return _last_name; }
        set { _last_name = _textInfo.ToTitleCase(value!.Trim()); }
    }

    public string? email
    {
        get { return _email; }
        set { _email = value!.Replace(" ", "").ToLower(); }
    }

    public string? phone
    {
        get { return _phone; }
        set
        {
            string _value = (value ?? string.Empty).Replace(" ", "");
            if (_value == string.Empty)
            {
                _phone = null;
            }
            else
            {
                _phone = _value;
            }
        }
    }

    public int? default_address_id { get; set; }
}
