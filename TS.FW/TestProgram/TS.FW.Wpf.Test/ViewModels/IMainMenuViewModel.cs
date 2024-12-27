using System.Collections.Generic;
using TS.FW.Wpf.Controls;
using TS.FW.Wpf.Core;

namespace TS.FW.Wpf.Test.ViewModels
{
    public abstract class IMainMenuViewModel : IMenuViewModel
    {
        public static List<IMainMenuViewModel> ToMenuList()
        {
            return IMenuViewModel.ToMenuViewModelList<IMainMenuViewModel>();
        }
    }
}
