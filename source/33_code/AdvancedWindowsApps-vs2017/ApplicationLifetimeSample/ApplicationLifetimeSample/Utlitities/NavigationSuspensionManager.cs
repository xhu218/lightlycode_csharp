﻿using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace ApplicationLifetimeSample.Utlitities
{

    public class NavigationSuspensionManager
    {
        private const string NavigationStateFile = "NavigationState.txt";

        public async Task SetNavigationStateAsync(string navigationState)
        {
            StorageFile file = await ApplicationData.Current.LocalCacheFolder.CreateFileAsync(NavigationStateFile, CreationCollisionOption.ReplaceExisting);
            Stream stream = await file.OpenStreamForWriteAsync();
            using (var writer = new StreamWriter(stream))
            {
                await writer.WriteLineAsync(navigationState);
            }
        }

        public async Task<string> GetNavigationStateAsync()
        {
            Stream stream = await ApplicationData.Current.LocalCacheFolder.OpenStreamForReadAsync(NavigationStateFile);
            using (var reader = new StreamReader(stream))
            {
                return await reader.ReadLineAsync();
            }
        }
    }
}
