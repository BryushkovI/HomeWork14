using HomeWork15.Models;
using HomeWork15.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork15.DataProvider
{
    class DataProvider
    {
        IParser _parser;
        public DataProvider(IParser Parser) => _parser = Parser;

        public void SaveData<T>(ObservableCollection<T> List)
        {
            
        }
        public ObservableCollection<T> GetData<T>()
        {
            return new ObservableCollection<T>();
        }
    }
}
