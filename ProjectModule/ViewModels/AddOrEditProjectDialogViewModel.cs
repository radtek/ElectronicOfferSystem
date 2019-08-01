using BusinessData;
using BusinessData.Dal;
using Common.Utils;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectModule.ViewModels
{
    public class AddOrEditProjectDialogViewModel : BindableBase
    {

        private Project project;
        public Project Project
        {
            get { return project; }
            set { SetProperty(ref project, value); }
        }

        private string dialogTitle;
        public string DialogTitle
        {
            get { return dialogTitle; }
            set
            {
                SetProperty(ref dialogTitle, value);
            }
        }

        #region 字典
        /// <summary>
        /// 权籍调查/补录
        /// </summary>
        private Dictionary<string, string> ownershipTypeList;
        public Dictionary<string, string> OwnershipTypeList
        {
            get { return ownershipTypeList; }
            set { SetProperty(ref ownershipTypeList, value); }
        }
        /// <summary>
        /// 测绘类型
        /// </summary>
        private Dictionary<string, string> mappingTypeList;
        public Dictionary<string, string> MappingTypeList
        {
            get { return mappingTypeList; }
            set { SetProperty(ref mappingTypeList, value); }
        }
        #endregion

        private static AddOrEditProjectDialogViewModel addOrEditProjectDialogViewModel = new AddOrEditProjectDialogViewModel();


        public AddOrEditProjectDialogViewModel()
        {
            // Project = new Project();
            // 初始化下拉框
            InitialComboBoxList();
        }

        public static AddOrEditProjectDialogViewModel getInstance()
        {
            return addOrEditProjectDialogViewModel;
        }

        private void InitialComboBoxList()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic = DictionaryUtil.GetDictionaryByName("权籍调查/补录");
            OwnershipTypeList = dic;

            dic = DictionaryUtil.GetDictionaryByName("测绘类型");
            MappingTypeList = dic;
        }
    }
}
