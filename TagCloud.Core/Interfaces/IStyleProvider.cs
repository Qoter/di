using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Core.Interfaces
{
    public interface IStyleProvider
    {
        Color BackgroundColor { get; }
        Style GetStyle(string word);
    }
}