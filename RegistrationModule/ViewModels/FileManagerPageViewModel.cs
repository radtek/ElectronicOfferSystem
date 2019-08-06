using BusinessData;
using BusinessData.Dal;
using Common.Events;
using Common.Models;
using Common.Utils;
using Common.Views;
using MaterialDesignThemes.Wpf;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using Common.ViewModels;

namespace RegistrationModule.ViewModels
{
    public class FileManagerPageViewMode : BindableBase, INavigationAware
    {
        //IEventAggregator EA;


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
        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name { get; set; }

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
        public DelegateCommand<object> DelFileCommand { get; set; }
        public DelegateCommand SelectNodeCommand { get; set; }

        public FileInfoDal FileInfoDal { get; set; }
        public ProjectDal ProjectDal { get; set; }

        public SnackbarMessageQueue MessageQueue { get; set; }

        public FileManagerPageViewMode()
        {
            //EA = ea;
            FileInfo = new FileInfo();
            ProjectDal = new ProjectDal();
            FileInfoDal = new FileInfoDal();
            // 初始化树
            InitTreeView();

            FileUpLoadCommand = new DelegateCommand(FileUpLoad);

            SelectNodeCommand = new DelegateCommand(SelectNode);

            DelFileCommand = new DelegateCommand<object>(DelFile);
        }

