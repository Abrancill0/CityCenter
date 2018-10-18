using System;
using Xamarin.Forms;
using City_Center.Models;
namespace City_Center.Clases
{  
		public class ChatDataTemplateSelector: DataTemplateSelector
        {

            public DataTemplate FromTemplate { get; set; }
            public DataTemplate ToTemplate { get; set; }

            protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
            {
            return ((Chat)item).UserName.Equals("Administrador") ? ToTemplate : FromTemplate;
            }


        }
}
