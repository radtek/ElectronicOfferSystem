﻿using ProjectModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ProjectModule
{
    public class ProjectModuleModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<AddOrEditProjectDialog>();
            containerRegistry.RegisterForNavigation<ProjectList>();
        }
    }
}