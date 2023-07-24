﻿using System.Globalization;

namespace YMA.Entities.Models;

public class AddressModel
{
    private readonly TextInfo _textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;

    private string? _title;
    private string? _full_address;
    private string? _province;
    private string? _district;
    private string? _neighbourhood;

    public string? id { get; set; }

    public string? title
    {
        get { return _title; }
        set { _title = _textInfo.ToTitleCase(value!.Trim()); }
    }

    public string? full_address
    {
        get { return _full_address; }
        set { _full_address = _textInfo.ToTitleCase(value!.Trim()); }
    }

    public string? province
    {
        get { return _province; }
        set { _province = _textInfo.ToTitleCase(value!.Trim()); }
    }

    public string? district
    {
        get { return _district; }
        set { _district = _textInfo.ToTitleCase(value!.Trim()); }
    }

    public string? neighbourhood
    {
        get { return _neighbourhood; }
        set { _neighbourhood = _textInfo.ToTitleCase(value!.Trim()); }
    }
}
