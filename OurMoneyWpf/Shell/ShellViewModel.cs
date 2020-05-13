using System.Windows;
using Caliburn.Micro;
using OurMoneyWpf.Modules.Categories;
using OurMoneyWpf.Modules.ForTesting;
using OurMoneyWpf.Modules.Input;

namespace OurMoneyWpf
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IShell
    {
        private readonly CategoriesViewModel categories;
        private readonly InputViewModel input;
        private readonly ForTestingViewModel forTesting;

        public ShellViewModel(CategoriesViewModel categories, InputViewModel input, ForTestingViewModel forTesting)
        {
            this.categories = categories;
            this.input = input;
            this.forTesting = forTesting;
        }
        public void Category()
        {
            ActivateItem(categories);
        }
        public void Input()
        {
            ActivateItem(input);
        }


        public void ForTesting()
        {
            ActivateItem(forTesting);
        }
    }
}