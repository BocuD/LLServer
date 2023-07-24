# LLServer

ASP.net core based server replacement for Love Live School Idol Festival ～after school ACTIVITY～ Next Stage.
Currently in active development.

## Supported Features

- Profile creation
- Live (difficulty) unlocks
- Score saving
- Card saving and leveling
- Badges and nameplates
- おでかけ♪スクールアイドル (the board game at the end of the game)

## Currently unsaved data

- Game history (only used in terminal mode so this doesn't matter right now)
- Stage unlocks (these are all unlocked by default)
- Music unlocks (these are all unlocked by default - note that you still need the right member cards to be able to play a song)

## To do

- Guest mode
- Terminal (card printer) support
- おでかけ♪スクールアイドル card earning
- Some way to get more member cards
- Save above mentioned data
- Ranking (is a placeholder right now)
- Stock machine compatibility / setup guide

# Usage
## Host computer
Clone the repo and open the project in Visual Studio or Rider, then just launch the project.
## Client
Make sure you are running the Nesys service (this can be copied from a stock machine drive from D, make sure to put it in the same folder on D as on the original drive).
A service entry can be made using Utils/create.bat.
Install the registry keys located in the same folder.
Modify your hosts file (`C:\Windows\System32\drivers\etc\hosts`) or use a proxy to point the following domains to your host computer.
Example: (this is assuming your server and client are the same computer)
> 127.0.0.1 cert.nesys.jp
> 
> 127.0.0.1 cert3.nesys.jp
> 
> 127.0.0.1 data.nesys.jp
> 
> 127.0.0.1 proxy.nesys.jp
> 
> 127.0.0.1 nesys.taito.co.jp
> 
> 127.0.0.1 fjm170920zero.nesica.net

Install the certificates located in `LLServer/Certificates` on the client computer using the powershell script located in the same folder. You will then need to patch the game to replace the RSA key to the one used in the server. The public key is located at file offset `0x595200`. The public key that should be used:

> -----BEGIN PUBLIC KEY-----   
> MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAy63nybDg2d0l5Em5RTsx  
> 0QJ4WhuT4DwrzJD/SdPDbOotXE5BiVycfNxcfXVSa74SvqThyQs4KasZyK/NWJN6  
> Xyi7NQgh2xKYc3eVj8b8MSkhz5Y7631dscLQRR9sDiTf2+jR8umd6U9op/ZucaOU  
> zaEcyHalryeeRwD8q7mtlBccL+5dSVVWuPaJ/Oh4Oivk4qNunYHygQ/iw2vBgN3f  
> 6tB1yiKlUe0T51FS1yJcavWilp2JA6XGEhh0OmFJX6wf5vPu9heTXGqnriClinXn  
> XV1zUPDaa0udD8n2OV9NphozqD7TT4pE68G65Xz/iLAaEudSg7f1Shu+VFtt/cF4  
> NwIDAQAB  
> -----END PUBLIC KEY-----
> 
This key can also be found in `LLServer/Common/CryptoConstants.cs`.
If everything is set up correctly, you should be able to start the game and use the original IO board / card reader to scan your card and connect to the server.
