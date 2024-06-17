# LuckyBox Plugin

The LuckyBox plugin for Rust spawns a lucky box in a small wooden box at a random location outside of monuments. When the lucky box is found, the player receives a note with a secret key, and a firework sound effect is played for all online players. Additionally, Discord webhook notifications are sent with the secret key and when a new lucky box spawns.

# Author

Created by: herbs.acab

## Features

- Spawns a lucky box in a small wooden box at a random location outside of monuments.
- The small wooden box is skinned with the Cargo Heli storage skin.
- Plays a firework sound effect for all online players when the lucky box is found.
- Sends a Discord webhook notification with the secret key when the lucky box is found.
- Sends a Discord webhook notification when a new lucky box spawns somewhere in the world.
- Allows administrators to check the status of the lucky box, create a new lucky box with a specified secret key, and teleport to the lucky box.

## Commands

### Player Commands

- `/box` - Displays the status of the lucky box (whether it has been found or not).

### Admin Commands

- `/locatebox` - Displays the location of the lucky box.
- `/newbox <secret_key>` - Creates a new lucky box with the specified secret key, removing any existing lucky box.
- `/tpbox` - Teleports the admin to the location of the lucky box.

## Permissions

- `luckybox.use` - Allows players to use the `/box` command.
- `luckybox.admin` - Allows administrators to use the `/locatebox`, `/newbox`, and `/tpbox` commands.

## Configuration

The plugin configuration includes the following settings:

- `PredefinedKey` - The default secret key for the lucky box.
- `BoxFound` - Indicates whether the lucky box has been found.
- `WebhookUrl` - The URL for the Discord webhook notification when the lucky box is found.
- `NewBoxWebhookUrl` - The URL for the Discord webhook notification when a new lucky box spawns.
- `BoxPosition` - The last known position of the lucky box.

## Installation

1. Download the `LuckyBox.cs` file and place it in your `oxide/plugins` directory.
2. Start or restart your Rust server to load the plugin.
3. Configure the plugin by editing the `oxide/config/LuckyBox.json` file.

## Example Configuration

```json
{
  "PredefinedKey": "This is a secret key",
  "BoxFound": false,
  "WebhookUrl": "https://discord.com/api/webhooks/your-webhook-url",
  "NewBoxWebhookUrl": "https://discord.com/api/webhooks/your-newbox-webhook-url",
  "BoxPosition": null
}
