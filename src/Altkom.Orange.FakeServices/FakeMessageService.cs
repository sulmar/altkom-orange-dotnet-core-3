﻿using Altkom.Orange.IServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Altkom.Orange.FakeServices
{
    public class FakeMessageService : IMessageService
    {
        public void Send(string message)
        {
            Trace.WriteLine(message);
        }
    }
}
