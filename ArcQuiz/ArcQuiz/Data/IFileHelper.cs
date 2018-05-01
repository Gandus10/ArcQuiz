using System;
using System.Collections.Generic;
using System.Text;

namespace ArcQuiz.Data
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
