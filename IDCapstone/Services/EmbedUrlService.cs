using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IDCapstone.Services
{
    public class EmbedUrlService
    {
        readonly Regex _Regex;
        readonly ILogger<EmbedUrlService> _Logger;

        public EmbedUrlService(ILogger<EmbedUrlService> logger)
        {
            _Logger = logger;
            _Regex = new System.Text.RegularExpressions.Regex(@"(?:https:\/\/)?clips\.twitch\.tv\/embed\?clip=([A-Za-z]*)");
        }

        public string GetEmbedUrl(string urlOrClipName)
        {
            if (!urlOrClipName.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                urlOrClipName = "https://clips.twitch.tv/embed?clip=" + urlOrClipName;
            }

            var match = _Regex.Match(urlOrClipName);

            if (match.Success)
            {
                _Logger.LogInformation($"Matched URL: {urlOrClipName}");
                return $"https://clips.twitch.tv/embed?clip={match.Groups[1].Value}"+"&parent=localhost";
            }
            else
            {
                _Logger.LogInformation($"Did not match URL: {urlOrClipName}");
                return urlOrClipName;
            }
        }
    }
}
