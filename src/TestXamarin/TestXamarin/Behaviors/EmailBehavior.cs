using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace TestXamarin.Behaviors
{
    public class EmailValidatorBehavior
    {
        public static readonly BindableProperty AttachBehaviorProperty =
         BindableProperty.CreateAttached(
           "AttachBehavior",
           typeof(bool),
           typeof(EmailValidatorBehavior),
           false,
           propertyChanged: OnAttachBehaviorChanged);

        public static bool GetAttachBehavior(BindableObject view)
        {
            return (bool)view.GetValue(AttachBehaviorProperty);
        }

        public static void SetAttachBehavior(BindableObject view, bool value)
        {
            view.SetValue(AttachBehaviorProperty, value);
        }

        static void OnAttachBehaviorChanged(BindableObject view, object oldValue, object newValue)
        {
            var entry = view as Entry;
            if (entry == null)
            {
                return;
            }

            bool attachBehavior = (bool)newValue;
            if (attachBehavior)
            {
                entry.TextChanged += OnEntryTextChanged;
            }
            else
            {
                entry.TextChanged -= OnEntryTextChanged;
            }
        }

        static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var entry = sender as Entry;
            bool isValid = Regex.IsMatch(args.NewTextValue, @"(\w+)((\.|-)\w+)*@(\w+)(\.\w+)*");
            entry.TextColor = isValid ? Color.Default : Color.Red;            
        }
    }
}
