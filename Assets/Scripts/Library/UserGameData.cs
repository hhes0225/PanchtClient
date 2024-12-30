using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public Int64 uid { get; set; }
    public string id { get; set; }
    public string nickname { get; set; }
    public DateTime? create_date { get; set; }
    public int total_games { get; set; }
    public int win_count { get; set; }
    public int draw_count { get; set; }
    public int lose_count { get; set; }
    public int tier_score { get; set; }
}
