using System;
using System.Collections.Generic;


namespace ChatBot_320
{
    public interface IResponseLoader
    {
        List<KeywordResponse> LoadResponses(string path);
    }
}
