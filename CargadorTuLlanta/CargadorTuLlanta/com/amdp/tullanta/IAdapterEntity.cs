﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CargadorTuLlanta.com.amdp.tullanta
{
    interface IAdapterEntity
    {
        IEntity getEntity(String line);
    }
}
