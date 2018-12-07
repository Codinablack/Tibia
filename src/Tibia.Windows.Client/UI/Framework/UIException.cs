﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tibia.Windows.Client
{
    /// <summary>
    /// Exception thrown when the UI is used in a bad (tm) way.
    /// </summary>
    class UIException : InvalidOperationException
    {
        public UIException(String message)
            : base(message)
        {
        }
    }
}
