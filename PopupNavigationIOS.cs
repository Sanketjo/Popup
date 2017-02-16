using System.Linq;
using Foundation;
using UIKit;
using ValuD.MobileApp.IWSM.Popup.IOS.Impl;
using Xamarin.Forms.Platform.iOS;
using ValuD.MobileApp.IWSM.iOS.Common;
using Xamarin.Forms;
using Popup;

[assembly: Dependency(typeof(PopupNavigationIOS))]
namespace ValuD.MobileApp.IWSM.Popup.IOS.Impl
{
    [Preserve(AllMembers = true)]
    internal class PopupNavigationIOS : IPopupNavigation
    {
        public void AddPopup(PopupPage page)
        {
            var topViewController = GetTopViewController();
            var topRenderer = topViewController.ChildViewControllers.LastOrDefault() as IVisualElementRenderer;

            if (topRenderer != null)
                page.Parent = topRenderer.Element;
            else
                page.Parent = Application.Current.MainPage;

            var renderer = page.GetOrCreateRenderer();

            topViewController.View.AddSubview(renderer.NativeView);
        }

        public void RemovePopup(PopupPage page)
        {
            var renderer = page.GetOrCreateRenderer();
            var viewController = renderer?.ViewController;

            if (viewController != null && !viewController.IsBeingDismissed)
                renderer.NativeView.RemoveFromSuperview();

        }

        private UIViewController GetTopViewController()
        {
            var topViewController = UIApplication.SharedApplication.KeyWindow.RootViewController;

            while (topViewController.PresentedViewController != null)
            {
                topViewController = topViewController.PresentedViewController;
            }

            return topViewController;
        }
    }
}