        private void SelectNode()
        {

            TreeNode = selectedTreeNode as TreeNode;
            if (TreeNode.ID.Length < 2)
            {
                FileInfoList = new ObservableCollection<FileInfo>(Project.FileInfos);
                return;
            }
            string selNodePath = Project.ID + "\\" + GetTreeFullPath(TreeNode);
            var fileInfoList = from f in new ObservableCollection<FileInfo>(Project.FileInfos)
                               where f.Path.IndexOf(selNodePath) >-1
                               select f;
            FileInfoList = new ObservableCollection<FileInfo>(fileInfoList);
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
            for (int i = 0; i < FileInfoList.Count; i++)
            {
                FileInfoList[i].FullPath = "D:\\vs-workspace\\Test\\" + FileInfoList[i].Path + FileInfoList[i].ID + FileInfoList[i].Extension;
            }
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

        /// <summary>
        /// 附件上传
        /// </summary>
        private void FileUpLoad()
        {
            //TreeNode = selectedTreeNode as TreeNode;
            if (TreeNode == null || TreeNode.ID.Length != 6)
            {
                MessageBox.Show("请选择叶节点", "提示");
                return;
            }

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
                // 项目路径
                string projectPath = FileHelper.ReadConfigInfo();

                String[] SelectedFilePaths = openFileDialog.FileNames;
                //bool isShow = false;
                foreach (String baseAddress in SelectedFilePaths)
                {
                    // 判断图片大小，不能超过2m（1024*2）kb
                    System.IO.FileInfo selectedFileInfo = new System.IO.FileInfo(baseAddress);
                    //if (selectedFileInfo.Length > 1024 * 1024 * 2)
                    //{
                    //    isShow = true;
                    //    continue;
                    //}
                    String filename = System.IO.Path.GetFileNameWithoutExtension(baseAddress); // 文件原名称
                    String extension = System.IO.Path.GetExtension(baseAddress); // 文件扩展名
                    String path = Project.ID + "\\" + GetTreeFullPath(TreeNode); // 相对路径
                    FileInfo uploadFile = new FileInfo();
                    uploadFile.ID = Guid.NewGuid();
                    uploadFile.ProjectID = Project.ID;
                    uploadFile.Name = filename;
                    uploadFile.Extension = extension;
                    uploadFile.Path = path;
                    uploadFile.Type = TreeNode.Name;
                    uploadFile.UpdateTime = DateTime.Now;
                    StringBuilder savePath = new StringBuilder(); // 文件保存路径
                    savePath.Append(projectPath).Append("\\").Append(path);
                    //address.Append(Project.ID).Append("\\").Append(TreeNode.Name);
                    if (!System.IO.Directory.Exists(savePath.ToString()))
                    {
                        System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(savePath.ToString());
                        directoryInfo.Create();
                    }
                    savePath.Append(uploadFile.ID).Append(uploadFile.Extension);
                    System.IO.FileStream fsRead = new System.IO.FileStream(baseAddress, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    System.IO.FileStream fsWrite = new System.IO.FileStream(savePath.ToString(), System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
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


                    FileInfoDal fileInfoDal = new FileInfoDal();
                    fileInfoDal.Add(uploadFile);
                    FileInfoList.Add(uploadFile);

                }
                //if (isShow)
                //{
                //    MessageBox.Show("上传的每张图片大小不能超过2M", "提示");
                //}

            }
        }


        /// <summary>
        /// 根据节点ID返回该节点的完整路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetTreeFullPath(TreeNode node)
        {
            if (node == null) return null;
            StringBuilder res = new StringBuilder();
            string id = node.ID;
            string rank1 = string.Empty;
            string rank2 = string.Empty;
            string rank3 = string.Empty;
            switch (node.ID.Length)
            {

                case 2:
                    rank1 = node.Name;
                    res.Append(rank1);
                    break;
                case 4:
                    Recursion(TreeList.FirstOrDefault(), id.Substring(0, 2));
                    rank1 = Name;
                    rank2 = node.Name;
                    res.Append(rank1).Append("\\").Append(rank2);
                    break;
                case 6:
                    Recursion(TreeList.FirstOrDefault(), id.Substring(0, 2));
                    rank1 = Name;
                    Recursion(TreeList.FirstOrDefault(), id.Substring(0, 4));
                    rank2 = Name;
                    rank3 = node.Name;
                    res.Append(rank1).Append("\\").Append(rank2).Append("\\").Append(rank3);
                    break;
                default:

                    break;
            }
            return res.Append("\\").ToString();
            //Recursion(TreeList.FirstOrDefault(), id.Substring(0, 2));
            //string rank1 = Name;
            //Recursion(TreeList.FirstOrDefault(), id.Substring(0, 4));
            //string rank2 = Name;
            //string rank3 = node.Name;

            //return res.Append(rank1).Append("\\").Append(rank2).Append("\\").Append(rank3).Append("\\").ToString();

        }
        /// <summary>
        /// 遍历树
        /// </summary>
        /// <param name="root"></param>
        /// <param name="id"></param>
        private void Recursion(TreeNode root, string id)
        {
            foreach (TreeNode treeNode in root.Children)
            {
                if (treeNode.ID.Length == 6)
                    continue;
                if (treeNode.ID.Equals(id))
                    Name = treeNode.Name;
                Recursion(treeNode, id);
            }
        }

        private void DelFile(object obj)
        {
            try
            {
                DataGrid dataGrid = obj as DataGrid;
                FileInfo = dataGrid.SelectedItem as FileInfo;
                FileHelper.DelFile(fileInfo);
                FileInfoList.Remove(FileInfo);
            }
            catch (Exception ex)
            {
                ErrorDialogViewModel.getInstance().show(ex);
                return;
            }
        }

        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitTreeView()
        {
            TreeList = new ObservableCollection<TreeNode>
            {
                new TreeNode("0", "附件类型",
                    new TreeNode ("01", "初始（首次）登记",
                        new TreeNode("0101", "国有建设用地使用权丨房屋所有权",
                            new TreeNode("010101", "登记费发票"),
                            new TreeNode("010102", "询问笔录"),
                            new TreeNode("010103", "领证凭证"),
                            new TreeNode("010104", "申请书"),
                            new TreeNode("010105", "税票"),
                            new TreeNode("010106", "房屋测绘报告"),
                            new TreeNode("010107", "开发建设单位与业主的书面约定材料"),
                            new TreeNode("010108", "建设工程规划许可证明文件"),
                            new TreeNode("010109", "其他法律、法规材料"),
                            new TreeNode("010110", "申请人身份证明"),
                            new TreeNode("010111", "不动产登记申请审批表"),
                            new TreeNode("010112", "不动产权属证书"),
                            new TreeNode("010113", "房屋、建设用地项目竣工验收证明文件")
                        ),
                        new TreeNode("0102", "一般抵押权",
                            new TreeNode("010201", "受理材料"),
                            new TreeNode("010202", "建设工程规划许可证"),
                            new TreeNode("010203", "申请人身份证明"),
                            new TreeNode("010204", "主债权合同"),
                            new TreeNode("010205", "其他法律、法规要求提供的材料"),
                            new TreeNode("010206", "不动产权属证书（证明）"),
                            new TreeNode("010207", "抵押合同"),
                            new TreeNode("010208", "委托书"),
                            new TreeNode("010209", "申请书"),
                            new TreeNode("010210", "税票"),
                            new TreeNode("010211", "领证凭证"),
                            new TreeNode("010212", "登记费发票"),
                            new TreeNode("010213", "询问笔录"),
                            new TreeNode("010214", "不动产登记申请审批表")
                        )
                    ),
                    new TreeNode("02", "预告登记",
                        new TreeNode("0201", "抵押权预告",
                            new TreeNode("020101", "领证凭证"),
                            new TreeNode("020102", "询问笔录"),
                            new TreeNode("020103", "申请书"),
                            new TreeNode("020104", "约定预告登记的协议"),
                            new TreeNode("020105", "登记费发票"),
                            new TreeNode("020106", "主债权合同"),
                            new TreeNode("020107", "房地产抵押合同"),
                            new TreeNode("020108", "申请人身份证明"),
                            new TreeNode("020109", "不动产登记申请审批表"),
                            new TreeNode("020110", "其他法律、法规要求提供的材料")
                        ),
                        new TreeNode("0202", "预告+预抵押",
                            new TreeNode("020201", "不动产登记申请审批表"),
                            new TreeNode("020202", "商品房买卖合同"),
                            new TreeNode("020203", "其他法律、法规要求提供的材料"),
                            new TreeNode("020204", "约定预告登记的协议"),
                            new TreeNode("020205", "房地产抵押合同和主债权合同"),
                            new TreeNode("020206", "申请书"),
                            new TreeNode("020207", "资质"),
                            new TreeNode("020208", "询问笔录"),
                            new TreeNode("020209", "领证凭证"),
                            new TreeNode("020210", "登记费发票"),
                            new TreeNode("020211", "委托书"),
                            new TreeNode("020212", "申请人身份证明")
                        )
                    ),
                    new TreeNode("03", "查（解）封登记",
                        new TreeNode("0301", "查封登记",
                            new TreeNode("030101", "工作证"),
                            new TreeNode("030102", "委托送达函"),
                            new TreeNode("030103", "协助执行通知书"),
                            new TreeNode("030104", "（预）查封裁定书"),
                            new TreeNode("030105", "申请书"),
                            new TreeNode("030106", "查封函"),
                            new TreeNode("030107", "其他法律、法规要求提供的材料"),
                            new TreeNode("030108", "查封决定书"),
                            new TreeNode("030109", "协助查封通知书")
                        )
                    )
                )
            };
        }
    }
}
