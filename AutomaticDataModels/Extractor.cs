using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AutomaticDataModels
{
    class Extractor
    {

        Dictionary<Type, Type> TypeDictionary;
        PropertyFinder propertyFinder;
        private static Extractor instance;

        private Extractor()
        {
            TypeDictionary = new Dictionary<Type, Type>();
            TypeDictionary.Add(typeof(ButtonDefinition), typeof(System.Windows.Controls.Button));
            TypeDictionary.Add(typeof(GridDefinition), typeof(Grid));
            propertyFinder = PropertyFinder.CreateInstance();
        }

        public static Extractor CreateInstance()
        {
            if(instance == null)
            {
                instance = new Extractor();
                
            }
            return instance;
        }

        private void BindProperty(BaseDefinition source, FrameworkElement boundElement, string sourcePropertyName, DependencyProperty property, IValueConverter converter = null)
        {
            Binding binding = new Binding(sourcePropertyName);
            binding.Source = source;
            binding.Mode = BindingMode.OneWay;
            if (converter != null)
                binding.Converter = converter;
            boundElement.SetBinding(property, binding);
        }

        public FrameworkElement ExtractAndBind(BaseDefinition model)
        {
            Type ElementType = TypeDictionary[model.GetType()];
            FrameworkElement UIElement = ControlFactory.CreateControl(ElementType);
            PropertyInfo[] properties = model.GetType().GetProperties();
            foreach (var property in properties)
            {
                object value = property.GetValue(model);
                DependencyProperty boundProperty;
                bool propertyFound = propertyFinder.TryGetProperty(property.Name, out boundProperty);
                if (propertyFound)
                {
                    IValueConverter converter;
                    bool converterExists = propertyFinder.TryGetConverter(boundProperty, out converter);
                    if (converterExists)
                        value = converter.Convert(value, null, null, null);
                    UIElement.SetValue(boundProperty, value);
                    BindProperty(model, UIElement, property.Name, boundProperty, converter);
                    
                }
            }
            model.Control = UIElement;

            if (model is ViewGroup)
            {
                ViewGroup vg = model as ViewGroup;
                Panel p = UIElement as Panel;
                foreach(var child in vg.Children)
                {
                    p.Children.Add(ExtractAndBind(child));
                }
            }
            return UIElement;
        }
    }
}
