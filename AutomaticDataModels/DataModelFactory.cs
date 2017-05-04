using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace AutomaticDataModels
{
    class DataModelFactory
    {

        private Extractor ModelExtractor;
        private Dictionary<Type, double> HeightDictionary;
        private Dictionary<Type, double> WidthDictionary;

        private static DataModelFactory instance;

        public static DataModelFactory CreateInstance()
        {
            if(instance == null)
            {
                instance = new DataModelFactory();
            }
            return instance;
        }

        private DataModelFactory()
        {
            ModelExtractor = Extractor.CreateInstance();
            HeightDictionary = new Dictionary<Type, double>();
            WidthDictionary = new Dictionary<Type, double>();
            BuildDictionaries();
        }

        private void BuildDictionaries()
        {
            HeightDictionary.Add(typeof(ButtonDefinition), 23);
            WidthDictionary.Add(typeof(ButtonDefinition), 75);
        }

        public T ConstructModel<T>() where T : BaseDefinition
        {
            T Model = (T)typeof(T).GetConstructor(new Type[] { }).Invoke(new object[] { });
            ModelExtractor.ExtractAndBind(Model);
            Model.HeightRequest = HeightDictionary[Model.GetType()];
            Model.WidthRequest = WidthDictionary[Model.GetType()];
            return Model;       
        }
    }
}
