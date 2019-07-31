using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Models
{
    /// <summary>
    /// 任务信息
    /// </summary>
    public class TaskMessage : BindableBase
    {
        /// <summary>
        /// 任务标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 任务详细信息
        /// </summary>
        public ObservableCollection<string> DetailMessages { get; set; }

        /// <summary>
        /// 任务进度
        /// </summary>
        private double progress;
        public double Progress
        {
            get { return progress; }
            set
            {
                ThreadPool.QueueUserWorkItem(delegate
                {
                    SynchronizationContext.SetSynchronizationContext(new
                    System.Windows.Threading.DispatcherSynchronizationContext(System.Windows.Application.Current.Dispatcher));
                    SynchronizationContext.Current.Post(pl =>
                    {
                        SetProperty(ref progress, value);
                    }, null);
                });
            }
        }


        public TaskMessage()
        {
            DetailMessages = new ObservableCollection<string>();
        }
    }
}
