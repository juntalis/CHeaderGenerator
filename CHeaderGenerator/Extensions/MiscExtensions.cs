﻿using EnvDTE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CHeaderGenerator.Extensions
{
    public static class MiscExtensions
    {
        public static List<T> ToList<T>(this T obj)
        {
            return new List<T>() { obj };
        }

        public static IEnumerable<ProjectItem> GetProjectItems(this IEnumerable<UIHierarchyItem> hItems)
        {
            return hItems.Select(i => i.Object as ProjectItem);
        }

        public static ProjectItem FindExistingItem(this Project project, string item)
        {
            string localFile = Path.GetFileName(item);
            foreach (ProjectItem prjItem in project.ProjectItems)
            {
                if (prjItem.Name.Equals(localFile, StringComparison.CurrentCultureIgnoreCase))
                    return prjItem;
            }

            return null;
        }
    }
}
