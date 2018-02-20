using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Serve.Platform.Configuration.UI.SeleniumTest.Common
{
    public static class Constants
    {
        #region MenuLinks
        public const string ConfigurationMenuLink = "a[href='/Configuration/Configuration']";
        public const string ManageMenuLink = "a[href='/Security']";
        public const string ConfigItemMenuLink = "a[href='/Configuration/ConfigurationItem']";
        public const string AppUsersMenuLink = "a[href='/UserManagement/Users']";
        public const string AppNamespaceMenuLink = "a[href='/ConfigAppNamespace/ConfigAppNamespace']";
        public const string ConfigItemMetadataMenuLink = "a[href='/ConfigurationItemMetdata']";
        #endregion

        #region Grid Elements

        public const string GridPagePrevious = "superGridPrevPage{0}";
        public const string GridPageNext = "superGridNextPage{0}";
        public const string GridLastPage = "superGridLastPage{0}";
        public const string GridFirstPage = "superGridFirstPage{0}";
        public const string GridGoToTextbox = "supergridgototext{0}";
        public const string GridGoToButtonSubmit = "goToPageBtn{0}";
        public const string GridTableId = "{0}Data";
        public const string GridCurrentPage = "superGridCurrentPage{0}";
        public const string GridTotalPageCount = "superGridPageTotalCount{0}";

        #endregion

        #region  Config Item constants

        public const string ConfigItemAppNameSpaceDllXPath = "//select[@id='ddlAppNameSpace']";
        public const string ConfigItemGridXPath = "//div[@id='myGrid']/table";
        public const string ShowInactiveItemsXPath = "//div[@id='body']/div/section/div/div[3]/div[3]/label/input";
        public const string SearchByNameTextBoxId = "txtSearch";
        public const string ClearImageIconXPath = "//div[@id='body']/div/section/div/div[3]/div/div";
        public const string AddNewButtonId = "create-item";
        public const string  GridId="myGrid";
        public const string ConfigItemLink = "Configuration/Config Item";
        public const  string SearchByName = "Search By Name";
        public const  string AddNewConfigAppNamespaceTitle = "Configuration Management Console - App Namespace";
        public const  string AddNewConfigItemTitle = "Configuration Management Console - Configuration Item";
        public const  string InvalidItemName = "HDF13RYSGA67HGSA";
        public const  string NoRecordsFoundMessage = "No matching records found";
        public const  string AppNamespaceTitle = "app namespace";
        public const string AddNewConfigItemLink = "Add New Item";
        public const string ErrorMessageDateId = "errordiv";
        public const string DeleteButtonClass = "delete";
        public const string DeleteTitle = "Do you want to change the status?";
        public const string IsSecureId = "IsSecure";
        public const string IsSecurePopUpContent = "You have selected to secure this value, all versions will remain secured";
        public const string AuthenticationKeyPopUp = "You are about to rotate the AuthenticationKey for Serve.Test, Are you sure?";
        public const string SymmetricKeyPopUp = "You are about to rotate the SymmetricKey for Serve.Test, Are you sure?";

        #endregion

        #region  Config Item Metadata constants

        public const string ConfigItemMetadataAppNameSpaceDllXPath = "//select[@id='ddlAppNameSpace']";
        public const string ConfigItemMetaDataLink = "Configuration/Config Item Meta Data";
        public const string AddNewConfigItemMetadataTitle = "Configuration Management Console - Configuration Item Metadata";

        #endregion

        #region Config AppNamespace Constants

        public const string ConfigAppNamespaceLink = "Manage/App Namespace";      
        public const string AppNamespaceNameTextBoxId = "txtName";
        public const string AppNamespaceDescriptionTextBoxId = "txtDescription";
        public const string SaveButtonId = "modalSubmit";
        public const string CloseButtonId = "modalClose";
        public const string IsEnabledCheckBoxId = "IsEnabled";
        public const string ErrorMessageId = "field-validation-error";
        public const string ConfigAppNamespaceGridXPath = "//div[@id='myGrid']//table/tbody/tr/th";

        #endregion

        #region ==Common==

        public const string GridFirst = "superGridFirstPage";
        public const string GridLast = "superGridLastPage";
        public const string GridPrevious = "superGridPrevPage";
        public const string GridNext = "superGridNextPage";
        public const string GridXPath = "//div[@id='{0}']/table";
        public const string GridRowName = "myGridRow{0}";
        public const string GridGoToText = "supergridgototext";
        public const string GridGo = "goToPageBtn";
        public const string LockImageXpath="//tr[@id='myGridRow0']/td[2]/a/img";

        #endregion

        public const string ConfigTestAppNamespaceName ="Config.Test.Dev";
    }
}
