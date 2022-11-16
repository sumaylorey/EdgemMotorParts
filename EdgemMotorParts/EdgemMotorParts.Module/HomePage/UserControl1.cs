using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using EdgemMotorPart.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EdgemMotorParts.Module.HomePage
{
    public partial class HomePage : UserControl
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void Report_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            XafApplication xafApplication = DomainService.Instance.Application;
            IObjectSpace objectSpace = null;
            var dialogController = new DialogController();
            dialogController.SaveOnAccept = true;

            dialogController.Cancelling += DialogController_Cancel;
            dialogController.Accepting += DialogController_Accepting;
            ShowViewParameters showViewParameters = new ShowViewParameters();
            dialogController.AcceptAction.Caption = "Add";
            dialogController.CancelAction.Caption = "Cancel";
            objectSpace = xafApplication.CreateObjectSpace();
            showViewParameters.CreatedView = xafApplication.CreateDetailView(objectSpace, "AddStockDTO_DetailView", true);
            showViewParameters.TargetWindow = TargetWindow.NewModalWindow;
            showViewParameters.Context = TemplateContext.PopupWindow;
            showViewParameters.Controllers.Add(dialogController);
            xafApplication.ShowViewStrategy.ShowView(showViewParameters, new ShowViewSource(null, null));
        }

        private void DialogController_Accepting(object sender, DialogControllerAcceptingEventArgs e)
        {
        }

        void DialogController_Cancel(object sender, EventArgs e)
        {
        }
    }
}
