﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NewLife.CMX.Web
{
    public interface ICommon
    {
        string Address { get; set; }

        String Foot { get; set; }

        String Header { get; set; }

        String Process();
    }
}
