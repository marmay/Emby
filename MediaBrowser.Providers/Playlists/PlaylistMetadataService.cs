﻿using MediaBrowser.Common.IO;
using MediaBrowser.Controller.Configuration;
using MediaBrowser.Controller.Library;
using MediaBrowser.Controller.Playlists;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Logging;
using MediaBrowser.Providers.Manager;
using System.Collections.Generic;

namespace MediaBrowser.Providers.Playlists
{
    class PlaylistMetadataService : MetadataService<Playlist, ItemLookupInfo>
    {
        public PlaylistMetadataService(IServerConfigurationManager serverConfigurationManager, ILogger logger, IProviderManager providerManager, IProviderRepository providerRepo, IFileSystem fileSystem, IUserDataManager userDataManager) : base(serverConfigurationManager, logger, providerManager, providerRepo, fileSystem, userDataManager)
        {
        }

        /// <summary>
        /// Merges the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        /// <param name="lockedFields">The locked fields.</param>
        /// <param name="replaceData">if set to <c>true</c> [replace data].</param>
        /// <param name="mergeMetadataSettings">if set to <c>true</c> [merge metadata settings].</param>
        protected override void MergeData(Playlist source, Playlist target, List<MetadataFields> lockedFields, bool replaceData, bool mergeMetadataSettings)
        {
            ProviderUtils.MergeBaseItemData(source, target, lockedFields, replaceData, mergeMetadataSettings);

            if (replaceData || string.IsNullOrEmpty(target.PlaylistMediaType))
            {
                target.PlaylistMediaType = source.PlaylistMediaType;
            }

            if (replaceData || target.Shares.Count == 0)
            {
                target.Shares = source.Shares;
            }

            if (mergeMetadataSettings)
            {
                target.LinkedChildren = source.LinkedChildren;
            }
        }
    }
}
