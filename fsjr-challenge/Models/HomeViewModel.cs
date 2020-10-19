using fsjr_challenge.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace fsjr_challenge.Models
{
    public class HomeViewModel
    {
        public UserData UserData { get; set; }
        public List<UserData> Users { get; set; }
    }
}
