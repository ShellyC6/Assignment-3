﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment1
{
    [Serializable]
    public enum Type
    {
        nothing,
        villa,
        apartment,
        townhouse,
        shop,
        warehouse,
        school,
        university
    }
    [Serializable]
    public enum Category
    {
        nothing,
        residential,
        commercial,
        institutional
    }
}