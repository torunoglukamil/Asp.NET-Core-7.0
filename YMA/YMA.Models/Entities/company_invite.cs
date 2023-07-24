using System;
using System.Collections.Generic;

namespace YMA.Entities.Entities;

public partial class company_invite
{
    public string id { get; set; } = null!;

    public string? receiver_id { get; set; }

    public string? sender_id { get; set; }

    public bool? is_buying { get; set; }

    public bool? is_selling { get; set; }

    public bool? is_current_account_registration { get; set; }

    public bool? is_accepted { get; set; }

    public DateTime? reply_date { get; set; }

    public DateTime? create_date { get; set; }
}
