using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using OurMoneyWpf.Modules.Categories;
using OurMoneyWpf.Modules.Input;

namespace OurMoneyWpf.ConfigureApp
{
    public class MainControlsServices
    {
        public MainControlsServices(SimpleContainer container)
        {
            container.PerRequest<InputViewModel>();
            container.PerRequest<CategoriesViewModel>();

        }
    }
}
