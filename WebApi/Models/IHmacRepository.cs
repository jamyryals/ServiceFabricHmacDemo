﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public interface IHmacRepository
    {
        Task<bool> HmacExists(string hmac);
    }
}