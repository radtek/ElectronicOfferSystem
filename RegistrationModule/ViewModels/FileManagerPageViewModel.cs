using BusinessData;
using BusinessData.Dal;
using Common.Events;
using Common.Models;
using Common.Utils;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace RegistrationModule.ViewModels
{
    public class FileManagerPageViewMode : BindableBase, INavigationAware
    {
        IEventAggregator EA;


        public Project Project { get; set; }

        private FileInfo fileInfo;
        public FileInfo FileInfo
        {
            get { return fileInfo; }
            set { SetProperty(ref fileInfo, value); }
        }

        private TreeNode treeNode;
        public TreeNode TreeNode
        {
            get { return treeNode; }
            set { SetProperty(ref treeNode, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private object selectedTreeNode;
        public object SelectedTreeNode
        {
            get { return selectedTreeNode; }
            set
            {
                this.MutateVerbose(ref selectedTreeNode, value, args => PropertyChanged?.Invoke(this, args));
            }
        }


        private ObservableCollection<FileInfo> fileInfoList;
        public ObservableCollection<FileInfo> FileInfoList
        {
            get { return fileInfoList; }
            set { SetProperty(ref fileInfoList, value); }
        }

        private ObservableCollection<TreeNode> treeList;

        public ObservableCollection<TreeNode> TreeList
        {
            get { return treeList; }
            set { SetProperty(ref treeList, value); }
        }

        public DelegateCommand FileUpLoadCommand { get; set; }

        public FileInfoDal FileInfoDal { get; set; }
        public ProjectDal ProjectDal { get; set; }

        

        public FileManagerPageViewMode()
        {
            //EA = ea;
            FileInfo = new FileInfo();
            ProjectDal = new ProjectDal();
            // 初始化树
            InitTreeView();

            FileUpLoadCommand = new DelegateCommand(FileUpLoad);
        }



        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // 获取选中项目
            Project project = navigationContext.Parameters["Project"] as Project;
            if (project != null)
            {
                Project = project;
            }
            // 初始化登记项目
            Project = ProjectDal.InitialRegistrationProject(Project);
            // 获取附件集合
            FileInfoList = new ObservableCollection<FileInfo>(Project.FileInfos);

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            Project project = navigationContext.Parameters["Project"] as Project;
            if (project != null)
            {
                return project != null;
            }
            else
            {
                return true;
            }
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        private void FileUpLoad()
        {
            TreeNode = selectedTreeNode as TreeNode;
            if (TreeNode == null || TreeNode.Children != null)
                return;

            // 选择附件
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "选择文件",
                Filter = "(*.jpg,*.png,*.jpeg,*.bmp)|*.jpg;*.png;*.jpeg;*.bmp",
                FileName = "选择文件",
                FilterIndex = 1,
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true,
                Multiselect = true
            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                // 暂时使用Test路径
                string projectPath = @"D:\vs-workspace\Test\";

                String[] SelectedFilePaths = openFileDialog.FileNames;
                bool isShow = false;
                foreach (String baseAddress in SelectedFilePaths)
                {
                    // 判断图片大小，不能超过2m（1024*2）kb
                    System.IO.FileInfo selectedFileInfo = new System.IO.FileInfo(baseAddress);
                    if (selectedFileInfo.Length > 1024 * 1024 * 2)
                    {
                        isShow = true;
                        continue;
                    }
                    String filename = System.IO.Path.GetFileName(baseAddress); // 文件原名称
                    String path = Project.ID + "\\" + TreeNode.Name + "\\"; // 相对路径


                    StringBuilder address = new StringBuilder(projectPath);
                    address.Append(Project.ID).Append("\\").Append(TreeNode.Name);
                    if (!System.IO.Directory.Exists(address.ToString()))
                    {
                        System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(address.ToString());
                        directoryInfo.Create();
                    }
                    address.Append("\\").Append(filename);
                    System.IO.FileStream fsRead = new System.IO.FileStream(baseAddress, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    System.IO.FileStream fsWrite = new System.IO.FileStream(address.ToString(), System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
                    byte[] arr = new byte[200];
                    //记录到底读取了多少字节的数据
                    int count = 0;
                    while (fsRead.Position < fsRead.Length)
                    {
                        //每一次读取,返回真正读取到的字节数，用count记录（最后一次读取后可能count可能会小于200）
                        count = fsRead.Read(arr, 0, arr.Length);
                        //将数组中的数据写入到指定的文件
                        fsWrite.Write(arr, 0, count);
                    }

                    fsWrite.Close();
                    fsRead.Close();


                    FileInfo uploadFile = new FileInfo();
                    uploadFile.ID = Guid.NewGuid();
                    uploadFile.ProjectID = Project.ID;
                    uploadFile.Name = filename;
                    uploadFile.Path = path;
                    uploadFile.Type = TreeNode.Name;
                    uploadFile.UpdateTime = DateTime.Now;
                    FileInfoDal fileInfoDal = new FileInfoDal();
                    fileInfoDal.Add(uploadFile);
                    FileInfoList.Add(uploadFile);
                }
                if (isShow)
                {
                    // DevComponents.DotNetBar.MessageBoxEx.Show("上传的每张图片大小不能超过2M");
                }

            }
        }

        private void DelFile()
        {
            // 删除数据
            //FileManageService.DeleteFileManageById(id);
            //_fileManages.Remove(fileManage);
            //// 删除文件
            //List<UserSet> userSets = UserSetService.SelectUserSet();
            //FileInfo file = new FileInfo(userSets[0].Path + "\\" + path);
            //if (file.Exists)
            //{
            //    file.Delete();
            //}
        }

        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitTreeView()
        {
            TreeList = new ObservableCollection<TreeNode>
            {
                new TreeNode("附件类型",
                    new TreeNode ("初始（首次）登记",
                        new TreeNode("国有建设用地使用权/房屋所有权",
                            new TreeNode("登记费发票"),
                            new TreeNode("询问笔录"),
                            new TreeNode("领证凭证"),
                            new TreeNode("申请书"),
                            new TreeNode("税票"),
                            new TreeNode("房屋测绘报告"),
                            new TreeNode("开发建设单位与业主的书面约定材料"),
                            new TreeNode("建设工程规划许可证明文件"),
                            new TreeNode("其他法律、法规材料"),
                            new TreeNode("申请人身份证明"),
                            new TreeNode("不动产登记申请审批表"),
                            new TreeNode("不动产权属证书"),
                            new TreeNode("房屋、建设用地项目竣工验收证明文件")
                        ),
                        new TreeNode("一般抵押权",
                            new TreeNode("受理材料"),
                            new TreeNode("建设工程规划许可证"),
                            new TreeNode("申请人身份证明"),
                            new TreeNode("主债权合同"),
                            new TreeNode("其他法律、法规要求提供的材料"),
                            new TreeNode("不动产权属证书（证明）"),
                            new TreeNode("抵押合同"),
                            new TreeNode("委托书"),
                            new TreeNode("申请书"),
                            new TreeNode("税票"),
                            new TreeNode("领证凭证"),
                            new TreeNode("登记费发票"),
                            new TreeNode("询问笔录"),
                            new TreeNode("不动产登记申请审批表")
                        )
                    ),
                    new TreeNode("预告登记",
                        new TreeNode("抵押权预告",
                            new TreeNode("领证凭证"),
                            new TreeNode("询问笔录"),
                            new TreeNode("申请书"),
                            new TreeNode("约定预告登记的协议"),
                            new TreeNode("登记费发票"),
                            new TreeNode("主债权合同"),
                            new TreeNode("房地产抵押合同"),
                            new TreeNode("申请人身份证明"),
                            new TreeNode("不动产登记申请审批表"),
                            new TreeNode("其他法律、法规要求提供的材料")
                        ),
                        new TreeNode("预告+预抵押",
                            new TreeNode("不动产登记申请审批表"),
                            new TreeNode("商品房买卖合同"),
                            new TreeNode("其他法律、法规要求提供的材料"),
                            new TreeNode("约定预告登记的协议"),
                            new TreeNode("房地产抵押合同和主债权合同"),
                            new TreeNode("申请书"),
                            new TreeNode("资质"),
                            new TreeNode("询问笔录"),
                            new TreeNode("领证凭证"),
                            new TreeNode("登记费发票"),
                            new TreeNode("委托书"),
                            new TreeNode("申请人身份证明")
                        )
                    ),
                    new TreeNode("查（解）封登记",
                        new TreeNode("查封登记",
                            new TreeNode("工作证"),
                            new TreeNode("委托送达函"),
                            new TreeNode("协助执行通知书"),
                            new TreeNode("（预）查封裁定书"),
                            new TreeNode("申请书"),
                            new TreeNode("查封函"),
                            new TreeNode("其他法律、法规要求提供的材料"),
                            new TreeNode("查封决定书"),
                            new TreeNode("协助查封通知书")
                        )
                    )
                )
            };
        }
    }
}
