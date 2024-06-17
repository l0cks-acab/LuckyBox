using Oxide.Core;
using Oxide.Core.Plugins;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Oxide.Plugins
{
    [Info("LuckyBoxNotifier", "herbs.acab", "1.2.0")]
    [Description("Notifies via Discord when a new LuckyBox is spawned and when it is found.")]
    public class LuckyBoxNotifier : RustPlugin
    {
        [PluginReference]
        private Plugin LuckyBox;

        private ConfigData configData;

        private class ConfigData
        {
            public string WebhookUrl { get; set; }
            public string AdminWebhookUrl { get; set; }
            public string AdminFindWebhookUrl { get; set; }
        }

        protected override void LoadDefaultConfig()
        {
            PrintWarning("Creating a new configuration file...");
            configData = new ConfigData
            {
                WebhookUrl = "YOUR_WEBHOOK_URL_HERE",
                AdminWebhookUrl = "YOUR_ADMIN_WEBHOOK_URL_HERE",
                AdminFindWebhookUrl = "YOUR_ADMIN_FIND_WEBHOOK_URL_HERE"
            };
            SaveConfig();
        }

        private void Init()
        {
            LoadConfigData();

            // Check if the LuckyBox plugin is loaded
            if (LuckyBox == null)
            {
                PrintError("LuckyBox plugin is not loaded. This plugin requires the LuckyBox plugin to function.");
                return;
            }

            // Subscribe to the event when a new LuckyBox is spawned
            LuckyBox.Call("OnLuckyBoxSpawned", (Action<Vector3, string>)OnLuckyBoxSpawned);
            // Subscribe to the event when a LuckyBox is found
            LuckyBox.Call("OnLuckyBoxFound", (Action<BasePlayer, Vector3, string>)OnLuckyBoxFound);
        }

        private void LoadConfigData()
        {
            configData = Config.ReadObject<ConfigData>();
            if (configData == null)
            {
                LoadDefaultConfig();
            }
        }

        private void OnLuckyBoxSpawned(Vector3 position, string secretKey)
        {
            // Notify regular users
            SendDiscordMessage(configData.WebhookUrl, $"A new LuckyBox has spawned on the map!");

            // Notify admins
            SendDiscordMessage(configData.AdminWebhookUrl, $"A new LuckyBox has spawned at {position} with the secret key: {secretKey}");
        }

        private void OnLuckyBoxFound(BasePlayer player, Vector3 position, string secretKey)
        {
            // Notify admins when a LuckyBox is found
            SendDiscordMessage(configData.AdminFindWebhookUrl, $"{player.displayName} has found a LuckyBox at {position} with the secret key: {secretKey}");
        }

        private void SendDiscordMessage(string webhookUrl, string message)
        {
            var payload = new Dictionary<string, object>
            {
                { "content", message }
            };

            webrequest.Enqueue(webhookUrl, JsonConvert.SerializeObject(payload), (code, response) =>
            {
                if (code != 200)
                {
                    PrintError($"Failed to send Discord message: {response}");
                }
            }, this, RequestMethod.POST, new Dictionary<string, string> { { "Content-Type", "application/json" } });
        }

        private void SaveConfig()
        {
            Config.WriteObject(configData, true);
        }
    }
}
